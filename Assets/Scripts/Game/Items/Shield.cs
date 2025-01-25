namespace Dungeons.Game.Items
{
    public class Shield : Item
    {
        public int defenseValue;

        private void Awake()
        {
            EquipSlot = EquipSlot.Body;
        }
    }
}