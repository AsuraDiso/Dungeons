using Enemies.Behaviours;
using UnityEngine;

namespace Enemies
{
    public class GolemAI : MobAI
    {
        private WanderAI _wanderAI;
        private ChaseAndAttackAndAvoidAI _chaseAndAttackAndAvoidAI;

        private void Start()
        {
            _wanderAI = new WanderAI(transform, _movement, 5f, 3f);
            _chaseAndAttackAndAvoidAI = new ChaseAndAttackAndAvoidAI(transform, Player, _combat, _movement, 1.5f);
        }

        private void Update()
        {
            if (IsDead()) return;
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
            
            if (distanceToPlayer < 5f)
            {
                _chaseAndAttackAndAvoidAI.Update();
            }
            else
            {
                _wanderAI.Update();
            }
        }
    }
}