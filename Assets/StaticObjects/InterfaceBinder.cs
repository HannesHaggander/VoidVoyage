using Messages;
using Resources.Weapons.Laser;
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

        }
    }
}
