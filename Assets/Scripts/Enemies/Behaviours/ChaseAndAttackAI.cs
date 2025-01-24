using Combats;
using Movements;
using PlayerSystem;
using UnityEngine;

namespace Enemies.Behaviours
{
    public class ChaseAndAttackAI
    {
        private Player _player;
        private Transform _ownerTransform;
        private Combat _combat;
        private Movement _movement;
        private float _followRange;

        public ChaseAndAttackAI(Transform ownerTransform, Player player, Combat combat, Movement movement, float followRange)
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
            
            float distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
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
            Vector3 direction = (_player.transform.position - _ownerTransform.position).normalized;
            _combat.TryAttack(direction);
            _combat.Target = _player.transform;
        }
    }
}