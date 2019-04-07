using System;
using UnityEngine;

namespace Resources.Weapons.Rocket
{
    public class RocketWeapon : MonoBehaviour, IWeapon
    {
        public int BaseDamage = 1;
        public float BaseFireRate = 1;

        void Start()
        {
        
        }

        void Update()
        {
        
        }

        public void Fire()
        {
            print("fire ze rockets!");
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
