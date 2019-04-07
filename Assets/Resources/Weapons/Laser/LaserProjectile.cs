using Resources.Ships;
using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Resources.Weapons.Laser
{
    public class LaserProjectile : MonoBehaviour, IProjectile
    {
        private float _speed = 20;
        public int Damage = 1;
        private Transform _source;

        protected void Awake()
        {
            Destroy(gameObject, 2);
        }

        protected void Update()
        {
            transform.position += transform.right * _speed * Time.deltaTime;
        }

        protected void OnTriggerEnter2D([NotNull] Collider2D col)
        {
            var currentParent = col.transform;
            if (_source != null)
            {
                while (true)
                {
                    if (currentParent.CompareTag("EntityRoot")) { break; }

                    currentParent = currentParent.parent;
                    if (currentParent != null) { continue; }

                    Debug.LogError("Failed to find entity root");
                    return;
                }

                if(currentParent.GetComponentsInChildren<Transform>().Contains(_source))
                {
                    return;
                }
            }

            //if (_source != null && col.transform.root.GetComponentsInChildren<Transform>().Contains(_source)) { return; }

            var entity = currentParent.GetComponent<IEntity>();
            if(entity == null) { throw new Exception("Root entity items has to have IEntity interface implementation");}
            entity.TakeDamage(Damage);
            Destroy(gameObject);
        }

        public IProjectile SetDamage(int damage)
        {
            if(damage >= 0) { Damage = damage; }
            return this;
        }

        public IProjectile SetSource(Transform source)
        {
            _source = source.root;
            return this;
        }
    }
}
