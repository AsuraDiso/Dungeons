using System;
using InventorySystem;
using UnityEngine;

namespace Items
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
        private EquipSlot _equipSlot;
        [SerializeField] private Collider _collider;

        [SerializeField] private Vector3 _defaultOverrideEquipPosition = Vector3.zero;
        [SerializeField] private Vector3 _defaultOverrideGroundRotation = new(0, 0, 45);
        private bool _isEquipped;

        public bool IsEquipped { get; set; }
        public Vector3 OverrideEquipPosition { get; set; }
        public Quaternion OverrideGroundRotation { get; set; }
        public EquipSlot EquipSlot { get; set; }

        private void Start() => UnEquip();

        public virtual void Equip()
        {
            _collider.enabled = false;
            IsEquipped = true;
            transform.rotation *= OverrideGroundRotation;
        }

        public virtual void UnEquip()
        {
            _collider.enabled = true; 
                IsEquipped = false;
            transform.rotation = OverrideGroundRotation;
        }

        private void Update()
        {
            if (!IsEquipped)
            {
                transform.rotation *= Quaternion.Euler(0, 45 * Time.deltaTime, 0);
            }
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            var inventory = other.GetComponentInParent<Inventory>();
            if (inventory != null)
            {
                inventory.EquipItem(this);
            }
        }
    }
}
