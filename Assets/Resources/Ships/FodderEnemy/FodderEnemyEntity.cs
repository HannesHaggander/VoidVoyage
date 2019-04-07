using System.Collections;
using System.Collections.Generic;
using Resources.Ships;
using Resources.Weapons;
using UnityEngine;

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
        Health -= amount;
        print($"Took {amount} damage, current hp: {Health}");
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
}
