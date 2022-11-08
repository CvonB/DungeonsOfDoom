namespace DungeonsOfDoom
{

    //TODO: Konvertera Monster till BaseClass och introducera minst 2 SubClasser
    class Monster : LivingEntity //Jag anser att denna kan vara abstrakt -- Christian
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            HasItem();
        }

        public string Name { get; set; }

        void HasItem()
        {
            Item tmp;
            //TODO: Redigera så det funkar med random item!
            int percentage = Random.Shared.Next(1, 100); 
            if (percentage < 10)
                Inventory.Add(new Item("Rare Item"));
            else if (percentage < 20)
                Inventory.Add(new Item("Common Item"));
        }

    }
    class Skeleton : Monster
    {
        public Skeleton(string name, int health) : base(name, health)
        {
        }
    }
    class Zombie : Monster
    {
        public Zombie(string name, int health) : base(name, health)
        {
        }
    }
}
