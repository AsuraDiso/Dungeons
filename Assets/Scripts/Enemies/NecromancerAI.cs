using System.Collections.Generic;
using Enemies.Behaviours;
using Items;
using UnityEngine;

namespace Enemies
{
    public class NecromancerAI : MobAI
    {
        private WanderAI _wanderAI;
        private ChaseAndAttackAndAvoidAI _chaseAndAttackAndAvoidAI;
        [SerializeField] private float _followDist; 
        [SerializeField] private int _stages; 
        [SerializeField] private Health _minionPrefab; 
        [SerializeField] private int _minionsAmount; 
        [SerializeField] private Weapon _weaponStage2; 
        [SerializeField] private Weapon _weaponStage3; 
        [SerializeField] private Weapon _weaponStage4; 
        
        private int _currentStage = 1;
        private List<Health> _minions = new();
        private void Start()
        {
            _wanderAI = new WanderAI(transform, _movement, 5f, 3f);
            _chaseAndAttackAndAvoidAI = new ChaseAndAttackAndAvoidAI(transform, Player, _combat, _movement, _followDist);
        }

        private void SpawnMinions()
        {
            var theta = Mathf.PI * 2f;
            var radius = 4;
            for (int i = 0; i < _minionsAmount; i++)
            {
                var position = new Vector3(Mathf.Cos(theta)*radius, Mathf.Sin(theta)*radius, 0f);
                var minion = Instantiate(_minionPrefab, transform.position+position, Quaternion.identity);
                _minions.Add(minion);
                var minionAI = minion.GetComponent<MinionAI>();
                minionAI.SetLeader(transform);
                minionAI.SetPlayer(Player);
            }
        }
        private void Update()
        {
            if (IsDead()) return;
            var stage = Mathf.Approximately(_health.GetPercent(), 1) ? 1 : Mathf.CeilToInt(_stages - _health.GetPercent() * _stages);
            if (_currentStage != stage)
            {
                Debug.Log($"Stage {_currentStage}: {stage}");
                if (stage == 2)
                    _inventory.EquipItem(Instantiate(_weaponStage2));
                else if (stage == 3)
                {
                    _inventory.EquipItem(Instantiate(_weaponStage3));
                } else if (stage == 4)
                {
                    _inventory.EquipItem(Instantiate(_weaponStage4));
                }
            }
            if (stage == 2)
            {
                _movement.Speed = 6;
            } else if (stage == 3)
            {
                if (_minions.Count < _minionsAmount) 
                    SpawnMinions();
            } else if (stage == 4)
            {
                
            }
            if (_chaseAndAttackAndAvoidAI.Update()) return;
            if (_wanderAI.Update())
                _combat.Target = null;
            _currentStage = stage;
        }
    }
}