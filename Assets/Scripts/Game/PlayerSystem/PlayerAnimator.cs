using UnityEngine;

namespace Dungeons.Game.PlayerSystem
{
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int RunSpeed = Animator.StringToHash("RunSpeed");
        private static readonly int Spawn = Animator.StringToHash("Spawn");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _player.Health.Death += OnDeath;
            _player.Combat.Hit += OnHit;
            _player.Movement.Move += OnMove;
            _player.Movement.Spawn += OnSpawn;
            _player.Movement.SpeedChange += OnSpeedChange;
        }

        private void OnSpeedChange()
        {
            _animator.SetFloat(RunSpeed, _player.Movement.Speed);
        }

        private void OnSpawn()
        {
            _animator.SetTrigger(Spawn);
        }

        private void OnMove()
        {
            _animator.SetBool(IsRunning, _player.Movement.IsMoving());
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