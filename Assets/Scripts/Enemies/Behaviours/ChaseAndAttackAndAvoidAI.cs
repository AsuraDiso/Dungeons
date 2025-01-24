using Combats;
using Movements;
using PlayerSystem;
using UnityEngine;

namespace Enemies.Behaviours
{
    public class ChaseAndAttackAndAvoidAI
    {
        private Player _player;
        private Transform _ownerTransform;
        private Combat _combat;
        private Movement _movement;
        private float _followRange;

        private bool _isShouldAvoid;
        private bool _isAboutToAvoid;
        private float _avoidanceDistance = 5f;
        private float _stunDuration = 1.0f; 
        private float _stunTimer;

        public ChaseAndAttackAndAvoidAI(Transform ownerTransform, Player player, Combat combat, Movement movement, float followRange)
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

            if (_isAboutToAvoid)
            {
                _stunTimer -= Time.deltaTime;
                if (_stunTimer <= 0)
                {
                    _isAboutToAvoid = false;
                    _isShouldAvoid = true;
                }
                return true; 
            }

            float distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
            if (distanceToPlayer < _followRange)
            {
                if (_isShouldAvoid)
                {
                    RunAway();
                    _combat.Target = null;
                }
                else
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
            }

            return true;
        }

        private void ChasePlayer()
        {
            _movement.GoToEntity(_player.transform);
        }

        private void RunAway()
        {
            Vector3 directionAway = (_ownerTransform.position - _player.transform.position).normalized;
            Vector3 runToPosition = _ownerTransform.position + directionAway * _avoidanceDistance;

            _movement.GoToPoint(runToPosition);

            float distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
            if (distanceToPlayer >= _avoidanceDistance)
            {
                _isShouldAvoid = false;
            }
        }

        private void AttackPlayer()
        {
            Vector3 direction = (_player.transform.position - _ownerTransform.position).normalized;
            _combat.TryAttack(direction);
            StartStun();
            _combat.Target = _player.transform;
        }

        private void StartStun()
        {
            _isAboutToAvoid = true;
            _stunTimer = _stunDuration;
        }
    }
}
