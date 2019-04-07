using Resources.Ships;
using System;
using System.Linq;
using CodeExtensions;
using JetBrains.Annotations;
using UnityEngine;

namespace Resources.Weapons.Laser
{
    public class LaserProjectile : MonoBehaviour, IProjectile<LaserProjectile>
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
            Transform t = null;
            if (_source != null && col.transform.IsEntityChild(_source, out t)) { return; }
            
            var entity = t?.GetComponent<IEntity>();
            if(entity == null) { throw new Exception("Root entity items has to have IEntity interface implementation");}
            entity.TakeDamage(Damage);
            Destroy(gameObject);
        }

        public IProjectile<LaserProjectile> SetDamage(int damage)
        {
            if(damage >= 0) { Damage = damage; }
            return this;
        }

        public IProjectile<LaserProjectile> SetSource(Transform source)
        {
            _source = source.root;
            return this;
        }
    }
}
