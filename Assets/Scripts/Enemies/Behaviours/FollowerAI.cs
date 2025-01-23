using Movements;
using UnityEngine;

namespace Enemies.Behaviours
{
    public class FollowerAI
    
    {
        private Transform _ownerTransform;
        private Transform _leaderTransform;
        private Movement _movement;
        
        public FollowerAI(Transform ownerTransform, Movement movement, Transform leaderTransform)
        {
            _ownerTransform = ownerTransform;
            _movement = movement;
            _leaderTransform = leaderTransform;

        }

        public void Update()
        {
            if (!_leaderTransform || !_ownerTransform) return;

            float distanceToLeader = Vector3.Distance(_ownerTransform.position, _leaderTransform.position);
            if (distanceToLeader > 3f)
            {
                _movement.GoToPoint(_leaderTransform.position);
            }
        }
    }
}