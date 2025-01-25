using Dungeons.Game.Enemies.Behaviours;
using UnityEngine;

namespace Dungeons.Game.Enemies
{
    public class MinionAI : MobAI
    {
        [SerializeField] private Transform _leaderTransform;
        private ChaseAndAttackAI _chaseAndAttackAI;
        private FollowerAI _followerAI;
        private WanderAI _wanderAI;

        private void Start()
        {
            _wanderAI = new WanderAI(transform, _movement, 5f, 3f);
            _followerAI = new FollowerAI(transform, _movement, _leaderTransform);
            _chaseAndAttackAI = new ChaseAndAttackAI(transform, Player, _combat, _movement, 1.5f);
        }

        private void Update()
        {
            if (IsDead()) return;
            var distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            float? distanceToLeader = null;
            if (_leaderTransform) distanceToLeader = Vector3.Distance(transform.position, _leaderTransform.position);
            if (distanceToPlayer < 5f && (distanceToLeader == null || distanceToLeader < 7f))
            {
                _chaseAndAttackAI.Update();
            }
            else
            {
                if (!_leaderTransform)
                    _followerAI.Update();
                else
                    _wanderAI.Update();
            }
        }

        public void SetLeader(Transform leaderTransform)
        {
            _leaderTransform = leaderTransform;
        }
    }
}