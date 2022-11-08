using System;

namespace DungeonsOfDoom
{
    class Item//Jag anser att denna kan vara abstrakt -- Christian
    {
        public Item(string name)
        {
            Name = name;
        }
        public Item()
        {
            var namesCount = Enum.GetNames(typeof(Items)).Length;
            int rand = new Random().Next(0, namesCount);
            Name = ((Items)rand).ToString();
        }

        public string Name { get; set; } //Item name kanske kan skapas av en metod som tar in rarity där det finns, ex "Epic Sword"? - Nima
        // Vi kan specifiera det i SubClassen annars - Christian

        //public bool Consumable => Type == "Consumable";
        public string Type  => this.GetType().ToString().Split('.')[1];
        public int Count { get; set; }
    }

    class Weapon : Item
    {
        public Weapon() : base("Axe")
        {

        }
    }

    class Consumable : Item
    {
        public Consumable() : base("Health Potion")
        {

        }
    }


    //TODO: Konvertera denna enum till en SubClass istället. Redan gjort men lämnar för att visa
    enum Items 
        {
            Sword = 0,
            Axe = 1,
            Spear = 2,
            Pear = 3,
            Apple = 4,
        }

}
