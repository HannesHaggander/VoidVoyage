using UnityEngine;

namespace Resources.Weapons
{
    public interface IProjectile
    {
        IProjectile SetDamage(int damage);
        IProjectile SetSource(Transform source);
    }
}
