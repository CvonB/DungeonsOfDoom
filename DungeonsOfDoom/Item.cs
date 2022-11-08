using System;

namespace DungeonsOfDoom
{
    class Item
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
