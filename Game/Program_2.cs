//#define MAIN

#if MAIN

using System.Xml.Linq;

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
        static void Main()
        {
            string filename = "loot.xml";
            if (!File.Exists(filename))
            {
                XDocument document = new XDocument(
                    new XElement("Items",
                        new XElement("Item",
                            new XAttribute("type", "Weapon"),
                            new XElement("Name", "Меч новичка"),
                            new XElement("Weight", 3.5),
                            new XElement("Damage", 15)
                        ),
                        new XElement("Item",
                            new XAttribute("type", "Potion"),
                            new XElement("Name", "Малое зелье здоровья"),
                            new XElement("Weight", 0.5),
                            new XElement("HealAmount", 50)
                        ),
                        new XElement("Item",
                            new XAttribute("type", "Weapon"),
                            new XElement("Name", "Деревянный лук"),
                            new XElement("Weight", 2.0),
                            new XElement("Damage", 10)
                        )
                    )
                );
                document.Save(filename);
            }

            Inventory inventory = new Inventory();

            XDocument doc = XDocument.Load(filename);
            foreach (XElement itemElement in doc.Root.Elements("Item"))
            {
                string type = itemElement.Attribute("type").Value;
                string name = itemElement.Element("Name").Value;
                double weight = double.Parse(itemElement.Element("Weight").Value, System.Globalization.CultureInfo.InvariantCulture);

                if (type == "Weapon")
                {
                    int damage = int.Parse(itemElement.Element("Damage").Value);
                    inventory.Items.Add(new Weapon(name, weight, damage));
                }
                else if (type == "Potion")
                {
                    int healAmount = int.Parse(itemElement.Element("HealAmount").Value);
                    inventory.Items.Add(new Potion(name, weight, healAmount));
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