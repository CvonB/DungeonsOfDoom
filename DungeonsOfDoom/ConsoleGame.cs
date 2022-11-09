using System.Runtime.InteropServices;

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
                EnterRoom();
                Console.Clear();
                DisplayWorld();
                DisplayStats();
                AskForMovement();
            } while (player.IsAlive);
            GameOver();
        }

        private void EnterRoom()
        {
            Room room = world[player.X, player.Y];
            if (room.ItemInRoom != null)
            {
                Console.WriteLine($"You picked up {room.ItemInRoom.Name}");
                Thread.Sleep(1000);
                player.Inventory.Add(room.ItemInRoom);
                room.ItemInRoom = null;
                StackItem(player.Inventory);
            }
            else if (room.MonsterInRoom != null)
            {
                Monster enemy = room.MonsterInRoom;
                Console.WriteLine($"You have encountered {enemy.Type}");
                Console.WriteLine($"Press any key to attack (or [R]un if you are scared)");
                if (Console.ReadKey(true).Key == ConsoleKey.R)
                    Flee();
                else
                {
                    Combat(player, enemy);
                    if (!enemy.IsAlive)
                    {
                        room.MonsterInRoom = null;
                    }
                }
            }
        }
        private void Flee()
        {
            int x = player.X;
            int y = player.Y;
            do
            {
                player.X = x; player.Y = y;
                switch (Random.Shared.Next(0, 4))
                {
                    case 0:
                        player.X++;
                        break;
                    case 1:
                        player.X--;
                        break;
                    case 2:
                        player.Y++;
                        break;
                    case 3:
                        player.Y--;
                        break;

                }

            } while (!(player.X >= 0 && player.X < world.GetLength(0) &&
                    player.Y >= 0 && player.Y < world.GetLength(1)));
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
                        world[x, y].MonsterInRoom = RandomMonster(x, y);

                    }
                    else if (percentage < 20 && notOnPlayer != 0)
                        world[x, y].ItemInRoom = RandomItem();
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
                string tmp = "";
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

        public void Combat(LivingEntity player, LivingEntity monster)
        {
            Console.WriteLine($"You damaged {monster.Name} for {player.Attack(monster)} damage.");
            if (monster.IsAlive)
            {
                Console.Write($" {monster.Name} has {monster.Health} health remaining.");
                Console.WriteLine($"{monster.Name} damaged you for {monster.Attack(player)} damage.");
            }
            else
            {
                Console.WriteLine($"You killed {monster.Name}. Grab you loot!");
                player.Inventory.AddRange(monster.Inventory);
                StackItem(player.Inventory);
            }
            Console.WriteLine("move along...");
            Console.ReadKey();
        }
    }
}
