using InventorySystem;
using Movements;
using UnityEngine;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerCombat _combat;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private PlayerMovement _movement;

        public Health Health { get => _health;  set => _health = value; }
        public Inventory Inventory { get => _inventory; set => _inventory = value; }
        public PlayerCombat Combat { get => _combat; set => _combat = value; }
        public PlayerController Controller { get => _controller; set => _controller = value; }
        public PlayerMovement Movement { get => _movement; set => _movement = value; }
    }
}
