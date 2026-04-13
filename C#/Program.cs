using System;

namespace _13._04
{
    internal class Program 
    {
        static void Main(string[] args) 
        {
            /*
             * Числовые
             * 1.Целые
             *      byte //1 байт
             *      short //2 байта
             *      int //4 байта
             *      long //8 байт
             *      ---
             *      Безнаковые(неотрицательные)
             *      ushort
             *      uint
             *      ulong
             * 2.Дробные
             *      float //4 байта
             *      double //8 байт
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
             * 1.Логические
             *  bool - TRUE/FALSE
             *      TRUE != 1 <-> 1 != FALSE
             *      FALSE != 0 <-> 0 != FALSE
             * 2.Строкове(примитивы)
             *      1.string - строка любой длины
             *      2.char - один символ (строго символ, а не его код)
            */
            {
                string sv = "Hello World!";
                char cv = 'a';

                // Шаблоны строк, если строку начать с символа "$", то тогда можно подставляет в нее
                sv = $"переменая CV = {cv}, SV = {sv.ToUpper()}";
                Console.WriteLine(sv);

                //Математические/логические операции аналогично С/С++
                {
                    short x = 2;
                    short y = 3;
                    int r = x + y;

                    bool v = x >= y || y % 2 == 0;
                }
                //if - условные операторы
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
                        case 0: { } break;
                        case 1: { } break;
                        default: break;
                    }

                    string name = "Nikolay";
                    switch (name) 
                    {
                        case "Nilolay": { } break;
                    }
                }
            }
        }
    }
}