using UnityEngine;
using System.Collections.Generic;
using Items;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Item _headSerialized;
        [SerializeField] private Item _bodySerialized;
        [SerializeField] private Item _handSerialized;
        private Dictionary<EquipSlot, Item> _slots = new();

        [SerializeField] private List<Item> _inventoryItems = new();

        [SerializeField] private Animator _animator;

        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Transform _rightHandTransform;
        [SerializeField] private Transform _bodyTransform;
        [SerializeField] private Transform _headTransform;

        private void Awake()
        {
            _slots[EquipSlot.Head] = null;
            _slots[EquipSlot.Body] = null;
            _slots[EquipSlot.Hand] = null;

            if (_headSerialized != null)
                EquipItem(Instantiate(_headSerialized));
            if (_bodySerialized != null)
                EquipItem(Instantiate(_bodySerialized));
            if (_handSerialized != null)
                EquipItem(Instantiate(_handSerialized));
        }

        public void AddItem(Item item)
        {
            _inventoryItems.Add(item);
        }

        public void EquipItem(Item item)
        {
            if (!item) return;
            if (CanEquip(item))
            {
                UnequipItem(item.EquipSlot);
                _slots[item.EquipSlot] = item;

                var targetTransform = GetSlotTransform(item.EquipSlot);
                if (targetTransform) AttachItemToTransform(item, targetTransform);

                item.Equip();
            }
        }


        public void UnequipItem(EquipSlot slot)
        {
            if (_slots.ContainsKey(slot) && _slots[slot])
            {
                DetachItemFromTransform(_slots[slot]);
                _slots[slot].UnEquip();
                _slots[slot] = null;
            }
        }

        private bool CanEquip(Item item)
        {
            return !item.IsEquipped;
        }

        private Transform GetSlotTransform(EquipSlot slot)
        {
            return slot switch
            {
                EquipSlot.Hand => _rightHandTransform,
                EquipSlot.Body => _bodyTransform,
                EquipSlot.Head => _headTransform,
                _ => null
            };
        }

        private void AttachItemToTransform(Item item, Transform slotTransform)
        {
            if (item && slotTransform)
            {
                item.transform.SetParent(slotTransform);
                item.transform.localPosition = Vector3.zero;
                item.transform.localPosition += item.OverrideEquipPosition;
                item.transform.localRotation = Quaternion.identity;
            }
        }

        private void DetachItemFromTransform(Item item)
        {
            if (item && item.transform.parent) item.transform.SetParent(null);
        }

        public Item GetItemInSlot(EquipSlot slot)
        {
            if (_slots.ContainsKey(slot)) return _slots[slot];

            return null;
        }
    }
}