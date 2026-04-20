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
        static void Main(string[] args)
        {
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
        }
    }
}
#endif //Main