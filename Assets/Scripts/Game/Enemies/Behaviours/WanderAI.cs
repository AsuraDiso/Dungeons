using Movements;
using UnityEngine;

namespace Enemies.Behaviours
{
    public class WanderAI
    {
        private Transform _ownerTransform;
        private Movement _movement;
        private float _wanderRadius;
        private float _stayTime;
        private float _stayTimer;
        private Vector3 _targetPosition;
        private Vector3 _spawnPosition;

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