//#define Main
using System.Linq;
using System;

#if Main
namespace _13._04
{
    internal class Program
    {
        static int Sqea(int x = 0) => x * x;
        /*
        {
            return x * x;
        }
        */

        static void Temp(ref int x)
        {
            x = 4;
        }

        static void Mas(int[] mas, int min = 0, int max = int.MaxValue)
        {
            Random rand = new Random();
            for (int i = 0; i < mas.Length; ++i)
            {
                mas[i] = rand.Next(min, max);
            }
        }

        static void Mas(string[] mas, string[] items)
        {
            Random rand = new Random();
            for (int i = 0; i < mas.Length; ++i)
            {
                mas[i] = items[rand.Next(0, items.Length)];
            }
        }

        static void Mas(string[] mas, int minLength = 0, int maxLength = 255)
        {
            Random rand = new Random();
            for (int i = 0; i < mas.Length; ++i)
            {
                mas[i] = "";
                int randLength = rand.Next(minLength, maxLength);
                for (int j = 0; j < randLength; ++j)
                {
                    mas[i] += char.ConvertFromUtf32(rand.Next(0, 255));
                }
            }
        }

        static void PrintMas(int[] mas)
        {
            foreach (int i in mas)
            {
                Console.Write($"{i}\t");
            }
        }
        static void Main(string[] args)
        {
            {
                int[] mas = new int[10];
                Mas(mas, -10, 50);
                PrintMas(mas);
                Console.WriteLine();
            }

            {
                int x = 509;
                // передача значений по ссылке
                Temp(ref x);
                Console.WriteLine(x);
            }
            Console.ReadLine();

            /*
             * Числовые
             *  1. Целые
             *         byte,    // 1 байт
             *         short,   // 2 байта
             *         int,     // 4 байта
             *         long     // 8 байта
             *         ___
             *         Безноковые(неотрицательные)
             *         ushort
             *         uint
             *         ulong
             *  2. Дробные
             *      float       // 4 байта
             *      double      // 8 байт
             */
            /*{
                float fv = 3.2f;
                double dv = 3.2;

                byte dyv = 2;
                short sv = 2;
                int iv = 2;
                long lv = 2;

                ushort usv = 2;
                uint uiv = 2;
                ulong ulv = 2;
            }*/

            /**
             * 1. Логические
             *  bool - TRUE/FALSE 
             *      TRUE != 1 <-> 1 != TRUE
             *      FALSE != 0  <-> 0 != FALSE
             *      
             *  2. Строковые(примитивы)
             *      1. string   - строка любой длины
             *      2. char     - один символ(строго символ, а не его код)
             */
            {
                string sv = "Hello World!";
                char cv = 'a';

                // Шаблоны строк, Если строку начать с символа ($), то тогда можно подставлять в неё значения
                sv = $"перемена CV = {cv}, sv = {sv.ToUpper()}";
                Console.WriteLine(sv);
            }

            /*
             *  Математические/логические операции аналогично C/C++
             */
            {
                short x = 2;
                short y = 3;
                int r = x + y;

                bool v = x >= y || y % 2 == 0;
            }

            // if - условные оператора
            {
                int x = 2;
                int y = 3;
                if (x > y)
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }

                switch (x)
                {
                    case 0:
                        {

                        }
                        break;
                    case 1: { } break;
                    default: break;
                }
            }

            // циклы
            {
                for (int i = 0; i < 10; ++i)
                {
                    Console.WriteLine($"[{i}]");
                }

                while (false) { }

                do { } while (false);
            }

            // Статические массивы
            {
                int size = int.Parse(Console.ReadLine());
                int[] mas = new int[size];
                mas[0] = 3;
                Console.WriteLine($"[0] = {mas[0]}");
                Console.WriteLine($"Длина = ${mas.Length}");
                Console.WriteLine($"Маx = ${mas.Max()}");
                Console.WriteLine($"Min = ${mas.Min()}");
                Console.WriteLine($"Sum = ${mas.Sum()}");
                Console.WriteLine($"Avg = ${mas.Average()}");

                foreach (int v in mas)
                {
                    Console.WriteLine(v);
                }

                int[,] sqeaMatrix = new int[5, 2];
                for (int i = 0; i < sqeaMatrix.GetLength(0); ++i)
                {
                    for (int j = 0; j < sqeaMatrix.GetLength(1); ++j)
                    {
                        sqeaMatrix[i, j] = i * j;
                    }
                }

                for (int i = 0; i < sqeaMatrix.GetLength(0); ++i)
                {
                    for (int j = 0; j < sqeaMatrix.GetLength(1); ++j)
                    {
                        Console.Write($"{sqeaMatrix[i, j]}\t");
                    }
                    Console.WriteLine();
                }

                int[][] stepsMatrix = new int[5][];
                for (int i = 0; i < stepsMatrix.Length; ++i)
                {
                    stepsMatrix[i] = new int[i + 1];
                }

                for (int i = 0; i < stepsMatrix.Length; ++i)
                {
                    for (int j = 0; j < stepsMatrix[i].Length; ++j)
                    {
                        stepsMatrix[i][j] = i * j;
                    }
                }

                for (int i = 0; i < stepsMatrix.Length; ++i)
                {
                    for (int j = 0; j < stepsMatrix[i].Length; ++j)
                    {
                        Console.Write($"{stepsMatrix[i][j]}\t");
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();

            /**
             *  Ввести с клавиатуры размер массива
                    Дальше создать массив и заполнить его значениями(с клавиатуры)
                    1. Найти количество значений массива, которые больше среднего значение
                        этого же массива
             * 
             */
            // Ввод размера массива
            Console.Write("Введите размер массива: ");
            int s = int.Parse(Console.ReadLine());

            // Создание и заполнение массива
            double[] array = new double[s];
            for (int i = 0; i < s; i++)
            {
                Console.Write($"Введите элемент [{i}]: ");
                array[i] = double.Parse(Console.ReadLine());
            }

            // Вычисление среднего значения массива
            double sum = 0;
            foreach (double num in array)
            {
                sum += num;
            }
            double average = sum / s;

            // Подсчет количества элементов, которые больше среднего
            int countAboveAverage = 0;
            foreach (double num in array)
            {
                if (num > average)
                {
                    countAboveAverage++;
                }
            }

            // Вывод результата
            Console.WriteLine($"\nСреднее значение массива: {average}");
            Console.WriteLine($"Количество элементов, которые больше среднего: {countAboveAverage}");
        }

    }
}
#endif //Main