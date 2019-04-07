using System;
using UnityEngine;

namespace Resources.Weapons
{
    public interface IWeapon
    {
        void Fire();
        int GetBaseDamage();
        float GetBaseFireRate();
        int GetDamage();
        float GetFireRate();
        Guid SetFireRateIncrease(float fireRate);
        Guid SetDamageIncrease(int damage);
        void RemoveFireRateIncrease(Guid id);
        void RemoveDamageIncrease(Guid id);
        void ResetDamage();
        void ResetFireRate();
        string GetPrefabPath();
        GameObject GetGameObject();
    }
}
