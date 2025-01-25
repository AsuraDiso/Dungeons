using Dungeons.Game.Enemies.Behaviours;
using UnityEngine;

namespace Dungeons.Game.Enemies
{
    public class WarriorAI : MobAI
    {
        [SerializeField] private Health.Health _minionPrefab;
        [SerializeField] private float _followDist;
        private ChaseAndAttackAndAvoidAI _chaseAndAttackAndAvoidAI;
        private WanderAI _wanderAI;

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