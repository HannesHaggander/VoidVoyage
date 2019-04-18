using CodeExtensions;
using Resources.Ships;
using StaticObjects;
using System;
using UnityEngine;

namespace Resources.Weapons.Laser
{
    public class LaserWeapon : MonoBehaviour, IWeapon
    {
        public float BaseFireRate = 0.5f;
        public int BaseDamage = 1;
        public GameObject Projectile;
        public Transform StatsObject { get; private set; }

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
                .GetComponent<LaserProjectile>()
                .SetDamage(5)
                .SetSource(transform.parent);
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
            return Mathf.Clamp(BaseDamage + GetStats().GetDamageMultiplier(), 0, int.MaxValue);
        }

        public float GetFireRate()
        {
            return Mathf.Clamp(BaseFireRate + GetStats().GetFireRateMultiplier(), 0.1f, float.PositiveInfinity);
        }

        public string GetPrefabPath()
        {
            return "Weapons/Laser/laser_weapon_prefab";
        }

        public GameObject GetGameObject() => gameObject;

        public void SetStatsObject(Transform statsObject)
        {
            StatsObject = statsObject;
        }

        private IShipStats GetStats() => StatsObject.GetComponent<IShipStats>();
        
    }
}
