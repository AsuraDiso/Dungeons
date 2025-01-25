using Dungeons.Game.Enemies.Behaviours;
using UnityEngine;

namespace Dungeons.Game.Enemies
{
    public class MageAI : MobAI
    {
        [SerializeField] private float _followDist;
        private ChaseAndAttackAI _chaseAndAttackAI;
        private WanderAI _wanderAI;

        private void Start()
        {
            _wanderAI = new WanderAI(transform, _movement, 5f, 3f);
            _chaseAndAttackAI = new ChaseAndAttackAI(transform, Player, _combat, _movement, _followDist);
        }

        private void Update()
        {
            if (IsDead()) return;
            if (_chaseAndAttackAI.Update()) return;
            if (_wanderAI.Update())
                _combat.Target = null;
        }
    }
}