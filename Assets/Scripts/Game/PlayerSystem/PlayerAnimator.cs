using UnityEngine;

namespace Dungeons.Game.PlayerSystem
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _player.Health.Death += OnDeath;
            _player.Combat.Hit += OnHit;
        }

        private void OnDeath()
        {
            _animator.SetTrigger(Death);
        }

        public void OnHit()
        {
            _animator.SetTrigger(Hit);
        }
    }
}