//#define MAIN

#if MAIN
using System.Numerics;

namespace _08._06
{
    class NumberCompare<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T? x, T? y)
        {
            if (!(x is null || y is null))
            {
                return x.CompareTo(y);
            }
            return -1;
        }
    }

    internal class Program
    {
        static void Sort<T>(IList<T> collection, NumberCompare<T> compare) where T : IComparable<T>
        {
            for (int i = 1; i < collection.Count; ++i)
            {
                for (int j = 0; j < collection.Count - i; ++j)
                {
                    if (compare.Compare(collection[j], collection[j + 1]) < -1)
                    {
                        T temp = collection[j];
                        collection[j] = collection[j + 1];
                        collection[j + 1] = temp;
                    }
                }
            }
        }

        delegate bool CompareCommonDelegate<T>(T x, T y);
        // delegate(делегат) - это специал класс/тип для функций
        delegate bool CompareNumberDelegate<T>(T x, T y) where T : INumber<T>;
        static void Sort<T>(IList<T> collection, CompareCommonDelegate<T> comparable)
        {
            for (int i = 1; i < collection.Count; ++i)
            {
                for (int j = 0; j < collection.Count - i; ++j)
                {
                    if (comparable(collection[j], collection[j + 1]))
                    {
                        T temp = collection[j];
                        collection[j] = collection[j + 1];
                        collection[j + 1] = temp;
                    }
                }
            }
        }

        static bool IsFirstLess(int a, int b) => a < b;

        static bool IsFirstMore(int a, int b) => a > b;

        delegate bool Matcher<T>(T item);
        static T? Find<T>(IList<T> collection, Matcher<T> match)
        {
            foreach (var item in collection)
            {
                if (match(item))
                {
                    return item;
                }
            }
            return default;
        }
        static bool IsEven(int x) => x % 2 == 0;
        static bool IsGreaterThanFour(int x) => x > 4;

        static void Main()
        {
            {
                List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };

                // Поиск первого чётного числа
                int? firstEven = Find(numbers, IsEven);
                Console.WriteLine(firstEven);

                // Поиск первого числа больше 4
                int? firstGreaterThanFour = Find(numbers, IsGreaterThanFour);
                Console.WriteLine(firstGreaterThanFour); 
            }

            {
                List<int> g = new List<int>();
                Sort(g, new NumberCompare<int>());
                Sort(g, IsFirstLess);
                Sort(g, IsFirstMore);
            }
        }
    }
}
#endif //MAIN