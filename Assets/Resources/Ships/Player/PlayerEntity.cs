using Assets.StaticObjects;
using JetBrains.Annotations;
using Messages;
using Resources.Weapons;
using StaticObjects;
using System;
using UnityEngine;


namespace Resources.Ships.Player
{
    [Serializable]
    public class PlayerEntity : MonoBehaviour, IEntity, IObjectReceiver
    {
        public float MovementSpeed = 1;
        public int Health = 100, MaxHealth = 100;
        public IWeapon CurrentWeapon
        {
            get => _currentWeaponCache;
            set
            {
                _currentWeaponCache = value;
                WeaponPrefabPath = value?.GetPrefabPath() ?? "";
            }
        }

        private string _inputHorizontal = "Horizontal", _inputVertical = "Vertical";
        private float _inputX, _inputY;
        private Vector2 _inputVector;
        private IWeapon _currentWeaponCache;
        [SerializeField] private string WeaponPrefabPath = "";
        private Guid? _subIdWeaponMessage;

        public Transform WeaponSlot;
        
        protected void Awake()
        {
            ItemCache.Instance.SetPlayerEntity(this);
        }

        protected void Start()
        {
            _subIdWeaponMessage = ObjectMessenger.Instance.Subscribe(typeof(WeaponMessage), this);
        }

        protected void Update()
        {
            _inputX = Input.GetAxisRaw(_inputHorizontal);
            _inputY = Input.GetAxisRaw(_inputVertical);
            _inputVector.x = _inputX;
            _inputVector.y = _inputY;
            if(Math.Abs(_inputVector.magnitude) > 0) { Move(_inputVector); }
            if (Input.GetButton("Fire1")) { CurrentWeapon?.Fire(); }

            if (Input.GetKeyDown(KeyCode.U))
            {
                print(JsonUtility.ToJson(this, true));
            }
        }

        protected void OnDestroy()
        {
            if (_subIdWeaponMessage.HasValue)
            {
                ObjectMessenger.Instance.Unsubscribe(_subIdWeaponMessage.Value);
            }
        }

        public void Move(Vector2 vector)
        {
            var moveVector =  vector.normalized * MovementSpeed * Time.deltaTime;
            transform.Translate(moveVector.x, moveVector.y, 0, Space.Self);
        }

        public void ChangeWeapon([NotNull] IWeapon weapon)
        {
            if(CurrentWeapon != null)
            {
                Destroy(CurrentWeapon.GetGameObject());
            }

            CurrentWeapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
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

            Health = Mathf.Clamp(Health + amount, Health, MaxHealth);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public GameObject GetGameObject() => gameObject;

        public void ReceiveObject(object message)
        {
            if(message is WeaponMessage weaponMsg)
            {
               ChangeWeapon(Instantiate(UnityEngine.Resources.Load<GameObject>(
                           weaponMsg.weapon.GetPrefabPath()), WeaponSlot.position, transform.rotation, transform)
                   .GetComponent<IWeapon>());
            }
        }
    }
}
