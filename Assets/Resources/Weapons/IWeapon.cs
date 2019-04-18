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
        string GetPrefabPath();
        GameObject GetGameObject();
        void SetStatsObject(Transform statsObject);
    }
}
