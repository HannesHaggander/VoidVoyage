using Resources.Weapons;
using System;
using JetBrains.Annotations;
using StaticObjects;
using UnityEditor;
using UnityEngine;


namespace Resources.Ships.Player
{
    [Serializable]
    public class PlayerEntity : MonoBehaviour, IEntity
    {
        public float MovementSpeed = 1;
        private string _inputHorizontal = "Horizontal", _inputVertical = "Vertical";
        private float _inputX, _inputY;
        private Vector2 _inputVector;
        public IWeapon CurrentWeapon;
        public int Health = 100;
        public int MaxHealth = 100;
        [SerializeField]
        private string WeaponPrefabPath = "";
        
        protected void Awake()
        {
            ItemCache.Instance.SetPlayerEntity(this);
            ChangeWeapon(GetComponentInChildren<IWeapon>());
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

        public void Move(Vector2 vector)
        {
            var moveVector =  vector.normalized * MovementSpeed * Time.deltaTime;
            transform.Translate(moveVector.x, moveVector.y, 0, Space.Self);
        }

        public void ChangeWeapon([NotNull] IWeapon weapon)
        {
            CurrentWeapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
            WeaponPrefabPath = CurrentWeapon.GetPrefabPath();
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
    }
}
