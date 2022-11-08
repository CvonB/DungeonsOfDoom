namespace DungeonsOfDoom
{

    //TODO: Skapa BasKlass där spealare och monster är subklasser.
    class Player : LivingEntity
    {
        public Player(int health, int x, int y)
        {
            Health = health;
            X = x;
            Y = y;
        }

    }
}
