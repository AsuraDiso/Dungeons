using UnityEngine;

namespace Items
{
    public enum WeaponType
    {
        Melee,
        DualMelee,
        TwoHandedMelee,
        Ranged,
        Magic
    }

    public class Weapon : Item
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private int _damage;
        [SerializeField] private int _range;
        [SerializeField] private float _cooldown;
        [SerializeField] private Projectile _projectile;

        public WeaponType WeaponType => _weaponType;
        public Projectile Projectile => _projectile;
        public int Damage => _damage;
        public int Range => _range;
        public float CoolDown => _cooldown;

        private void Awake()
        {
            EquipSlot = EquipSlot.Hand;
        }
    }
}