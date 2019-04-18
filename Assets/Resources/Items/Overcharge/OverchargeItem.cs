using Resources.Ships;
using Resources.Ships.Player;
using StaticObjects;
using System;
using System.Collections;
using UnityEngine;

namespace Resources.Items
{
    public class OverchargeItem : MonoBehaviour, IItem
    {
        private Guid? _damageTrackId, _fireRateTrackId;
        private float _duration = 5;
        private Coroutine _cooldownRoutine;
        public Transform StatsTransform { get; private set; }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && _cooldownRoutine == null)
            {
                Activate();
            }
        }

        private IEnumerator StartCoolDown()
        {
            yield return new WaitForSecondsRealtime(_duration);
            Deactivate();
        }

        public void Activate()
        {
            print($"{name}{DateTime.Now:T}: Activating item");
            _cooldownRoutine = StartCoroutine(StartCoolDown());
            
            _damageTrackId = GetStats().AddDamageMultiplier(10);
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
