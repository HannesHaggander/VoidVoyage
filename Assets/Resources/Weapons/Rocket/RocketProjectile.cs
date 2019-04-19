using CodeExtensions;
using Resources.Ships;
using System;
using UnityEngine;

namespace Resources.Weapons.Rocket
{
    public class RocketProjectile : MonoBehaviour, IProjectile<RocketProjectile>
    {
        public Transform Target;
        private float _speed = 5;
        private int _damage = 1;
        private Transform _source;

        protected void Update()
        {
            MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            if (!Target)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.Lerp(transform.position, Target.position, _speed * Time.deltaTime);
        }

        public IProjectile<RocketProjectile> SetTarget(Transform target)
        {
            Target = target;
            return this;
        }

        protected void OnTriggerEnter2D(Collider2D col)
        {
            Transform t = null;
            if(_source != null && col.transform.IsEntityChild(_source, out t))
            {
                return;
            }

            var entity = t?.GetComponent<IEntity>();
            if(entity == null)
            {
                throw new Exception("Failed to find entity in entity root");
            }

            entity.TakeDamage(_damage);
            Destroy(gameObject);
        }

        public IProjectile<RocketProjectile> SetDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public IProjectile<RocketProjectile> SetSource(Transform source)
        {
            _source = source;
            return this;
        }
    }
}

