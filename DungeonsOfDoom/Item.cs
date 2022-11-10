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
        /// <summary>
        /// Returns SubClass to string.
        /// </summary>
        public string Type  => this.GetType().ToString().Split('.')[1];
        /// <summary>
        /// Returns how many of said Item is in Inventory. Only applicable if Stackable == true.
        /// </summary>
        public int Count { get; set; } = 1;

        public Rarity Rare { get; set; }
        /// <summary>
        /// Allows item to be stacked on top of each other.
        /// </summary>
        public bool Stackable { get; set; }
        /// <summary>
        /// Returns the power of the Item. If weapon, will deal said amount as damage. If armor, will remove said amount from damage
        /// If consumable, will affect player by said amount. Ex heal player by power.
        /// </summary>
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

    
    class Unarmed : Weapon
    {
        public Unarmed() : base("Your fists", 10)
        {

            StrongAgainst = ArmorTypes.Nothing;
        }

        public Unarmed(int power) : base("Your fists", power)
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
            Power = 0;
            ArmorType = type;
        }

        public ArmorTypes ArmorType { get; set; }
    }

    class Unarmored : Armor    {
        public Unarmored() : base("Heavy Mail", ArmorTypes.Unarmored)
        {
            
        }
    }
    class HeavyMail : Armor    {
        public HeavyMail() : base("Heavy Mail", ArmorTypes.Heavy)
        {
            Power = 3;
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
