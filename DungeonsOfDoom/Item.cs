using System;

namespace DungeonsOfDoom
{
    abstract class Item//Jag anser att denna kan vara abstrakt -- Christian
    {
        public Item(string name)
        {
            Name = name;
        }
        public Item()
        {

        }

        public string Name { get; set; } //Item name kanske kan skapas av en metod som tar in rarity där det finns, ex "Epic Sword"? - Nima
        // Vi kan specifiera det i SubClassen annars - Christian

        //public bool Consumable => Type == "Consumable";
        public string Type  => this.GetType().ToString().Split('.')[1];
        public int Count { get; set; } = 1;

        
    }

    //La till SubClasses för Weapon: Axe, Sword, Spear och gav dem förutom namn Power också. - Martin
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
        public int Power { get; set; }
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

        }
    }


   
}
