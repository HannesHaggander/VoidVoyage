using Messages;
using Resources.Weapons.Laser;
using Resources.Weapons.Rocket;
using UnityEngine;

namespace StaticObjects
{
    public class InterfaceBinder : MonoBehaviour
    {
        public void ChangeWeaponLaser()
        {
            ObjectMessenger.Instance.Publish(new WeaponMessage
            {
                weapon = new LaserWeapon()
            });
        }

        public void ChangeWeaponRocket()
        {
            ObjectMessenger.Instance.Publish(new WeaponMessage
            {
                weapon = new RocketWeapon()
            });
        }
    }
}
