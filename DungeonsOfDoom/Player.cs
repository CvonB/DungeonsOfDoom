namespace DungeonsOfDoom
{
    public class Player : LivingEntity
    {
        public Player()
        {
            Health = 30;
            X = 0;
            Y = 0;
            EntityColor = ConsoleColor.Green;
            EquippedWeapon = new Unarmed(10);
            EquippedArmor = new Armor("Unarmored",ArmorTypes.Unarmored,0);
            CritChance = 10;
        }

    }
}
