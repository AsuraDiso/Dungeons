using System;
using UnityEngine;

namespace Dungeons.Game.Health
{
    public class Health : MonoBehaviour
    {
        public event Action Death;
        public event Action HealthDelta;
        public event Action Revive;
        
        [SerializeField] private float _currentHealth;
        [SerializeField] private float _maxHealth;

        public void DoDelta(float val)
        {
            _currentHealth += val;

            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            HealthDelta?.Invoke();
            if (_currentHealth <= 0) Death?.Invoke();
        }

        public bool IsDead()
        {
            return _currentHealth <= 0;
        }

        public float GetPercent()
        {
            return _currentHealth / _maxHealth;
        }

        public void OnRevive()
        {
            _currentHealth = _maxHealth;
            Revive?.Invoke();
        }
    }
}