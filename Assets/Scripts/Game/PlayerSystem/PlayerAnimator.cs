using System;
using InventorySystem;
using Movements;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Player _player;
        private static readonly int Hit = Animator.StringToHash("Hit");

        private void Awake()
        {
            _player.Health.OnDeath += OnDeath;
            // _player.Health.OnHit += OnHit;
        }

        private void OnDeath()
        {
        }

        public void OnHit()
        {
        }
    }
}