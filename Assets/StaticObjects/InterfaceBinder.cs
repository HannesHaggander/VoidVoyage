using System;
using Messages;
using Persistence;
using Resources.Items.Overcharge;
using Resources.Weapons.Laser;
using Resources.Weapons.Rocket;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

namespace StaticObjects
{
    public class InterfaceBinder : MonoBehaviour
    {
        public Transform[] Buttons;
        public bool StartVisible;

        protected void Start()
        {
            foreach (var button in Buttons)
            {
                button.gameObject.SetActive(StartVisible);
            }
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleButtonActiveStatus();
            }
        }

        private void ToggleButtonActiveStatus()
        {
            foreach (var button in Buttons)
            {
                button.gameObject.SetActive(!button.gameObject.activeSelf);
            }
        }

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
