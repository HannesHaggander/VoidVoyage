using System;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Ships.Player
{
    public class PlayerShipStats : MonoBehaviour, IShipStats
    {
        private int _maxHealthBonus = 0, _bonusDamage = 0;
        private float _fireRateBonus = 0;
        private readonly Dictionary<Guid, StatTracker> _activeStats = new Dictionary<Guid, StatTracker>();

        #region MonoFunctions
        protected void Awake()
        {
        
        }

        protected void Start()
        {
        
        }

        protected void Update()
        {
        
        }

        protected void FixedUpdate()
        {
        
        }

        protected void LateUpdate()
        {
        
        }

        protected void OnDestroy()
        {
        
        }

        #endregion

        #region MonoTriggerEvents 

        protected void OnTriggerEnter2D()
        {
        
        }

        protected void OnTriggerStay2D()
        {
        
        }

        protected void OnTriggerExit2D()
        {
        
        }

        #endregion

        public int GetMaxHealthBonus() => _maxHealthBonus;

        public Guid AddMaxHealthBonus(int bonusHealth)
        {
            var trackId = Guid.NewGuid();
            _activeStats.Add(trackId, new StatTracker(StatTracker.StatType.Health, bonusHealth));
            _maxHealthBonus += bonusHealth;
            return trackId;
        }

        public void RemoveHealthBonus(Guid id)
        {
            if(!_activeStats.TryGetValue(id, out var stat)) { return; }

            _maxHealthBonus -= (int)stat.Value;
            _activeStats.Remove(id);
        }

        public int GetDamageMultiplier() => _bonusDamage;

        public Guid AddDamageMultiplier(int bonusDamage)
        {
            var trackId = Guid.NewGuid();
            _activeStats.Add(trackId, new StatTracker(StatTracker.StatType.Damage, bonusDamage));
            _bonusDamage += bonusDamage;
            return trackId;
        }

        public void RemoveDamageBonus(Guid id)
        {
            if(!_activeStats.TryGetValue(id, out var stat)) { return; }

            _bonusDamage -= (int)stat.Value;
            _activeStats.Remove(id);
        }

        public float GetFireRateMultiplier() => _fireRateBonus;

        public Guid AddFireRateMultiplier(float bonusFireRate)
        {
            var trackId = Guid.NewGuid();
            _activeStats.Add(trackId, new StatTracker(StatTracker.StatType.FireRate, bonusFireRate));
            _fireRateBonus += bonusFireRate;
            return trackId;
        }

        public void RemoveFireRateBonus(Guid id)
        {
            if(!_activeStats.TryGetValue(id, out var stat)) { return; }

            _fireRateBonus -= (float)stat.Value;
            _activeStats.Remove(id);
        }
    }

    internal class StatTracker
    {
        public object Value { get; private set; }
        public StatType Stat { get; private set; }

        public enum StatType
        {
            NotSet,
            Health,
            Damage, 
            FireRate
        }

        public StatTracker(StatType stat, object value)
        {
            Stat = stat;
            Value = value;
        }
    }
}
