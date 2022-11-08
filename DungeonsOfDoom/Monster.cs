namespace DungeonsOfDoom
{

    //TODO: Konvertera Monster till BaseClass och introducera minst 2 SubClasser
    class Monster //Jag anser att denna kan vara abstrakt -- Christian
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            HasItem();
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public bool IsAlive { get { return Health > 0; } }
        public List<Item> Inventory { get;} = new List<Item>();   

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
}
