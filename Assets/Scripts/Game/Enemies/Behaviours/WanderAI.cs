using Dungeons.Game.Movements;
using UnityEngine;

namespace Dungeons.Game.Enemies.Behaviours
{
    public class WanderAI
    {
        private readonly Movement _movement;
        private readonly Transform _ownerTransform;
        private readonly Vector3 _spawnPosition;
        private readonly float _stayTime;
        private float _stayTimer;
        private Vector3 _targetPosition;
        private readonly float _wanderRadius;

        public WanderAI(Transform ownerTransform, Movement movement, float wanderRadius, float stayTime)
        {
            _ownerTransform = ownerTransform;
            _movement = movement;
            _wanderRadius = wanderRadius;
            _stayTime = stayTime;
            _stayTimer = 0f;

            _spawnPosition = _ownerTransform.position;
        }

        public bool Update()
        {
            if (_stayTimer <= 0f)
            {
                if (Vector3.Distance(_ownerTransform.position, _targetPosition) < 0.5f)
                    _stayTimer = _stayTime;
                else if (!_movement.IsMoving()) _movement.GoToPoint(_targetPosition);
            }
            else
            {
                _stayTimer -= Time.deltaTime;

                if (_stayTimer <= 0f)
                {
                    var randomCircle = Random.insideUnitCircle * _wanderRadius;
                    var randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);

                    _targetPosition = _spawnPosition + randomOffset;
                }
            }

            return true;
        }
    }
}