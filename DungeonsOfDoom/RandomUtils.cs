using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    public static class RandomUtils
    {
        public static Rarity RandomRarity()
        {
            int tmp = Random.Shared.Next(0,Enum.GetNames(typeof(Rarity)).Length);
            return (Rarity)tmp;
        }
    }
}
