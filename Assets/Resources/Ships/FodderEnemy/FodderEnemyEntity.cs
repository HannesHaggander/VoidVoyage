using System;
using Resources.Weapons;
using UnityEngine;

namespace Resources.Ships.FodderEnemy
{
    public class FodderEnemyEntity : MonoBehaviour, IEntity
    {
        public int Health { get; private set; } = 10;

        protected void Start()
        {
        
        }
    
        protected void Update()
        {
        
        }

        public void Move(Vector2 vector)
        {
        
        }

        public void ChangeWeapon(IWeapon weapon)
        {
        
        }

        public void TakeDamage(int amount)
        {
            print($"[{name}:{DateTime.Now:T}] Hit for {amount}");
            Health -= amount;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void DealDamage(IEntity target)
        {
        
        }

        public void Heal(int amount)
        {
        
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public IShipStats GetStats()
        {
            throw new System.NotImplementedException();
        }
    }
}
