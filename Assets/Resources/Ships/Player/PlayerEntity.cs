using Assets.StaticObjects;
using JetBrains.Annotations;
using Messages;
using Persistence;
using Resources.Items;
using Resources.Weapons;
using StaticObjects;
using System;
using System.Linq;
using UnityEngine;


namespace Resources.Ships.Player
{
    [Serializable]
    [RequireComponent(typeof(PlayerShipStats))]
    public class PlayerEntity : MonoBehaviour, IEntity, IObjectReceiver, IPersistable
    {
        [NonSerialized]
        public float MovementSpeed = 5;
        public int Health = 100, MaxHealth = 100;
        public IWeapon CurrentWeapon
        {
            get => _currentWeaponCache;
            set
            {
                _currentWeaponCache = value;
                _weaponPrefabPath = value?.GetPrefabPath() ?? "";
            }
        }
        public IItem CurrentItem
        {
            get => _currentItemCache;
            set
            {
                _currentItemCache = value;
                _itemPrefabPath = value?.GetPrefabPath();
            }
        }
        public PlayerShipStats Stats
        {
            get;
            private set;
        }

        private string _inputHorizontal = "Horizontal", _inputVertical = "Vertical";
        private float _inputX, _inputY;
        private Vector2 _inputVector;
        private IWeapon _currentWeaponCache;
        private IItem _currentItemCache;
        [SerializeField][HideInInspector]
        private string _weaponPrefabPath = "", _itemPrefabPath = "";
        private Guid? _subIdWeaponMessage, _subIdItemMessage, _subIdSavePersistables, _subIdLoadPersistables;
        [NonSerialized]
        public Transform WeaponSlots, ItemSlots;
        
        protected void Awake()
        {
            ItemCache.Instance.SetPlayerEntity(this);
            WeaponSlots = GetComponentsInChildren<Transform>().FirstOrDefault(x => x.tag.Equals("WeaponSlot"));
            ItemSlots = GetComponentsInChildren<Transform>().FirstOrDefault(x => x.tag.Equals("ItemSlot"));
            Stats = GetComponent<PlayerShipStats>();
        }

        protected void Start()
        {
            _subIdWeaponMessage = ObjectMessenger.Instance.Subscribe(typeof(WeaponMessage), this);
            _subIdItemMessage = ObjectMessenger.Instance.Subscribe(typeof(ItemMessage), this);
            _subIdSavePersistables = ObjectMessenger.Instance.Subscribe(typeof(SavePersistablesMessage), this);
            _subIdLoadPersistables = ObjectMessenger.Instance.Subscribe(typeof(LoadPersistablesMessage), this);

            Load();
        }

        protected void Update()
        {
            _inputX = Input.GetAxisRaw(_inputHorizontal);
            _inputY = Input.GetAxisRaw(_inputVertical);
            _inputVector.x = _inputX;
            _inputVector.y = _inputY;
            if(Math.Abs(_inputVector.magnitude) > 0) { Move(_inputVector); }
            if (Input.GetButton("Fire1")) { CurrentWeapon?.Fire(); }
            if (Input.GetKeyDown(KeyCode.F)) { CurrentItem?.Activate(); }

            if (Input.GetKeyDown(KeyCode.U)) { Save(); }
            if (Input.GetKeyDown(KeyCode.L)) { Load(); }
        }

        protected void OnDestroy()
        {
            ObjectMessenger.Instance.Unsubscribe(ref _subIdWeaponMessage);
            ObjectMessenger.Instance.Unsubscribe(ref _subIdItemMessage);
            ObjectMessenger.Instance.Unsubscribe(ref _subIdSavePersistables);
            ObjectMessenger.Instance.Unsubscribe(ref _subIdLoadPersistables);
        }

        public void Move(Vector2 vector)
        {
            var moveVector =  vector.normalized * MovementSpeed * Time.deltaTime;
            transform.Translate(moveVector.x, moveVector.y, 0, Space.Self);
        }

        public void ChangeWeapon(string prefabPath)
        {
            if(CurrentWeapon != null)
            {
                Destroy(CurrentWeapon.GetGameObject());
                CurrentWeapon = null;
            }

            if (string.IsNullOrEmpty(prefabPath)) { return; }

            var weapon = Instantiate(UnityEngine.Resources.Load<GameObject>(
                    prefabPath), WeaponSlots.position, transform.rotation, transform)
                .GetComponent<IWeapon>();

            ChangeWeapon(weapon);
            weapon.SetStatsObject(transform);
        }

        public void ChangeWeapon([NotNull] IWeapon weapon)
        {
            if(CurrentWeapon != null)
            {
                Destroy(CurrentWeapon.GetGameObject());
            }

            CurrentWeapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void ChangeItem(string prefabPath)
        {
            if(CurrentItem != null)
            {
                Destroy(CurrentItem.GetGameObject());
                CurrentItem = null;
            }

            if (string.IsNullOrEmpty(prefabPath)) { return; }

            var item = Instantiate(UnityEngine.Resources.Load<GameObject>(
                    prefabPath), ItemSlots.position, transform.rotation, transform)
                .GetComponent<IItem>();

            ChangeItem(item);
            item.SetStatsObject(transform);
        }

        public void ChangeItem([NotNull] IItem item)
        {
            if(CurrentItem != null)
            {
                Destroy(CurrentItem.GetGameObject());
            }

            CurrentItem = item ?? throw new ArgumentNullException(nameof(item));
        }

        public void TakeDamage(int amount)
        {
            if(amount < 0) { return; }

            Health -= amount;
        }

        public void DealDamage([NotNull] IEntity target)
        {
            if(target == null) { throw new ArgumentNullException(nameof(target)); }
            if(CurrentWeapon == null) { return; }
            target.TakeDamage(CurrentWeapon.GetDamage());
        }

        public void Heal(int amount)
        {
            if(amount < 0) { return; }

            Health = Mathf.Clamp(Health + amount, Health, MaxHealth + Stats.GetMaxHealthBonus());
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public IShipStats GetStats()
        {
            return Stats;
        }

        public GameObject GetGameObject() => gameObject;

        public void ReceiveObject(object message)
        {
            if(message is WeaponMessage weaponMsg)
            {
                ChangeWeapon(weaponMsg.Weapon.GetPrefabPath());
            }

            if(message is ItemMessage itemMsg)
            {
                ChangeItem(itemMsg.Item.GetPrefabPath());
            }

            if(message is SavePersistablesMessage)
            {
                Save();
            }

            if(message is LoadPersistablesMessage)
            {
                Load();
            }
        }

        public void Save()
        {
            PersistenceOperations.SaveToFile(nameof(PlayerEntity), this);
        }

        public void Load()
        {
            JsonUtility.FromJsonOverwrite(PersistenceOperations.GetFileContent(nameof(PlayerEntity)), this);
            ChangeWeapon(_weaponPrefabPath);
            ChangeItem(_itemPrefabPath);
        }
    }
}
