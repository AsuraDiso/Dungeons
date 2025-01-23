using UnityEngine;

namespace Items
{
    public class Hat : Item
    {
        public int defenseValue; 
        public bool isHelmet; 
    
        private void Awake()
        {
            EquipSlot = EquipSlot.Head;
            OverrideGroundRotation = Quaternion.Euler(0, 0, 0);
            if (!isHelmet)
            {
                OverrideEquipPosition = new Vector3(0f, 0.55f, 0f); 
            }
        }
    }
}