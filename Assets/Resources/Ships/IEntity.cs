using Resources.Weapons;
using UnityEngine;

namespace Resources.Ships
{
    public interface IEntity
    {
        void Move(Vector2 vector);
        void ChangeWeapon(IWeapon weapon);
        void TakeDamage(int amount);
        void DealDamage(IEntity target);
        void Heal(int amount);
        Transform GetTransform();
    }
}
