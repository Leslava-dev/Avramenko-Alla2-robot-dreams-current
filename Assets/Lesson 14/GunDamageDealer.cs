using System;
using UnityEngine;

namespace Dummies
{
    public class GunDamageDealer : MonoBehaviour
    {
        public event Action<int> OnHit;

        [SerializeField] private HealthSystem _healthSystem;
        [SerializeField] private RaycastShoot _gun;
        [SerializeField] private int _damage = 25;

        public RaycastShoot Gun => _gun;

        private void Start()
        {
            _gun.OnHit += GunHitHandler;
        }

        private void GunHitHandler(Collider collider)
        {
            Health health = null;

            if (_healthSystem.GetHealth(collider, out health))
            {
                Debug.Log("Before hit HP = " + health.HealthValue);
                health.TakeDamage(_damage);
                Debug.Log("After hit HP = " + health.HealthValue);
            }
            OnHit?.Invoke(health != null ? 1 : 0);
        }
        
    }
}