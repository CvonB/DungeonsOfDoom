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
            Power= 10;
        }

    }
}
