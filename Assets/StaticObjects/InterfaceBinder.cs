using Messages;
using Resources.Items.Overcharge;
using Resources.Weapons.Laser;
using Resources.Weapons.Rocket;
using UnityEngine;

namespace StaticObjects
{
    public class InterfaceBinder : MonoBehaviour
    {
        public void ChangeWeaponLaser()
        {
            var path = LaserWeapon.ResourcePrefabPath;
            ObjectMessenger.Instance.Publish(new WeaponMessage
            {
                Weapon = new LaserWeapon()
            });
        }

        public void ChangeWeaponRocket()
        {
            ObjectMessenger.Instance.Publish(new WeaponMessage
            {
                Weapon = new RocketWeapon()
            });
        }

        public void ChangeItemOverCharge()
        {
            ObjectMessenger.Instance.Publish(new ItemMessage
            {
                Item = new OverchargeItem()
            });
        }

        public void LoadPersistables()
        {
            ObjectMessenger.Instance.Publish(new LoadPersistablesMessage());
        }

        public void SavePersistables()
        {
            ObjectMessenger.Instance.Publish(new SavePersistablesMessage());
        }
    }
}
