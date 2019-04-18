using System;

namespace Resources.Ships
{
    public interface IShipStats 
    {
        int GetMaxHealthBonus();
        Guid AddMaxHealthBonus(int bonusHealth);
        void RemoveHealthBonus(Guid id);
        
        int GetDamageMultiplier();
        Guid AddDamageMultiplier(int bonusDamage);
        void RemoveDamageBonus(Guid id);

        float GetFireRateMultiplier();
        Guid AddFireRateMultiplier(float bonusFireRate);
        void RemoveFireRateBonus(Guid id);
    }
}
