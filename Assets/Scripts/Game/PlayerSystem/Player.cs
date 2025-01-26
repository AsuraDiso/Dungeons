using System;
using Dungeons.Game.InventorySystem;
using UnityEngine;

namespace Dungeons.Game.PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Health.Health Health { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public PlayerCombat Combat { get; private set; }
        [field: SerializeField] public PlayerController Controller { get; private set; }
        [field: SerializeField] public PlayerMovement Movement { get; private set; }

        private void Start()
        {
            name = nameof(Player);
        }

        public void MoveToZeros() => transform.position = Vector3.zero;
    }
}