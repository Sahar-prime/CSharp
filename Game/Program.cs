//#define MAIN

#if MAIN
namespace Game
{
    abstract class Item
    {
        public string Name;
        public double Weight;

        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public abstract void Use();
    }

    class Weapon : Item
    {
        public int Damage;

        public Weapon(string name, double weight, int damage) : base(name, weight)
        {
            Damage = damage;
        }

        public override void Use()
        {
            Console.WriteLine($"Вы экипировали {Name}. Урон: {Damage}");
        }
    }

    class Potion : Item
    {
        public int HealAmount;

        public Potion(string name, double weight, int healAmount) : base(name, weight)
        {
            HealAmount = healAmount;
        }

        public override void Use()
        {
            Console.WriteLine($"Вы выпили {Name} и восстановили {HealAmount} HP!");
        }
    }

    class Inventory
    {
        public List<Item> Items = new List<Item>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filename = "loot.txt";
            if (!File.Exists(filename))
            {
                string[] lines = {
                    "Weapon,Меч новичка,3.5,15",
                    "Potion,Малое зелье здоровья,0.5,50",
                    "Weapon,Деревянный лук,2.0,10"
                };
                File.WriteAllLines(filename, lines);
            }

            Inventory inventory = new Inventory();

            string[] fileLines = File.ReadAllLines(filename);
            foreach (string line in fileLines)
            {
                string[] parts = line.Split(',');

                string type = parts[0];
                string name = parts[1];

                double weight = double.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                int stat = int.Parse(parts[3]);

                if (type == "Weapon")
                {
                    inventory.Items.Add(new Weapon(name, weight, stat));
                }
                else if (type == "Potion")
                {
                    inventory.Items.Add(new Potion(name, weight, stat));
                }
            }

            double totalWeight = 0;
            foreach (Item item in inventory.Items)
            {
                totalWeight += item.Weight;
            }
            Console.WriteLine($"Общий вес предметов: {totalWeight} кг\n");

            Console.WriteLine("--- Использование предметов ---");
            foreach (Item item in inventory.Items)
            {
                item.Use();
            }
        }
    }
}
#endif //MAIN