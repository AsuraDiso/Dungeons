using Combats;
using InventorySystem;
using Movements;
using UnityEngine;

namespace Enemies
{
    public abstract class MobAI : MonoBehaviour
    {
        private Health _player;
        [SerializeField] protected Health _health;
        [SerializeField] protected Combat _combat;
        [SerializeField] protected Movement _movement;
        [SerializeField] protected Inventory _inventory;

        protected bool IsDead()
        {
            return _health.IsDead();
        }

        public Health Player
        {
            get => _player;
            set => _player = value;
        }
    }
}