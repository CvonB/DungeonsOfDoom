namespace DungeonsOfDoom
{
    class Monster
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
            int percentage = Random.Shared.Next(1, 100); // Redigeras så det funkar med random item!
            if (percentage < 10)
                Inventory.Add(new Item("Rare Item"));
            else if (percentage < 20)
                Inventory.Add(new Item("Common Item"));
        }

    }
}
