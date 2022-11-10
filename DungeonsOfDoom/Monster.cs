namespace DungeonsOfDoom
{
    abstract class Monster : LivingEntity, ICarryable
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            HasItem();
            Rare = RandomUtils.RandomRarity();
            EquippedWeapon = new Unarmed();
            EquippedArmor = new Unarmored();
            MonsterList.Add(this);

        }

        public Monster(string name, int health, Rarity rare)
        {
            Name = name;
            Health = health;
            HasItem();
            Rare = rare;
            MonsterList.Add(this);
        }

        public override int Health
        {
            get => base.Health;
            set
            {
                base.Health = value;
                if (base.Health <= 0)
                {
                    MonsterList.Remove(this);
                }
            }
        }

        public override string Name
        {
            get
            {
                if (IsAlive)
                    return base.Name;

                return $"{base.Name} corpse";
            }
            set => base.Name = value;
        }

        public void Interact()
        {

        }

        public static int MonsterCounter => MonsterList.Count;
        public static List<Monster> MonsterList { get; private set; } = new List<Monster>();

        override public Rarity Rare { get; set; }

        public string[] Ascii { get; set; }

        public bool Stackable { get; set; }

        public int Count { get; set; }

        public string Type => this.GetType().ToString().Split('.')[1];

        void HasItem() //Vi kanske kan göra så att RandomItem tar en enum som är rare,epic osv. - Christian
        {
            int percentage = Random.Shared.Next(0, 100);
            if (percentage < 20)
                Inventory.Add(ConsoleGame.RandomItem());
            //else if (percentage < 20)
            //    Inventory.Add(new Item("Common Item"));
        }

    }
    // Olika monsterklasser startar med olika Health värden.
    // Skulle kunna lägga till saker som svaghet mot vissa typer av vapen,
    // skala upp Items värde och antal beroende på monsterklassens styrka - Nima

    //Vi kan göra en till subclass som är Armortype. Så kan vi göra så att vissa armortypes är svaga mot vissa weapontypes. 
    class Skeleton : Monster
    {
        public Skeleton() : base("Skeleton", 30)
        {

            EntityColor = ConsoleColor.DarkYellow;
            Ascii = "      .-.\r\n     (o.o)\r\n      |=|\r\n     __|__\r\n   //.=|=.\\\\\r\n  // .=|=. \\\\\r\n  \\\\ .=|=. //\r\n   \\\\(_=_)//\r\n    (:| |:)\r\n     || ||\r\n     () ()\r\n     || ||\r\n     || ||\r\n    ==' '==".Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
        }

    }
    class Ghost : Monster
    {
        public Ghost() : base("Ghost", 45)
        {
            EquippedWeapon = new Unarmed(15);
            EntityColor = ConsoleColor.Red;
            Ascii = "       .-.\r\n      ( \" )\r\n   /\\_.' '._/\\\r\n   |         |\r\n    \\       /\r\n     \\    /`\r\n   (__)  /\r\n   `.__.'".Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
    class Beast : Monster
    {
        public Beast() : base("Beast", 15)
        {
            EntityColor = ConsoleColor.Magenta;
            Ascii = "        _\r\n       / \\      _-'\r\n     _/|  \\-''- _ /\r\n__-' { |          \\\r\n    /             \\\r\n    /       \"o.  |o }\r\n    |            \\ ;\r\n                  ',\r\n       \\_         __\\\r\n         ''-_    \\.//\r\n           / '-____'\r\n          /\r\n        _'\r\n      _-'".Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
    class Zombie : Monster
    {
        public Zombie(int health, Rarity rarity) : base("Zombie", health, rarity)
        {
            EntityColor = ConsoleColor.Cyan;
        }

        public Zombie() : base("Zombie", 20)
        {
            EquippedWeapon = new Axe(15);
            EntityColor = ConsoleColor.Cyan;
            Ascii = "                        _,--~~~,\r\n                       .'        `.\r\n                       |           ;\r\n                       |           :\r\n                      /_,-==/     .'\r\n                    /'`\\*  ;      :      \r\n                  :'    `-        :      \r\n                  `~*,'     .     :      \r\n                     :__.,._  `;  :      \r\n                     `\\'    )  '  `,     \r\n                         \\-/  '     )     \r\n                         :'          \\ _\r\n                          `~---,-~    `,)\r\n          ___                   \\     /~`\\\r\n    \\---__ `;~~~-------------~~~(| _-'    `,\r\n  ---, ' \\`-._____     _______.---'         \\\r\n \\--- `~~-`,      ~~~~~~                     `,\r\n\\----      )                                   \\\r\n\\----.  __ /                                    `-\r\n \\----'` -~____  \r\n               ~~~~~--------,.___             \r\n                                 ```\\_".Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        }
    }
}
