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

    abstract class Weapon : Item
    {

        public Weapon(string weaponType, int power) : base(weaponType)
        {
            Power = power;
            
            
        }
        // if Armor = StrongAgainst > Damage = 1.5 else Damage 1
        // rand 0-100.
        // if (crit > rand) Damage *= 2
        // Health - power*Damage
        public ArmorTypes StrongAgainst { get; set; }
    }

    class Sword : Weapon
    {
        public Sword() : base("Sword", 20)
        {
            StrongAgainst = ArmorTypes.Unarmored;
        }
    }

    class Axe : Weapon
    {
        public Axe() : base("Axe", 15)
        {

        }
    }

    class Spear : Weapon
    {
        public Spear() : base("Super Spear", 10)
        {

        }
    }

    class Consumable : Item
    {
        public Consumable() : base("Health Potion")
        {
            Stackable = true;
            Power = 5;
        }
    }
}
