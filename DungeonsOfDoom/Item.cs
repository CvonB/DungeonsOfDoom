namespace DungeonsOfDoom
{
    public interface ICarryable
    {
        public string Name { get; set; }
        public string Type { get; }
        public int Power { get; set; }
        public Rarity Rare { get; set; }
        public bool Stackable { get; set; }
        public int Count { get; set; }
        void Interact() { }
        void Interact(LivingEntity Player) { }

        public int CritChance { get; set; }

    }

    public abstract class Item : ICarryable
    {
        public Item(string name)
        {
            Name = name;

            Rare = RandomUtils.RandomRarity();
        }

        public Item(string name, Rarity rare)
        {
            Name = name;
            Rare = rare;
        }

        public virtual void Interact(LivingEntity user) { }
        public string Name { get => $"{this.Rare} {_name}"; set => _name = value; }
        private string _name;

        //public bool Consumable => Type == "Consumable";
        /// <summary>
        /// Returns SubClass to string.
        /// </summary>
        public string Type => this.GetType().ToString().Split('.')[1];
        /// <summary>
        /// Returns how many of said Item is in Inventory. Only applicable if Stackable == true.
        /// </summary>
        public int Count { get; set; } = 1;
        public int CritChance { get; set; }

        public Rarity Rare { get; set; }
        /// <summary>
        /// Allows item to be stacked on top of each other.
        /// </summary>
        public bool Stackable { get; set; }
        /// <summary>
        /// Returns the power of the Item. If weapon, will deal said amount as damage. If armor, will remove said amount from damage
        /// If consumable, will affect player by said amount. Ex heal player by power.
        /// </summary>
        public int Power
        {
            get => _power;
            set => _power = value + (2 * (int)this.Rare);
        }

        int _power;
    }
    #region Weapons
    public abstract class Weapon : Item
    {

        public Weapon(string weaponType, int power) : base(weaponType)
        {
            Power = power;
        }
        public Weapon(string weaponType, int power, Rarity rare) : base(weaponType,rare)
        {
            Power = power;
        }



        // if Armor = StrongAgainst > Damage = 1.5 else Damage 1
        // rand 0-100.
        // if (crit > rand) Damage *= 2
        // Health - power*Damage

        public ArmorTypes StrongAgainst { get; set; }
        public override void Interact(LivingEntity user)
        {
            user.EquippedWeapon = this;
        }
    }


    public class Unarmed : Weapon
    {
        public Unarmed() : base("Your fists", 0, Rarity.Common)
        {

            StrongAgainst = ArmorTypes.Nothing;
        }

        public Unarmed(int power) : base("Your fists", power, Rarity.Common)
        {
            StrongAgainst = ArmorTypes.Nothing;

        }
    }

    public class Sword : Weapon
    {
        public Sword() : base("Sword", 15)
        {
            StrongAgainst = ArmorTypes.Light;
            CritChance = Random.Shared.Next(1, 15);
        }

        public Sword(int power) : base("Sword", power)
        {

        }
    }

    public class Mace : Weapon
    {
        public Mace(int power) : base("Heavy Mace", power)
        {
            CritChance = Random.Shared.Next(1, 15);

        }
        public Mace() : base("Mace", 15)
        {
            StrongAgainst = ArmorTypes.Heavy;
            CritChance = Random.Shared.Next(1, 15);

        }
    }

    public class Spear : Weapon
    {
        public Spear() : base("Spear", 15)
        {
            StrongAgainst = ArmorTypes.Unarmored;
            CritChance = Random.Shared.Next(1, 15);

        }
        public Spear(int power) : base("Spear", power)
        {

        }
    }
    #endregion

    #region Armor
    public class Armor : Item
    {
        public Armor(string armorName, ArmorTypes type) : base(armorName)
        {
            Power = Random.Shared.Next(0, 10);
            ArmorType = type;
        }
        public Armor(string armorName, ArmorTypes type, int power) : base(armorName)
        {
            Power = power;
            ArmorType = type;
        }
        public Armor(ArmorTypes type) : base("Monster Item")
        {
            Power = 0;
            ArmorType = type;
        }
        public override void Interact(LivingEntity user)
        {
            user.EquippedArmor = this;
        }

        public ArmorTypes ArmorType { get; set; }
    }
    #endregion

    #region Consumables
    public class Consumable : Item
    {
        public Consumable() : base("Health Potion")
        {
            Stackable = true;
            Power = 5;
        }
        // Nedan use borde flyttas ner till subclass "healing items" för att sedan kunna ha consumables som ger andra effekter.
        public override void Interact(LivingEntity user)
        {
            user.Health += Power;
            Count--;
            if (Count == 0)
                user.Inventory.Remove(this);
        }
    }
    #endregion
}
