using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Resources.Weapons.Rocket
{
    public class RocketWeapon : MonoBehaviour, IWeapon
    {
        public int BaseDamage = 1;
        public float BaseFireRate = 1;
        public GameObject Projectile;

        private DateTime _lastFire;

        public void Fire()
        {
            if( _lastFire.AddSeconds(GetFireRate()) > DateTime.Now) { return; }
            var target = GetClosestTarget();
            if (target == null) { return; }

            _lastFire = DateTime.Now;
            Instantiate(Projectile, transform.position, transform.rotation)
                .GetComponent<RocketProjectile>()
                .SetTarget(target.transform)
                .SetDamage(3)
                .SetSource(transform.parent);
        }

        public GameObject GetClosestTarget()
        {
            KeyValuePair<GameObject, float>? mvp = null;
            foreach(var g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                var distance = Vector3.Distance(transform.position, g.transform.position);
                if (!mvp.HasValue || distance < mvp.Value.Value)
                {
                    mvp = new KeyValuePair<GameObject, float>(g, distance);
                }
            }

            return mvp?.Key;
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
            return GetBaseDamage();
        }

        public float GetFireRate()
        {
            return GetBaseFireRate();
        }

        public Guid SetFireRateIncrease(float fireRate)
        {
            throw new NotImplementedException();
        }

        public Guid SetDamageIncrease(int damage)
        {
            throw new NotImplementedException();
        }

        public void RemoveFireRateIncrease(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveDamageIncrease(Guid id)
        {
            throw new NotImplementedException();
        }

        public void ResetDamage()
        {
            throw new NotImplementedException();
        }

        public void ResetFireRate()
        {
            throw new NotImplementedException();
        }

        public string GetPrefabPath() => "Weapons/Rocket/rocket_weapon_prefab";

        public GameObject GetGameObject() => gameObject;
    }
}
