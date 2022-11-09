using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    internal class LivingEntity
    {
        public int Health { get; set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor EntityColor { get; set; }
        
    }

    enum ArmorTypes
    {
        Unarmored = 0,
        Light = 1,
        Medium = 2,
        Heavy = 3,
    }
    enum Rarity
    {
        Common = 0,
        Rare = 1,
    }
}
