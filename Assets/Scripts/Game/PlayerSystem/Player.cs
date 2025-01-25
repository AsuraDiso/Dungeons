using InventorySystem;
using UnityEngine;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public Inventory Inventory { get; private set; }
        [field: SerializeField] public PlayerCombat Combat { get; private set; }
        [field: SerializeField] public PlayerController Controller { get; private set; }
        [field: SerializeField] public PlayerMovement Movement { get; private set; }
    }
}