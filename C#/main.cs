//#define Main
using System;

#if Main 
namespace _15._04
{
    // interface - описания возможностей класса

    /*
     * interface INameInterface {
     * }
     */
    interface IMoveble2D
    {
        // Модификаторы доступа не прописываются, т.к они тут по умолчанию все должны быть public
        // реализацию так же дать нельзя
        void Move(int x, int y);
    }

    interface IMoveble3D
    {
        void Move(int x, int y, int z);
    }

    class NameClass
    {
        public int x;
        protected string y;
        private float t;
        /*private*/
        double g;

        public const int version = 2;
    }

    class Circle
    {
        // readonly - модификатор, который обозначает поле, как доступное только на чтение
        // НО один раз ему можно указать значение - только в конструкторе
        public readonly int r;

        public Circle(int r)
        {
            this.r = r;
        }
    }

    class Point2D : IMoveble2D
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

        public Point2D() { }
        public Point2D(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

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

        public virtual double Distanse(Point2D other)
        {
            return Math.Sqrt(Math.Pow(x - other.x, 2) + Math.Pow(y - other.y, 2));
        }

        public int getX() => x;
        public int getY()
        {
            return y;
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Point3D : Point2D, IMoveble3D
    {
        int z;

        public Point3D() { }

        public Point3D(int x, int y, int z) : base(x, y)
        {
            this.z = z;
        }

        public int Z
        {
            get => z; set => z = value;
        }

        public void Move(int x, int y, int z)
        {
            X = x;
            Y = y;
            this.z = z;
        }
    }

    partial class TempPartial
    {
        int field1, field2, field3;
    }

    partial class TempPartial
    {
        public int Sum()
        {
            return field1 + field2 + field3;
        }
    }

    abstract class TempVirtual
    {
        // Виртуальный метод объязан иметь своё собственное опредение
        public virtual void Virtual() { }

        // Абстрактный метод не должен иметь своё определение,
        // Но так же, если в классе есть хотя бы один абстрактный метод, то весь класс так же должен быть
        // отмечен как абстрактный
        public abstract void Abstract();
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
            get
            {
                return h;
            }
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

        static double GetDistanse(Point2D a, Point2D b)
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
            Point3D point3D = new Point3D { X = 1, Y = 3, Z = 2 };
            Console.WriteLine($"X = {point3D.X}, Y = {point3D.Y}, Z = {point3D.Z}");

            Circle circle = new Circle(2);

            // Ошибка - работа с полей, обозначенным как readonly, на запись
            // circle.r = 2;

            Point2D p1 = new Point2D();
            p1.X = 10; p1.Y = 20;
            Point2D p2 = new Point2D { X = 20, Y = 10 };

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