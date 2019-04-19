using System;
using System.Collections;
using Resources.Ships;
using UnityEngine;

namespace Resources.Items.Overcharge
{
    public class OverchargeItem : MonoBehaviour, IItem
    {
        private Guid? _damageTrackId, _fireRateTrackId;
        private float _duration = 5;
        private Coroutine _cooldownRoutine;
        public Transform StatsTransform { get; private set; }

        private IEnumerator StartCoolDown()
        {
            yield return new WaitForSecondsRealtime(_duration);
            Deactivate();
        }

        public void Activate()
        {
            if(_cooldownRoutine != null) { return; }

            print($"{name}{DateTime.Now:T}: Activating item");
            _cooldownRoutine = StartCoroutine(StartCoolDown());
            
            _damageTrackId = GetStats().AddDamageMultiplier(1);
            _fireRateTrackId = GetStats().AddFireRateMultiplier(-1);
        }

        public void Deactivate()
        {
            print($"{name}{DateTime.Now:T}: Deactivating item");

            if (_damageTrackId.HasValue)
            {
                GetStats().RemoveDamageBonus(_damageTrackId.Value);
                _damageTrackId = null;
            }

            if (_fireRateTrackId.HasValue)
            {
                GetStats().RemoveFireRateBonus(_fireRateTrackId.Value);
                _fireRateTrackId = null;
            }
            
            if(_cooldownRoutine != null)
            {
                StopCoroutine(_cooldownRoutine);
                _cooldownRoutine = null;
            }
        }

        public string GetPrefabPath()
        {
            return "Items/Overcharge/overcharge_item_prefab";
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        private IShipStats GetStats()
        {
            return StatsTransform.GetComponent<IShipStats>();
        }

        public void SetStatsObject(Transform statsObject)
        {
            StatsTransform = statsObject;
        }

        protected void OnDestroy()
        {
            Deactivate();
        }
    }
}
