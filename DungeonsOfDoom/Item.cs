using System;

namespace DungeonsOfDoom
{
    //TODO: Konvertera Item till BaseClass och introducera minst 2 SubClasser
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

        public string Name { get; set; }
        public bool Consumable => Type == "Consumable";
        public string Type { get; set; }
    }

        enum Items
        {
            Sword = 0,
            Axe = 1,
            Spear = 2,
            Pear = 3,
            Apple = 4,
        }

}
