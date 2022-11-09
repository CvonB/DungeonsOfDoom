using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract internal class LivingEntity
    {
        public int Health { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor EntityColor { get; set; }
        public int Power { get; set; }
        public string Name { get; set; }



        public int Attack(LivingEntity opponent)
        {
            int damage = Power;
            if (IsAlive)
                opponent.Health -= damage;
            return damage;
        }

    }

    enum ArmorTypes
    {
        Unarmored = 0,
        Light = 1,
        Medium = 2,
        Heavy = 3,
        Etheral = 4,
    }
    enum Rarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Epic = 3,
        Legendary = 4,
    }
}
