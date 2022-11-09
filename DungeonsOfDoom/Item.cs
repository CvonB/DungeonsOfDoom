using System;

namespace DungeonsOfDoom
{
    abstract class Item
    {
        public Item(string name)
        {
            Name = name;
            Rare = Rarity.Common;
        }

        public Item(string name, Rarity rare)
        {
            Name = name;
            Rare = rare;
        }

        public string Name { get; set; } 

        //public bool Consumable => Type == "Consumable";
        public string Type  => this.GetType().ToString().Split('.')[1];
        public int Count { get; set; } = 1;

        public Rarity Rare { get; set; }
        public bool Stackable { get; set; }
        public int Power { get; set; }
    }
    #region Weapons
    abstract class Weapon : Item
    {

        public Weapon(string weaponType, int power) : base(weaponType)
        {
            Power = power;
        }
        public Weapon(string weaponType) : base(weaponType)
        {
            Power = 1;
        }

        // if Armor = StrongAgainst > Damage = 1.5 else Damage 1
        // rand 0-100.
        // if (crit > rand) Damage *= 2
        // Health - power*Damage

        public ArmorTypes StrongAgainst { get; set; }
    }

    
    class Unarmored : Weapon
    {
        public Unarmored() : base("Your fists", 10)
        {

            StrongAgainst = ArmorTypes.Nothing;
        }

        public Unarmored(int power) : base("Your fists", power)
        {
            StrongAgainst = ArmorTypes.Nothing;

        }
    }

    class Sword : Weapon
    {
        public Sword() : base("Sword", 20)
        {
            StrongAgainst = ArmorTypes.Unarmored;
        }

        public Sword(int power) : base("Sword", power)
        {

        }
    }

    class Axe : Weapon
    {
        public Axe(int power) : base("Axe", power)
        {

        }        
        public Axe() : base("Axe", 15)
        {

        }
    }

    class Spear : Weapon
    {
        public Spear() : base("Regular Spear", 10)
        {

        }
        public Spear(int power) : base("Spear", power)
        {

        }
    }
    #endregion

    #region Armor
    class Armor : Item
    {
        public Armor(string armorName, ArmorTypes type) : base(armorName)
        {
            Power = 1;
            ArmorType = type;
        }

        public ArmorTypes ArmorType { get; set; }
    }

    class HeavyMail : Armor    {
        public HeavyMail() : base("Heavy Mail", ArmorTypes.Heavy)
        {

        }
    }
    #endregion

    #region Consumables
    class Consumable : Item
    {
        public Consumable() : base("Health Potion")
        {
            Stackable = true;
            Power = 5;
        }
    }
    #endregion
}
