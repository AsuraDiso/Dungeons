using Dungeons.Game.Movements;
using UnityEngine;

namespace Dungeons.Game.Enemies.Behaviours
{
    public class FollowerAI
    {
        private readonly Transform _leaderTransform;
        private readonly Movement _movement;
        private readonly Transform _ownerTransform;

        public FollowerAI(Transform ownerTransform, Movement movement, Transform leaderTransform)
        {
            _ownerTransform = ownerTransform;
            _movement = movement;
            _leaderTransform = leaderTransform;
        }

        public void Update()
        {
            if (!_leaderTransform || !_ownerTransform) return;

            var distanceToLeader = Vector3.Distance(_ownerTransform.position, _leaderTransform.position);
            if (distanceToLeader > 3f) _movement.GoToPoint(_leaderTransform.position);
        }
    }
}