using Dungeons.Game.Combats;
using Dungeons.Game.Movements;
using Dungeons.Game.PlayerSystem;
using UnityEngine;

namespace Dungeons.Game.Enemies.Behaviours
{
    public class ChaseAndAttackAndAvoidAI
    {
        private readonly float _avoidanceDistance = 5f;
        private readonly Combat _combat;
        private readonly float _followRange;
        private bool _isAboutToAvoid;

        private bool _isShouldAvoid;
        private readonly Movement _movement;
        private readonly Transform _ownerTransform;
        private readonly Player _player;
        private readonly float _stunDuration = 1.0f;
        private float _stunTimer;

        public ChaseAndAttackAndAvoidAI(Transform ownerTransform, Player player, Combat combat, Movement movement,
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

            var distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
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
            var directionAway = (_ownerTransform.position - _player.transform.position).normalized;
            var runToPosition = _ownerTransform.position + directionAway * _avoidanceDistance;

            _movement.GoToPoint(runToPosition);

            var distanceToPlayer = Vector3.Distance(_ownerTransform.position, _player.transform.position);
            if (distanceToPlayer >= _avoidanceDistance) _isShouldAvoid = false;
        }

        private void AttackPlayer()
        {
            var direction = (_player.transform.position - _ownerTransform.position).normalized;
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