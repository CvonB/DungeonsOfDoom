namespace DungeonsOfDoom
{
    abstract class Monster : LivingEntity, ICarryable
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            HasItem();
            Rare = Rarity.Common;
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

        public Rarity Rare { get; set; }

        public string Ascii { get; set; }

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
            Ascii = "\t\t\t\t         _,.-------.,_\r\n\t\t\t\t     ,;~'             '~;,\r\n\t\t\t\t   ,;                     ;,\r\n\t\t\t\t  ;                         ;\r\n\t\t\t\t ,'                         ',\r\n\t\t\t\t,;                           ;,\r\n\t\t\t\t; ;      .           .      ; ;\r\n\t\t\t\t| ;   ______       ______   ; |\r\n\t\t\t\t|  `/~\"     ~\" . \"~     \"~\\'  |\r\n\t\t\t\t|  ~  ,-~~~^~, | ,~^~~~-,  ~  |\r\n\t\t\t\t |   |        }:{        |   |\r\n\t\t\t\t |   l       / | \\       !   |\r\n\t\t\t\t .~  (__,.--\" .^. \"--.,__)  ~.\r\n\t\t\t\t |     ---;' / | \\ `;---     |\r\n\t\t\t\t  \\__.       \\/^\\/       .__/\r\n\t\t\t\t   V| \\                 / |V\r\n\t\t\t\t    | |T~\\___!___!___/~T| |\r\n\t\t\t\t    | |`IIII_I_I_I_IIII'| |\r\n\t\t\t\t    |  \\,III I I I III,/  |\r\n\t\t\t\t     \\   `~~~~~~~~~~'    /\r\n\t\t\t\t       \\   .       .   /     \r\n\t\t\t\t         \\.    ^    ./\r\n\t\t\t\t           ^~~~^~~~^";
        }

    }
    class Ghost : Monster
    {
        public Ghost() : base("Ghost", 45)
        {
            EquippedWeapon = new Unarmed(15);
            EntityColor = ConsoleColor.Red;
            Ascii = "\t\t\t      .'``'.      ...\r\n\t\t\t     :o  o `....'`  ;\r\n\t\t\t     `. O         :'\r\n\t\t\t       `':          `.\r\n\t\t\t         `:.          `.\r\n\t\t\t          : `.         `.\r\n\t\t\t         `..'`...       `.\r\n\t\t\t                 `...     `.\r\n\t\t\t                     ``...  `.\r\n\t\t\t                          `````.";
        }
    }
    class Beast : Monster
    {
        public Beast() : base("Beast", 15)
        {
            EntityColor = ConsoleColor.Magenta;
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
        }
    }
}
