using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    abstract internal class LivingEntity
    {
        virtual public int Health { get; set; }
        public List<ICarryable> Inventory { get; set; } = new List<ICarryable>();
        public bool IsAlive { get { return Health > 0; } }
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor EntityColor { get; set; }
        public int Power {get; set; }
        public virtual string Name { get; set; }
        public int CritChance { get; set; }
        /// <summary>
        /// Current item affecting entity damage.
        /// </summary>
        public Weapon EquippedWeapon { get; set; }
        /// <summary>
        /// Current item reducing taken damage by entity.
        /// </summary>
        public Armor EquippedArmor { get; set; }

        /// <summary>
        /// Makes entity attack a designated opponent. Damage is affected by entity's equiped weapon
        /// as well as opponent's equiped armor. Further affected by attacking entity's CritChange property.
        /// </summary>
        /// <param name="opponent"></param>
        /// <returns></returns>
        public int Attack(LivingEntity opponent)
        {
            double damage = Power + EquippedWeapon.Power; //Double för att kunna utföra matematiska beräkningar
            if (EquippedWeapon.StrongAgainst == opponent.EquippedArmor.ArmorType)
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
