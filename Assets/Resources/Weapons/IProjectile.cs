using UnityEngine;

namespace Resources.Weapons
{
    public interface IProjectile<T>
    {
        IProjectile<T> SetDamage(int damage);
        IProjectile<T> SetSource(Transform source);
    }
}
