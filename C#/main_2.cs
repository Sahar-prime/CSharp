//#define Main

using System;
using System.Collections.Generic;
using System.Linq;

#if Main
namespace _20._04
{
    internal class Program
    {
        static void GetInfo(List<int> collection)
        {
            Console.WriteLine($"Размер = {collection.Count}");
            Console.WriteLine($"Вместимость = {collection.Capacity}");
        }

        class Point
        {
            public int x, y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            //public override bool Equals(object? obj)
            //{
            //    if (obj is Point point)
            //    {
            //        return this.x == point.x && this.y == point.y;
            //    }
            //    return false;
            //}
        }

        // Функции группировки принимают элементы коллекции, а возвращают значение группы
        static int GroupPointByX(Point point)
        {
            return point.x;
        }

        static int GroupPointByAsinX(Point point)
        {
            return Math.Sign(point.x);
        }

        static double Distanse(Point target, Point _base)
        {
            return Math.Sqrt(
                    Math.Pow(target.x - _base.x, 2) +
                    Math.Pow(target.y - _base.y, 2)
                );
        }

        static double Distanse(Point target)
        {
            return Distanse(target, new Point(0, 0));
        }

        /**
         *  1. Создать словарь "телефонная книга"
         *      Ключ - имя человека, значение - его номер телефона
         *      1. Поменять номер телефона у пользователя
         *      *2. Поменять имя контакта в словаре(менять ключ нельзя - удаляем старый контакт 
         *          и на его места создаем новую запись)
         * 
         *  2. Вводим текст, посчитать сколько раз в тексте встречалось каждое слово
         */
        // 1. Телефонная книга
        static void PhoneBookTask()
        {
            Dictionary<string, string> phoneBook = new Dictionary<string, string>
        {
            { "Иван", "+123" },
            { "Мария", "+444" },
            { "Алексей", "+555" }
        };

            Console.WriteLine("\n--- Телефонная книга ---");
            foreach (var contact in phoneBook)
            {
                Console.WriteLine($"{contact.Key}: {contact.Value}");
            }

            // 1.1. Изменение номера телефона
            Console.WriteLine("\nВведите имя для изменения номера:");
            string nameToUpdate = Console.ReadLine();
            if (phoneBook.ContainsKey(nameToUpdate))
            {
                Console.WriteLine($"Текущий номер: {phoneBook[nameToUpdate]}");
                Console.WriteLine("Введите новый номер:");
                phoneBook[nameToUpdate] = Console.ReadLine();
                Console.WriteLine("Номер обновлен!");
            }
            else
            {
                Console.WriteLine("Контакт не найден.");
            }

            // 1.2. Изменение имени контакта
            Console.WriteLine("\nВведите текущее имя контакта для изменения:");
            string oldName = Console.ReadLine();
            if (phoneBook.ContainsKey(oldName))
            {
                Console.WriteLine("Введите новое имя:");
                string newName = Console.ReadLine();
                string phoneNumber = phoneBook[oldName];
                phoneBook.Remove(oldName);
                phoneBook.Add(newName, phoneNumber);
                Console.WriteLine("Имя контакта изменено!");
            }
            else
            {
                Console.WriteLine("Контакт не найден.");
            }

            Console.WriteLine("\nОбновленная телефонная книга:");
            foreach (var contact in phoneBook)
            {
                Console.WriteLine($"{contact.Key}: {contact.Value}");
            }
        }

        // 2. Подсчет слов в тексте
        static void WordCountTask()
        {
            Console.WriteLine("\n--- Подсчет слов в тексте ---");
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();

            // Разбиваем текст на слова, игнорируя знаки препинания
            var words = inputText.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            // Группируем слова и считаем их количество
            var wordCounts = words
                .GroupBy(word => word.ToLower())
                .ToDictionary(group => group.Key, group => group.Count());

            Console.WriteLine("\nКоличество вхождений каждого слова:");
            foreach (var pair in wordCounts)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value}");
            }
        }

        static void Main(string[] args)
        {
            PhoneBookTask();
            WordCountTask();

            {

                string d = "333";
                d.Split(' '); // "Hello world" -> ["Hello", "world"];
                Point p1 = new Point(1, 1);
                Point p2 = new Point(1, 1);

                Dictionary<Point, double> points = new Dictionary<Point, double>();

                while (true)
                {
                    int x, y;
                    Console.WriteLine("Введите координаты точки:");
                    Console.Write("X: ");
                    x = int.Parse(Console.ReadLine());
                    Console.Write("Y: ");
                    y = int.Parse(Console.ReadLine());

                    Point p = new Point(x, y);
                    if (!points.ContainsKey(p))
                    {
                        points.Add(p, Distanse(p));
                    }
                    Console.WriteLine($"Расстояние от точки ({p.x}, {p.y}) = {points[p]}");

                    if (x == 0 && y == 0) 
                    {
                        break;
                    }
                }
            }

            {
                Random rand = new Random();

                List<Point> points = new List<Point>(15);
                for (int i = 0; i < 15; ++i)
                {
                    points.Add(
                        new Point(rand.Next(-2, 2),
                                    rand.Next(-2, 2))
                    );
                }

                Dictionary<int, List<Point>> groupPointByX = points.GroupBy(GroupPointByX).ToDictionary(
                   (group) => group.Key,
                   group => 
                   {
                       return group.Select(item => item).ToList();
                   }
                );

                groupPointByX = points.GroupBy(GroupPointByAsinX).ToDictionary(
                   (group) => group.Key,
                   group => {
                       return group.Select(item => item).ToList();
                   }
                );
                Console.WriteLine(groupPointByX);
            }

            {
                Dictionary<int, string> translate = new Dictionary<int, string> {
                    { 0, "ноль" },
                    { 1, "один" },
                    { 2, "два" },
                };

                /*
                List<KeyValuePair<int, string>> d = new List<KeyValuePair<int, string>>();
                Dictionary<int, string> dd = new Dictionary<int, string>(d);
                */

                translate.Add(3, "три");
                translate.Add(4, "четыре");
                translate.Add(5, "пять");

                while (true)
                {
                    Console.Write("Введите цифру: ");
                    int change = int.Parse(Console.ReadLine());

                    // ContainsKey - проверка на наличие ключа в словаре 
                    if (translate.ContainsKey(change))
                    {
                        Console.WriteLine($"{change} - {translate[change]}");
                    }
                    else
                    {
                        Console.WriteLine($"Нет перевода для '{change}'");
                        Console.Write("Добавить?(Y/N): ");
                        string changeAdd = Console.ReadLine();
                        if (changeAdd == "Y")
                        {
                            Console.Write("Укажите перевод: ");
                            string value = Console.ReadLine();
                            // translate.Add(change, value);
                            translate[change] = value;
                        }
                        else { break; }
                    }
                }


                Console.ReadKey();
            }

            // SortedSet - двоичное дерево, хранящая только уникальные значения коллекции в отсортированном виде
            {
                // Коллекция уникальный значений в формате дерева
                SortedSet<int> tree = new SortedSet<int>();
                tree.Add(2);
                tree.Add(3);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(2);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(3);
                tree.Add(2);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(3);

                foreach (int value in tree)
                {
                    Console.WriteLine(value);
                }
            }

            {
                // Коллекция уникальный значений в формате дерева
                HashSet<int> tree = new HashSet<int>();
                tree.Add(2);
                tree.Add(3);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(2);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(3);
                tree.Add(2);
                tree.Add(1);
                tree.Add(2);
                tree.Add(3);
                tree.Add(3);

                foreach (int value in tree)
                {
                    Console.WriteLine(value);
                }
            }

            // Queue - Очередь
            // FIFO - First In First Out - первый зашел - первый вышел
            {
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(4); // Добавить элемент в конец очереди
                queue.Dequeue(); // Удалить элемент из начала
            }

            // Stack - список, работающий только на двух операциях добавление и удаления элентов из начала
            // LIFO - Last In - First Out - последний зашел - первый вышел
            {
                Stack<int> stack = new Stack<int>();

                stack.Push(3); // Добавить элемент в начало
                stack.Pop(); // удалить элемент из начала

                Stack<List<int>> data = new Stack<List<int>>();
                List<int> list = new List<int>(15);

                Random random = new Random();
                for (int i = 0; i < 15; ++i)
                {
                    list.Add(random.Next(0, 20));
                }
                list.Sort();
                data.Push(list);

                int targetValue = random.Next(0, 20);
                while (data.Count != 0)
                {
                    List<int> target = data.Pop();

                    if (target.Count > 0)
                    {
                        int middleIndex = target.Count / 2;
                        int middle = target[middleIndex];
                        if (targetValue < middle)
                        {
                            data.Push(target.GetRange(0, middleIndex));
                        }
                        else if (targetValue > middle)
                        {
                            data.Push(target.GetRange(middleIndex, middleIndex));
                        }
                        else
                        {
                            Console.WriteLine($"Элемент {targetValue} на позиции {middleIndex}");
                        }
                    }

                }
            }

            // LinkedList - двусзясный список
            {
                LinkedList<int> list = new LinkedList<int>();
                list.AddLast(1);
                list.AddLast(2);
                list.AddLast(3);

                list.AddFirst(4);
                list.AddFirst(5);
                list.AddFirst(6);
                // 6 5 4 1 2 3

                LinkedListNode<int> v1 = list.Find(1);
                list.AddAfter(v1, 100);

                // Имеет конструктор копирования
                // как и любой другой конструктор копирования коллекций принимаем в себя
                // абстракцию над любой коллекций
                //  Интрерфейс IEnumerable
                LinkedList<int> copy = new LinkedList<int>(list);

                List<int> copyArray = new List<int>(copy);

                List<int> intersect = copy.Intersect(copyArray).ToList();
            }

            // Динамический массив - List
            /**
             *  Есть 3 конструктора
             *      1. По умолчанию - создающий пустую коллекцию
             *      2. Принимающий capacity - 'Вместимость' коллекци
             *          Вместимость - это объем памяти выделенный под коллекцию(неконечный)
             *      3. Принимающий другую коллекцию - конструктор копирования
             */
            {

                int size = 3;
                List<int> collection = new List<int>(size);
                GetInfo(collection);
                Console.WriteLine("\n===============\n");

                for (int i = 0; i < 5; ++i)
                {
                    // Add - Добавление элемента в конец
                    collection.Add(i);
                    GetInfo(collection);
                    Console.WriteLine("\n===============\n");
                }

                List<int> copy = new List<int>(collection);
                GetInfo(copy);

                // Добавить в массив не один элемент, а сразу все элементы другой коллекции
                copy.AddRange(collection);

                Console.ReadKey();
            }

            /*
            *  1. В массиве на 20 элементов, заполненым случаным образом, найти первый элемент, который больше минимального
            *  2. В списке на 10, заполненым случаным образом, добавить после каждоый 3(тройки) одну 7(семерку)
            *  
            *  3. В массиве на 10 элементов, заполненым случаным образом, посчитать сумму уникальный значений
            *      1 2 2 2 2 2 2 -> сумма = 3
            */
            {
                Random rnd = new Random();
                //1
                int[] array = new int[20];
                for (int i = 0; i < 20; i++) array[i] = rnd.Next(1, 100);
                int min = array.Min();
                int result = array.FirstOrDefault(x => x > min);

                Console.WriteLine($"Минимум: {min}, Первый элемент больше минимума: {result}");

                //2
                LinkedList<int> linkedList = new LinkedList<int>();
                for (int i = 0; i < 10; i++) linkedList.AddLast(rnd.Next(1, 10));

                var currentNode = linkedList.First;
                while (currentNode != null)
                {
                    if (currentNode.Value == 3)
                    {
                        linkedList.AddAfter(currentNode, 7);
                        currentNode = currentNode.Next;
                    }
                    currentNode = currentNode.Next;
                }

                //3
                int[] numbers = new int[10];
                for (int i = 0; i < 10; i++) numbers[i] = rnd.Next(1, 100);
                HashSet<int> uniqueValues = new HashSet<int>(numbers);

                int sum = 0;
                foreach (int value in uniqueValues)
                {
                    sum += value;
                }

                Console.WriteLine($"Сумма уникальных: {sum}");
            }

        }
    }
}
#endif //Main