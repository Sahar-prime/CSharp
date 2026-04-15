//#define Main
using System;

#if Main 
namespace _15._04
{
    class NameClass
    {
        public int x;
        protected string y;
        private float t;
        /*private*/
        double g;
    }

    class Point
    {
        int x, y;

        // Синтаксический сахар - Специальная обертка над(для) языком, которая позволяет более удобно/компактно писать код
        // Синтаксис свойства C# - специальный синтаксис для методов get/set
        /*
         * ASSECC_LEVEL TYPE NAME_PROPERTY {
         *      ASSECC_LEVEL get {
         *          ...
         *      }
         *      
         *      ASSECC_LEVEL set {
         *          ...
         *      }
         * }
         */

        public int X
        {
            // public int getX() {return x;}
            get
            {
                return x;
            }
            /*protected*/
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get => y;
            /*protected*/
            set => y = value;
        }

        public double Distanse(Point other)
        {
            return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));
        }

        public int getX() => x;
        public int getY()
        {
            return y;
        }
    }

    /**
     *  Класс квадрат {Ширина, Высота} через свойства!
     *      Методы расчёта периметра и площади
     *      Создать 2 экземпляра и сравнить их по площади
     */
    class Square 
    {
        double h, w;

        public double H
        {
            // public int getX() {return x;}
            get
            {
                return h;
            }
            /*protected*/
            set
            {
                h = value;
            }
        }
        public double W
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }

        public double P() 
        {
            return 2 * w + 2 * h;
        }
        public double S()
        {
            return w * h;
        }
    }


    internal class Program
    {

        static void Temp(NameClass name)
        {
            name.x = 111;
        }

        static double GetDistanse(Point a, Point b)
        {
            return Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2)
            );
            /*
            return Math.Sqrt(
                Math.Pow(a.getX() - b.getX(), 2) + 
                Math.Pow(a.getY() - b.getY(), 2)
            );
            */
        }

        static void Main(string[] args)
        {
            Point p1 = new Point();
            p1.X = 10; p1.Y = 20;
            Point p2 = new Point { X = 20, Y = 10 };

            Console.WriteLine($"Расстояние = {GetDistanse(p1, p2)}");

            // Создана переменная типа NameClass, но не проинициплизовая
            // Все объекты классов, всегда, являются ссылками
            NameClass nameClass;

            nameClass = new NameClass();
            nameClass.x = -2;

            Console.WriteLine(nameClass.x);
            Temp(nameClass);
            Console.WriteLine(nameClass.x);

            //Square
            Square sq1 = new Square { H = 10, W = 20 };
            Square sq2 = new Square { H = 11, W = 7 };

            double S_1 = sq1.S();
            double S_2 = sq2.S();
            if (S_1 > S_2)
                Console.WriteLine($"Площадь первого ({S_1}) больше");
            else if (S_1 < S_2)
                Console.WriteLine($"Площадь второго ({S_2}) больше");
            else
                Console.WriteLine("Площади равны");
        }
    }
}
#endif //Main