namespace DungeonsOfDoom
{
    class ConsoleGame
    {
        Room[,] world;
        Player player;
        public void Play()
        {
            Console.CursorVisible = false;
            CreatePlayer();
            CreateWorld();

            do
            {
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                EnterRoom();
                AskForMovement();
            } while (player.IsAlive);
            GameOver();
        }

        private void EnterRoom()
        {
            Room room = world[player.X, player.Y];
            if (room.ItemInRoom != null) //If there is an item in the room
            {
                player.Inventory.Add(room.ItemInRoom); //Picks up item and adds to inventory of player.
                room.ItemInRoom = null; //Removes item from the location.
                StackItem(player.Inventory);
            }
            else if (room.MonsterInRoom != null) //If there is a monster in the room
            {
                Monster enemy = room.MonsterInRoom;
                Combat(player, enemy);
                if (!room.MonsterInRoom.IsAlive)
                {
                    player.Inventory.AddRange(enemy.Inventory);
                    enemy = null;
                    StackItem(player.Inventory);
                }
                Console.ReadKey();
            }
        }

        private void StackItem(List<Item> inventory)
        {

            Item tmpItem = null;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Stackable)
                {
                    int j = 0;
                    var tmpList = inventory.Where(x => x.Name.Equals(inventory[i].Name)).Where(x => x.Type.Equals(inventory[i].Type)).ToList();
                    foreach (var item in tmpList)
                    {
                        if (j++ > 0)
                        {
                            inventory.Remove(item);
                            tmpItem.Count++;
                        }
                        else
                            tmpItem = item;
                    }
                }
            }
        }

        #region Creation
        private void CreatePlayer()
        {
            player = new Player();
        }

        private void CreateWorld()
        {
            int notOnPlayer = 0;
            world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    int percentage = Random.Shared.Next(1, 100);

                    if (percentage < 10 && notOnPlayer != 0)
                    {
                        world[x, y].MonsterInRoom = RandomMonster(x, y);   //new Monster("Skeleton", 30);  //gammal

                    }
                    //else if (percentage < 20)   //Gammla systemet
                    //    world[x, y].ItemInRoom = new Item();
                    else if (percentage < 20 && notOnPlayer != 0)
                        world[x, y].ItemInRoom = RandomItem(); // Generar random item
                    notOnPlayer++;
                }
            }
        }

        #endregion

        #region RandomGen
        public static Item RandomItem()
        {
            var rand = new Random().Next(0, tableOfItems.Length);
            return tableOfItems[rand]();
        }

        private static Func<Item>[] tableOfItems =
        {
            () => new Consumable(),
            () => new Axe(),
            () => new Sword(),
            () => new Spear()
        };

        public static Monster RandomMonster(int x, int y)

        {
            var rand = new Random().Next(0, tableOfMonsters.Length);
            Monster newMonster = tableOfMonsters[rand]();
            newMonster.X = x;
            newMonster.Y = y;
            rand = new Random().Next(-3, 3);
            newMonster.Health += rand;
            return newMonster;
        }

        private static Func<Monster>[] tableOfMonsters =
        {
            () => new Ghost(),
            () => new Skeleton(),
            () => new Beast(),
            () => new Zombie(),
        };
        #endregion

        #region Display
        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                    {
                        Console.ForegroundColor = player.EntityColor;
                        Console.Write("P");
                        Console.ResetColor();

                    }
                    else if (room.MonsterInRoom != null)
                    {
                        Console.ForegroundColor = room.MonsterInRoom.EntityColor;
                        Console.Write("M");
                        Console.ResetColor();


                    }
                    else if (room.ItemInRoom != null)
                        Console.Write("I");
                    else
                        Console.Write(".");
                }
                Console.WriteLine();
            }
        }

        private void DisplayStats()
        {
            Console.WriteLine($"Health: {player.Health}");
            Console.WriteLine("[I]nventory:");

        }


        private void Inventory(List<Item> inventory)
        {
            Console.Clear();
            foreach (var item in inventory)
            {
                string tmp ="";
                if (item.Stackable)
                    tmp = $"{item.Power} Health {item.Count}x";
                else
                    tmp = $"{item.Power} power.";
                Console.WriteLine($"{item.Name} of type: {item.Type} {tmp} ");
            }
            Console.ReadKey();
            Console.Clear();
            DisplayWorld();
            DisplayStats();
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Play();
        }
#endregion
        private void AskForMovement()
        {
            bool isValidKey = false;
            do
            {
                int newX = player.X;
                int newY = player.Y;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow: newX++; isValidKey = true; break;
                    case ConsoleKey.LeftArrow: newX--; isValidKey = true; break;
                    case ConsoleKey.UpArrow: newY--; isValidKey = true; break;
                    case ConsoleKey.DownArrow: newY++; isValidKey = true; break;
                    case ConsoleKey.I: Inventory(player.Inventory); break;
                    default: isValidKey = false; break;
                }
                if (newX >= 0 && newX < world.GetLength(0) &&
                    newY >= 0 && newY < world.GetLength(1))
                {
                    player.X = newX;
                    player.Y = newY;
                }
                else
                    isValidKey = false;
            } while (!isValidKey);
        }

        public void Combat(LivingEntity attacker, LivingEntity opponent)
        {
            Console.WriteLine($"You damaged {opponent.Name} for {attacker.Attack(opponent)} damage.");
            if (opponent.IsAlive)
            {
                Console.WriteLine($"{opponent.Name} damaged you for {opponent.Attack(attacker)} damage.");
            }
        }

    }
}
