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
    // Olika monsterklasser startar med olika Health värden.
    // Skulle kunna lägga till saker som svaghet mot vissa typer av vapen,
    // skala upp Items värde och antal beroende på monsterklassens styrka
    class Skeleton : Monster
    {
        public Skeleton() : base("Skeleton", 30)
        {
        }
    }
    class Zombie : Monster
    {
        public Zombie() : base("Zombie", 45)
        {
        }
    }
}
