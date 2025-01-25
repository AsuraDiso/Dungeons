namespace Dungeons.Game.Items
{
    public class Armor : Item
    {
        public int defenseValue;

        private void Awake()
        {
            EquipSlot = EquipSlot.Body;
        }
    }
}