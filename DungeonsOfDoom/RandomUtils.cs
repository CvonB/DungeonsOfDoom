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
            int tmp = Random.Shared.Next(0,Enum.GetNames(typeof(Rarity)).Length);
            return (Rarity)tmp;
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
    }

}
