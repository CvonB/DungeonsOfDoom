using System;

namespace DungeonsOfDoom
{
    //TODO: Konvertera Item till BaseClass och introducera minst 2 SubClasser
    class Item//Jag anser att denna kan vara abstrakt -- Christian
    {
        public Item(string name)
        {
            Name = name;
        }
        public Item()
        {
            var namesCount = Enum.GetNames(typeof(Items)).Length;
            int rand = new Random().Next(0, namesCount);
            Name = ((Items)rand).ToString();
        }

        public string Name { get; set; } //Item name kanske kan skapas av en metod som tar in rarity där det finns, ex "Epic Sword"?

        //public bool Consumable => Type == "Consumable";
        //public string Type { get; set; }
    }
    class Consumable : Item
    {
        //Här kan man kanske lägga in egenskaper som t.ex. healvalue, plus metod för vad som händer när en consumable används.
    }
    class Weapon : Item
    {
        //Här kanske damage, weapon type, rarity etc. kan vara lämpliga egenskaper.
    }

    //TODO: Titta på dessa två subclasser. 
    //class Weapon : Item
    //{
    //    public Weapon() : base("Axe")
    //    {

    //    }
    //}

    //class Potion : Item
    //{
    //    public Potion() : base("Health Potion")
    //    {

    //    }
    //}


    //TODO: Konvertera denna enum till en SubClass istället
    enum Items 
        {
            Sword = 0,
            Axe = 1,
            Spear = 2,
            Pear = 3,
            Apple = 4,
        }

}
