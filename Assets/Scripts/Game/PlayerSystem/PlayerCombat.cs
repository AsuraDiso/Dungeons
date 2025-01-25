using PlayerSystem;
using Combats;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerCombat : Combat
    {
        [SerializeField] private Player _player;

        private Coroutine _attackCoroutine;

        public void FixedUpdate()
        {
            if (!_player?.Controller) return;
            var attackDirection = _player.Controller.AttackDirection;
            var isattacking = attackDirection != Vector3.zero;
            if (isattacking && _attackCoroutine == null) TryAttack(attackDirection);
        }

        public override void Update()
        {
            if (!_player?.Controller) return;
            var attackDirection = _player.Controller.AttackDirection;

            if (attackDirection != Vector3.zero)
                _rigidbody.rotation = Quaternion.Lerp(
                    _rigidbody.rotation,
                    Quaternion.LookRotation(attackDirection),
                    Time.fixedDeltaTime * 10f
                );
        }
    }
}