using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public static class RandomUtils
    {
        /// <summary>
        /// Returns a random enum in Rarity.
        /// </summary>
        /// <returns></returns>
        public static Rarity RandomRarity()
        {
            switch (Random.Shared.Next(0,100))
            {
                case < 6:
                    return Rarity.Legendary;
                case < 18:
                    return Rarity.Epic;
                case < 40:
                    return Rarity.Rare;
                case < 70:
                    return Rarity.Uncommon;
                default:
                    return Rarity.Common;
            }
        }

        /// <summary>
        /// Returns a random enum in ArmorTypes.
        /// </summary>
        /// <returns></returns>
        public static ArmorTypes RandomArmor()
        {
            int tmp = Random.Shared.Next(0,Enum.GetNames(typeof(ArmorTypes)).Length);
            return (ArmorTypes)tmp;
        }

        /// <summary>
        /// Returns the consoleColor of given rarity
        /// </summary>
        /// <param name="rarity"></param>
        /// <returns></returns>
        public static ConsoleColor RarityColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Uncommon:
                    return ConsoleColor.Green;
                case Rarity.Rare:
                    return ConsoleColor.Blue;
                case Rarity.Epic:
                    return ConsoleColor.Magenta;
                case Rarity.Legendary:
                    return ConsoleColor.Red;

                    default:
                    return ConsoleColor.White;
            }

        }

        /// <summary>
        /// Creates random int between 0 and func: tableOfItems length. Then calls func:tableOfItems and manipulates returned item instance. 
        /// </summary>
        /// <returns></returns>
        public static Item RandomItem()
        {
            int rand;
            switch (Random.Shared.Next(0,100))
            {
                case < 30:
                    rand = new Random().Next(0, tableOfWeapons.Length);
                    return tableOfWeapons[rand]();
                case < 60:
                    rand = new Random().Next(0, tableOfArmors.Length);
                    return tableOfArmors[rand]();
                default:
                    rand = new Random().Next(0, tableOfConsumables.Length);
                    return tableOfConsumables[rand]();


            }
        }

        /// <summary>
        /// Returns a new Weapon instance by given index.
        /// </summary>
        private static Func<Item>[] tableOfWeapons =
        {
            () => new Mace(),
            () => new Sword(),
            () => new Spear()
        };
        /// <summary>
        /// Returns a new Weapon instance by given index.
        /// </summary>
        private static Func<Item>[] tableOfArmors =
        {
            () => new Armor("Mail plate",ArmorTypes.Heavy),
            () => new Armor("Heavy mail plate",ArmorTypes.Heavy,10),
            () => new Armor("Leather armor",ArmorTypes.Light),
            () => new Armor("Thick leather armor",ArmorTypes.Light,10),
            () => new Armor("Heavy chain mail",ArmorTypes.Medium,10),
            () => new Armor("Chain mail",ArmorTypes.Medium),
            () => new Armor("your skin but thicker",ArmorTypes.Unarmored,2),
        };
        /// <summary>
        /// Returns a new Weapon instance by given index.
        /// </summary>
        private static Func<Item>[] tableOfConsumables =
        {
            () => new Consumable(),
        };

        /// <summary>
        /// Creates random int between 0 and func: tableOfMonster length. Then calls func:tableOfMonster and manipulates returned Monster instance. 
        /// </summary>
        /// <returns></returns>
        public static Monster RandomMonster(int x, int y)

        {
            var rand = new Random().Next(0, tableOfMonsters.Length);
            Monster newMonster = tableOfMonsters[rand]();
            newMonster.X = x;
            newMonster.Y = y;
            rand = new Random().Next(-3, 3);
            newMonster.Health += rand;
            return newMonster;
        }

        /// <summary>
        /// Returns a new Monster instance by given index.
        /// </summary>
        private static Func<Monster>[] tableOfMonsters =
        {
            () => new Ghost(),
            () => new Skeleton(),
            () => new Beast(),
            () => new Zombie(),
        };

    }

}
