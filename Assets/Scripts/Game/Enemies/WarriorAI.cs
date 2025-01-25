using Enemies.Behaviours;
using UnityEngine;

namespace Enemies
{
    public class WarriorAI : MobAI
    {
        private WanderAI _wanderAI;
        private ChaseAndAttackAndAvoidAI _chaseAndAttackAndAvoidAI;
        [SerializeField] private Health _minionPrefab;
        [SerializeField] private float _followDist;

        private void Start()
        {
            _wanderAI = new WanderAI(transform, _movement, 5f, 3f);
            _chaseAndAttackAndAvoidAI =
                new ChaseAndAttackAndAvoidAI(transform, Player, _combat, _movement, _followDist);
        }

        private void Update()
        {
            if (IsDead()) return;
            if (_chaseAndAttackAndAvoidAI.Update()) return;
            if (_wanderAI.Update())
                _combat.Target = null;
        }
    }
}