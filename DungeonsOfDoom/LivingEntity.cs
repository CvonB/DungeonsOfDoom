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
        public int Power => EquipedWeapon.Power;
        public string Name { get; set; }
        public int CritChance { get; set; }
        public Weapon EquipedWeapon { get; set; }
        public Armor EquipedArmor { get; set; }

        public int Attack(LivingEntity opponent)
        {
            double damage = Power - opponent.EquipedArmor.Power; //Double för att kunna utföra matematiska beräkningar
            if (EquipedWeapon.StrongAgainst == opponent.EquipedArmor.ArmorType)
                damage *= 1.5;
            if(Random.Shared.Next(0,100) < CritChance)
                damage *= 2;
            if (IsAlive)
                opponent.Health -= (int)Math.Round(damage); //konvererar sedan tillbaka till en int
            return (int)Math.Round(damage);
        }
    }

    enum ArmorTypes
    {
        Unarmored = 0,
        Light = 1,
        Medium = 2,
        Heavy = 3,
        Etheral = 4,
        Nothing = 5,
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
