using Enemies.Behaviours;
using UnityEngine;

namespace Enemies
{
    public class MinionAI : MobAI
    {
        private WanderAI _wanderAI;
        private FollowerAI _followerAI;
        private ChaseAndAttackAI _chaseAndAttackAI;
        [SerializeField] private Transform _leaderTransform;

        public void SetLeader(Transform leaderTransform)
        {
            _leaderTransform = leaderTransform;
        }
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
            if (_leaderTransform)
            {
                distanceToLeader = Vector3.Distance(transform.position, _leaderTransform.position);
            }
            if (distanceToPlayer < 5f && (distanceToLeader == null || distanceToLeader < 7f))
            {
                _chaseAndAttackAI.Update();
            }
            else
            {
                if (!_leaderTransform)
                {
                    _followerAI.Update();
                }
                else
                {
                    _wanderAI.Update();
                }
            }
        }
    }
}