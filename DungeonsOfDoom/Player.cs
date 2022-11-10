namespace DungeonsOfDoom
{
    class Player : LivingEntity
    {
        public Player()
        {
            Health = 30;
            X = 0;
            Y = 0;
            EntityColor = ConsoleColor.Green;
            EquippedWeapon = new Unarmed();
            EquippedArmor = new Armor("Unarmored",ArmorTypes.Unarmored);
            CritChance = 10;
        }

    }
}
