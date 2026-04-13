namespace _13._04
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            {
                float fv = 3.2f;
                double dv = 3.2;

                byte dyv = 2;
                short sv = 2;
                int iv = 2;
                long lv = 2;

                ushort usv = 2;
                uint uiv = 2;
                ulong ulv = 2;
            }

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
                for(int i = 0; i < 10; ++i)
                {
                    Console.WriteLine($"[{i}]");
                }

                while(false) { }

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

                foreach(int v in mas)
                {
                    Console.WriteLine(v);
                }

                int[,] sqeaMatrix = new int[5, 2];
                for(int i = 0; i < sqeaMatrix.GetLength(0); ++i)
                {
                    for(int j = 0; j < sqeaMatrix.GetLength(1); ++j)
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
                for(int i = 0; i < stepsMatrix.Length; ++i)
                {
                    stepsMatrix[i] = new int[i + 1];
                }

                for(int i = 0; i < stepsMatrix.Length; ++i)
                {
                    for(int j = 0; j < stepsMatrix[i].Length; ++j)
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
        }
        
    }
}
