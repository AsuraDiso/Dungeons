using Dungeons.Game.InventorySystem;
using UnityEngine;

namespace Dungeons.Game.Items
{
    public enum EquipSlot
    {
        None,
        Head,
        Body,
        Hand
    }

    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private Collider _collider;

        [SerializeField] private Vector3 _defaultOverrideEquipPosition = Vector3.zero;
        [SerializeField] private Vector3 _defaultOverrideGroundRotation = new(0, 0, 45);
        private EquipSlot _equipSlot;
        private bool _isEquipped;

        public bool IsEquipped { get; set; }
        public Vector3 OverrideEquipPosition { get; set; }
        public Quaternion OverrideGroundRotation { get; set; }
        public EquipSlot EquipSlot { get; set; }

        private void Awake()
        {
            UnEquip();
        }

        private void Update()
        {
            if (!IsEquipped) transform.rotation *= Quaternion.Euler(0, 45 * Time.deltaTime, 0);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            var inventory = other.GetComponentInParent<Inventory>();
            if (inventory != null) inventory.EquipItem(this);
        }

        public void Equip()
        {
            _collider.enabled = false;
            IsEquipped = true;
            transform.rotation *= OverrideGroundRotation;
        }

        public void UnEquip()
        {
            _collider.enabled = true;
            IsEquipped = false;
            transform.rotation = OverrideGroundRotation;
        }
    }
}