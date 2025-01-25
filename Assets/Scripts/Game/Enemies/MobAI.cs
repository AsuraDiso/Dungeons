using Dungeons.Game.Combats;
using Dungeons.Game.InventorySystem;
using Dungeons.Game.Movements;
using Dungeons.Game.PlayerSystem;
using UnityEngine;

namespace Dungeons.Game.Enemies
{
    public abstract class MobAI : MonoBehaviour
    {
        [SerializeField] protected Health.Health _health;
        [SerializeField] protected Combat _combat;
        [SerializeField] protected Movement _movement;
        [SerializeField] protected Inventory _inventory;

        public Player Player { get; set; }

        protected bool IsDead()
        {
            return _health.IsDead();
        }
    }
}