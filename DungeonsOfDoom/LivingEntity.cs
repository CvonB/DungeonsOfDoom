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
}
