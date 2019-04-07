using StaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeExtensions;
using UnityEditor;
using UnityEngine;

namespace Resources.Weapons.Laser
{
    public class LaserWeapon : MonoBehaviour, IWeapon
    {
        public float BaseFireRate = 0.5f;
        public int BaseDamage = 1;
        public GameObject Projectile;

        private readonly Dictionary<Guid, float> _fireRateIncrease = new Dictionary<Guid, float>();
        private readonly Dictionary<Guid, int> _damageIncrease = new Dictionary<Guid, int>();
        private Camera _mainCamera;
        private DateTime _lastFire;

        protected void Update()
        {
            if(_mainCamera == null) { _mainCamera = ItemCache.Instance.MainCamera; }
            else { transform.Rotate2DToPosition(_mainCamera.ScreenToWorldPoint(Input.mousePosition)); }
        }

        public void Fire()
        {
            if(_lastFire.AddSeconds(GetFireRate()) > DateTime.Now) { return; }
            _lastFire = DateTime.Now;
            Instantiate(Projectile, transform.position, transform.rotation)
                .GetComponent<IProjectile>()
                .SetDamage(5)
                .SetSource(transform);
        }

        public int GetBaseDamage()
        {
            return BaseDamage;
        }

        public float GetBaseFireRate()
        {
            return BaseFireRate;
        }

        public int GetDamage()
        {
            return Mathf.Clamp(BaseDamage + _damageIncrease.Sum(pair => pair.Value), 0, int.MaxValue);
        }

        public float GetFireRate()
        {
            return Mathf.Clamp(BaseFireRate + _fireRateIncrease.Sum(pair => pair.Value), 0.1f, float.PositiveInfinity);
        }

        public Guid SetFireRateIncrease(float fireRate)
        {
            var guid = Guid.NewGuid();
            _fireRateIncrease.Add(guid, -fireRate);
            return guid;
        }

        public Guid SetDamageIncrease(int damage)
        {
            var guid = Guid.NewGuid();
            _damageIncrease.Add(guid, damage);
            return guid;
        }

        public void RemoveFireRateIncrease(Guid id)
        {
            _fireRateIncrease.Remove(id);
        }

        public void RemoveDamageIncrease(Guid id)
        {
            _damageIncrease.Remove(id);
        }

        public void ResetDamage()
        {
            _damageIncrease.Clear();
        }

        public void ResetFireRate()
        {
            _fireRateIncrease.Clear();
        }

        public string GetPrefabPath()
        {
            return "Weapons/Laser/laser_weapon_prefab";
        }
    }
}
