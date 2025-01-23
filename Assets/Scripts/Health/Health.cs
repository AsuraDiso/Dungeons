using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnDeath;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Animator _animator;
    public void DoDelta(float val)
    {
        _currentHealth += val;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth <= 0)
        {
            //_animator.SetTrigger("Death");
        }
    }

    public bool IsDead()
    {
        return _currentHealth <= 0;
    }

    public float GetPercent()
    {
        return _currentHealth / _maxHealth;
    }

    public void Revive()
    {
        _currentHealth = _maxHealth;
        _animator.SetTrigger("Revive");
    }

}