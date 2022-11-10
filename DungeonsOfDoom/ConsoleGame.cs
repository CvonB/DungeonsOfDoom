﻿namespace DungeonsOfDoom
{
    class ConsoleGame
    {
        Room[,] world;
        Player player;
        static int origRow; //Used in writing console at specific point
        static int origCol; //Used in writing console at specific point
        public void Play()
        {
            Console.CursorVisible = false;
            CreatePlayer();
            CreateWorld();
            
            do
            {
                ClearBelow();
                EnterRoom();
                DisplayWorld();
                DisplayStats();
                AskForMovement();
            } while (player.IsAlive && Monster.MonsterCounter > 0);
            GameOver();
        }



        /// <summary>
        /// Writes s in column x and row y in console.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void WriteAt(string s)
        {
            try
            {
                Console.SetCursorPosition(origCol + 0, origRow + 6);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Returns a random value in ENUM rarity.
        /// </summary>
        /// <returns></returns>
        public Rarity RandomRarity()
        {
            Array values = Enum.GetValues(typeof(Rarity));
            Random random = new Random();
            return (Rarity)values.GetValue(random.Next(values.Length));
        }


        /// <summary>
        /// Checks whether the room that the player is located in includes either an item or monster. 
        /// If item the player will loot the item and it will be added to player.inventory.
        /// If moonster the player can either start combat with monster (Method: Combat) or run away (Method: Flee)
        /// </summary>
        private void EnterRoom()
        {
            Room room = world[player.X, player.Y];
            
            if (room.ItemInRoom != null)
            {
                WriteAt($"You picked up {room.ItemInRoom.Name}");
                player.Inventory.Add(room.ItemInRoom);
                room.ItemInRoom = null;
                StackItem(player.Inventory);
            }
            else if (room.MonsterInRoom != null)
            {
                Monster enemy = room.MonsterInRoom;
                WriteAt($"You have encountered {enemy.Type}");
                WriteAt($"Press any key to attack (or [R]un if you are scared)",0,7);
                if (Console.ReadKey(true).Key == ConsoleKey.R)
                    Flee();
                else
                {
                    ClearBelow();
                    Combat(player, enemy);
                    if (!enemy.IsAlive)
                    {
                        room.MonsterInRoom = null;
                    }
                }
            }

        }

        /// <summary>
        /// Moves the player to a random walkable spot, offset by players current location by 1.
        /// </summary>
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
            ClearBelow();
        }

        /// <summary>
        /// Takes player inventory and for each stackable item it will remove duplicates and add to the first instance's count.
        /// </summary>
        /// <param name="inventory"></param>
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
        /// <summary>
        /// Creates new player instance.
        /// </summary>
        private void CreatePlayer()
        {
            player = new Player();
        }

        /// <summary>
        /// Creates new Room array for field: world. Populates world with random monsters and random items at random X and Y value.
        /// </summary>
        private void CreateWorld()
        {
            int notOnPlayer = 0;
            Monster.MonsterList.Clear();
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

        /// <summary>
        /// Creates random int between 0 and func: tableOfItems length. Then calls func:tableOfItems and manipulates returned item instance. 
        /// </summary>
        /// <returns></returns>
        public static Item RandomItem()
        {
            var rand = new Random().Next(0, tableOfItems.Length);
            return tableOfItems[rand]();
        }

        /// <summary>
        /// Returns a new Item instance by given index.
        /// </summary>
        private static Func<Item>[] tableOfItems =
        {
            () => new Consumable(),
            () => new Axe(),
            () => new Sword(),
            () => new Spear()
        };

        /// <summary>
        /// Creates random int between 0 and func: tableOfMonster length. Then calls func:tableOfMonster and manipulates returned Monster instance. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a new Monster instance by given index.
        /// </summary>
        private static Func<Monster>[] tableOfMonsters =
        {
            () => new Ghost(),
            () => new Skeleton(),
            () => new Beast(),
            () => new Zombie(),
        };
        #endregion

        #region Display
        /// <summary>
        /// Prints world[] in the console.
        /// </summary>
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
                        WriteAt("P",x,y);
                        Console.ResetColor();
                    }
                    else if (room.MonsterInRoom != null)
                    {
                        Console.ForegroundColor = room.MonsterInRoom.EntityColor;
                        WriteAt("M",x,y);
                        Console.ResetColor();
                    }
                    else if (room.ItemInRoom != null)
                        WriteAt("I",x,y);
                    else
                        WriteAt(".",x,y);
                }
                Console.WriteLine();
            }
        }

        private void ClearBelow()
        {
            for (int i = world.GetLength(1); i < 20; i++)
            {
                WriteAt("                                                                                                                     ", 0, i);
            }
        }

        /// <summary>
        /// Displays player.Health and player stats
        /// </summary>
        private void DisplayStats()
        {
            WriteAt($"Health: {player.Health}", 40, 0);
            WriteAt($"[I]nventory:", 40, 1);
            WriteAt($"Monster count: {Monster.MonsterCounter}", 40, 2);
        }

        /// <summary>
        /// Displays all items in player.Inventory and allows user to interact with inventory.
        /// </summary>
        /// <param name="inventory"></param>
        private void Inventory(List<Item> inventory)
        {
            int indent = 0, startRow = 8;
            int picked = 0;
            WriteAt("[I]nventory close", indent, startRow-2);
            WriteAt("[E]quip", indent, startRow-1);
            for (int i = 0; i < inventory.Count; i++)
            {
                Item item = inventory[i];
                string tmp = "";
                if (item.Stackable)
                    tmp = $"{item.Count}x";
                else
                {
                    if (player.EquipedWeapon == item || player.EquipedArmor == item)
                        tmp = $"{tmp} [Equiped]";
                }
                WriteAt($"{item.Name} {tmp}", indent, i+ startRow);

            }

            InventoryMove(picked,indent,startRow);
            ClearBelow();
        }

        private void InventoryMove(int picked, int indent, int startRow)
        {
            int previous = picked;
            if (player.Inventory.Count == 0)
                return;
            while (true)
            {
                Item item = player.Inventory[picked];
                WriteAt("   ", 20+ indent, previous + startRow);
                WriteAt("<--", 20+ indent, picked + startRow);
                WriteAt("--------------------", 50+ indent, startRow);
                WriteAt("--------------------", 50+ indent, startRow+10);
                for (int i = startRow; i < startRow+11; i++)
                {
                WriteAt("|", 50 + indent, i);
                WriteAt("|", 70 + indent, i);
                }
                WriteAt($"Type:            ", 52 + indent, 3+ startRow);
                WriteAt($"Power:           ", 52 + indent, 4+ startRow);
                WriteAt($"Rarity:          ", 52 + indent, 5 + startRow);

                WriteAt($"Type:   {item.Type}", 52 + indent, 3 + startRow);
                WriteAt($"Power:  {item.Power}", 52 + indent, 4 + startRow);
                WriteAt($"Rarity: {item.Rare}", 52 + indent, 5 + startRow);

                switch (Console.ReadKey(true).Key) 
                {
                    case ConsoleKey.UpArrow:
                        if (picked > 0)
                        {
                            previous= picked;
                            picked--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (picked < player.Inventory.Count-1)
                        {
                            previous= picked;
                            picked++;
                        }
                        break;
                    case ConsoleKey.E:
                        // lägg Use metoden här
                        break;
                    case ConsoleKey.I:
                        return;
                }



            }
        }

        /// <summary>
        /// Displays "Game over" in console and lets player restart.
        /// </summary>
        private void GameOver()
        {
            Console.Clear();
            if (Monster.MonsterCounter > 0)
            {
                Console.WriteLine("Game over...");
            }
            else
            {
                Console.WriteLine("You finished the level.");
            }
            Console.ReadKey();
            Play();
        }
        #endregion
        /// <summary>
        /// Waits for the player to input an arrowkey to move or press I to open inventory
        /// </summary>
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

        /// <summary>
        /// Makes player attack monster. If monster lives it will then attack back, else monsters inventory will be moved to players inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="monster"></param>
        public void Combat(LivingEntity player, LivingEntity monster)
        {
            WriteAt($"You damaged {monster.Name} for {player.Attack(monster)} damage.");
            if (monster.IsAlive)
            {
                WriteAt($"{monster.Name} has {monster.Health} health remaining.",0,7);
                WriteAt($"{monster.Name} damaged you for {monster.Attack(player)} damage.",0,8);
            }
            else
            {
                WriteAt($"You killed {monster.Name}. Grab you loot!", 0,7);
                player.Inventory.AddRange(monster.Inventory);
                StackItem(player.Inventory);
            }
        }
    }

}
