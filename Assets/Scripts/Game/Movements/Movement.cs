using System;
using UnityEngine;

namespace Dungeons.Game.Movements
{
    public class Movement : MonoBehaviour
    {
        public event Action Move;
        public event Action SpeedChange;
        public event Action Spawn;
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _idleThreshold;
        [SerializeField] protected float _stopDistance = 0.5f;

        protected float _idleTimer;
        protected bool _isFunnyIdle;
        protected Vector3 _moveDirection;
        protected Vector3 _targetPosition;

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                SpeedChange?.Invoke();
            }
        }

        private void Start()
        {
            Spawn?.Invoke();
            Speed = _speed;
        }

        public void Update()
        {
            var isMoving = _moveDirection != Vector3.zero;
            Move?.Invoke();
            if (isMoving)
            {
                _idleTimer = 0f;
                _isFunnyIdle = false;
                //_animator.SetBool("FunnyIdle", _isFunnyIdle);
                
                var targetRotation = Quaternion.LookRotation(_moveDirection);
                _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, Time.deltaTime * 10f);
            }
            else
            {
                _idleTimer += Time.deltaTime;
                if (_idleTimer >= _idleThreshold)
                {
                    _isFunnyIdle = !_isFunnyIdle;
                    //_animator.SetBool("FunnyIdle", _isFunnyIdle);
                    _idleTimer = 0;
                }
            }

            if (_targetPosition != Vector3.zero)
            {
                var distanceToTarget = Vector3.Distance(transform.position, _targetPosition);
                if (distanceToTarget <= _stopDistance) StopMoving();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero)
            {
                var newPosition = _rigidbody.position + _moveDirection * (Speed * Time.fixedDeltaTime);
                _rigidbody.MovePosition(newPosition);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            StopMoving();
        }

        private void OnValidate()
        {
            Speed = _speed;
        }

        public void GoToPoint(Vector3 point)
        {
            _targetPosition = point;
            _moveDirection = (point - transform.position).normalized;
        }

        public void GoToEntity(Transform entity)
        {
            GoToPoint(entity.position);
        }

        public bool IsMoving()
        {
            return _rigidbody.linearVelocity.magnitude > 0.01f;
        }

        public void StopMoving()
        {
            _targetPosition = Vector3.zero;
            _moveDirection = Vector3.zero;
            Move?.Invoke();
        }
    }
}