namespace DungeonsOfDoom
{


    class Monster : LivingEntity //Jag anser att denna kan vara abstrakt -- Christian
    {
        public Monster(string name, int health)
        {
            Name = name;
            Health = health;
            HasItem();
        }


        public string Ascii { get; set; }
        public string Name { get; set; }



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
            EntityColor = ConsoleColor.Green;
            Ascii = "\t\t\t\t         _,.-------.,_\r\n\t\t\t\t     ,;~'             '~;,\r\n\t\t\t\t   ,;                     ;,\r\n\t\t\t\t  ;                         ;\r\n\t\t\t\t ,'                         ',\r\n\t\t\t\t,;                           ;,\r\n\t\t\t\t; ;      .           .      ; ;\r\n\t\t\t\t| ;   ______       ______   ; |\r\n\t\t\t\t|  `/~\"     ~\" . \"~     \"~\\'  |\r\n\t\t\t\t|  ~  ,-~~~^~, | ,~^~~~-,  ~  |\r\n\t\t\t\t |   |        }:{        |   |\r\n\t\t\t\t |   l       / | \\       !   |\r\n\t\t\t\t .~  (__,.--\" .^. \"--.,__)  ~.\r\n\t\t\t\t |     ---;' / | \\ `;---     |\r\n\t\t\t\t  \\__.       \\/^\\/       .__/\r\n\t\t\t\t   V| \\                 / |V\r\n\t\t\t\t    | |T~\\___!___!___/~T| |\r\n\t\t\t\t    | |`IIII_I_I_I_IIII'| |\r\n\t\t\t\t    |  \\,III I I I III,/  |\r\n\t\t\t\t     \\   `~~~~~~~~~~'    /\r\n\t\t\t\t       \\   .       .   /     \r\n\t\t\t\t         \\.    ^    ./\r\n\t\t\t\t           ^~~~^~~~^";
        }

    }
    class Ghost : Monster
    {
        public Ghost() : base("Ghost", 45)
        {
            EntityColor = ConsoleColor.Red;
            Ascii = "\t\t\t      .'``'.      ...\r\n\t\t\t     :o  o `....'`  ;\r\n\t\t\t     `. O         :'\r\n\t\t\t       `':          `.\r\n\t\t\t         `:.          `.\r\n\t\t\t          : `.         `.\r\n\t\t\t         `..'`...       `.\r\n\t\t\t                 `...     `.\r\n\t\t\t                     ``...  `.\r\n\t\t\t                          `````.";
        }
    }
}
