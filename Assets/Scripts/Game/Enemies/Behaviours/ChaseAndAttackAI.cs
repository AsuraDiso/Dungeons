using Dungeons.Game.Combats;
using Dungeons.Game.Movements;
using Dungeons.Game.PlayerSystem;
using UnityEngine;

namespace Dungeons.Game.Enemies.Behaviours
{
    public class ChaseAndAttackAI
    {
        private readonly Combat _combat;
        private readonly float _followRange;
        private readonly Movement _movement;
        private readonly Transform _ownerTransform;
        private readonly Player _player;

        public ChaseAndAttackAI(Transform ownerTransform, Player player, Combat combat, Movement movement,
            float followRange)
        {
            _ownerTransform = ownerTransform;
            _player = player;
            _combat = combat;
            _movement = movement;
            _followRange = followRange;
        }

        public bool Update()
        {
            if (!_player || !_ownerTransform) return false;
            if (_player.Health.IsDead()) return false;

            var distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
            if (distanceToPlayer < _followRange)
            {
                if (distanceToPlayer < _combat.GetAttackRange())
                {
                    _movement.StopMoving();
                    AttackPlayer();
                }
                else
                {
                    _combat.Target = null;
                    ChasePlayer();
                }
            }

            return true;
        }

        private void ChasePlayer()
        {
            _movement.GoToEntity(_player.transform);
        }

        private void AttackPlayer()
        {
            var direction = (_player.transform.position - _ownerTransform.position).normalized;
            _combat.TryAttack(direction);
            _combat.Target = _player.transform;
        }
    }
}