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
                AskForMovement();
                EnterRoom();
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
            }
            else if (room.MonsterInRoom != null) //If there is a monster in the room
            {
                player.Inventory.AddRange(room.MonsterInRoom.Inventory);
                room.MonsterInRoom = null;
            }
        }

        private void CreatePlayer()
        {
            player = new Player(30, 0, 0);
        }

        private void CreateWorld()
        {
            world = new Room[20, 5];
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    world[x, y] = new Room();

                    int percentage = Random.Shared.Next(1, 100);

                    if (percentage < 10)
                    {
                        world[x, y].MonsterInRoom = RandomMonster(x,y);   //new Monster("Skeleton", 30);  //gammal

                    }
                    //else if (percentage < 20)   //Gammla systemet
                    //    world[x, y].ItemInRoom = new Item();
                    else if (percentage < 20)
                        world[x, y].ItemInRoom = RandomItem(); // Generar random item
                }
            }
        }

        //TODO: Titta gärna på denna metod. Den tar en random av de två Item Subclasserna
        public static Item RandomItem()
        {
            var rand = new Random().Next(0, tableOfItems.Length);
            return tableOfItems[rand]();
        }

        private static Func<Item>[] tableOfItems =
        {
            () => new Consumable(),
            () => new Weapon()
        };

        public static Monster RandomMonster(int x, int y)

        {
            var rand = new Random().Next(0, tableOfMonsters.Length);
            Monster newMonster = tableOfMonsters[rand]();
            newMonster.X = x;
            newMonster.Y = y;
            return newMonster;
        }

        private static Func<Monster>[] tableOfMonsters =
        {
            () => new Ghost(),
            () => new Skeleton()
        };

        private void DisplayWorld()
        {
            for (int y = 0; y < world.GetLength(1); y++)
            {
                for (int x = 0; x < world.GetLength(0); x++)
                {
                    Room room = world[x, y];
                    if (player.X == x && player.Y == y)
                        Console.Write("P");
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
            Console.WriteLine("Items: ");
            foreach (var item in player.Inventory)
            {
                Console.WriteLine($"{item.Name} of type: {item.Type} ");
            }
        }

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

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game over...");
            Console.ReadKey();
            Play();
        }
    }
}
