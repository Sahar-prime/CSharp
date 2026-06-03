//#define MAIN

#if MAIN
using System.Numerics;

namespace _01._06
{
    // Обычная дробь (a / b)
    class Decimal
    {
        public uint down;
        public int up;

        public Decimal(int up, uint down)
        {
            this.up = up;
            this.down = down;
        }

        static public Decimal operator +(Decimal first, Decimal second)
        {
            Decimal result = new Decimal(
                (int)(first.up * second.down + first.down * second.up),
                first.down * second.down);
            return result;
        }
        static public Decimal operator +(Decimal first, int second)
            => first + new Decimal(second, 1);
        static public Decimal operator +(int first, Decimal second)
            => second + first;

        static public Decimal operator -(Decimal first, Decimal second)
            => first + new Decimal(-second.up, second.down);
        static public Decimal operator -(Decimal first, int second)
            => first - new Decimal(second, 1);
        static public Decimal operator -(int first, Decimal second)
            => new Decimal(first, 1) - second;

        static public Decimal operator *(Decimal first, Decimal second)
        {
            return new Decimal(first.up * second.up, first.down * second.down);
        }
        static public Decimal operator *(Decimal first, int second)
            => first * new Decimal(second, 1);
        static public Decimal operator *(int first, Decimal second)
            => second * first;

        static public Decimal operator /(Decimal first, Decimal second)
        {
            return first * new Decimal((int)(Math.Sign(second.up) * second.down), (uint)(Math.Abs(second.up)));
        }
        static public Decimal operator /(Decimal first, int second)
            => first / new Decimal(second, 1);
        static public Decimal operator /(int first, Decimal second)
            => new Decimal(first, 1) / second;

        static public bool operator !=(Decimal first, Decimal second)
            => !(first == second);
        static public bool operator ==(Decimal first, Decimal second)
            => first.Equals(second);

        static public bool operator <(Decimal first, Decimal second)
            => !(first >= second);
        static public bool operator <=(Decimal first, Decimal second)
            => !(first > second);

        static public bool operator >=(Decimal first, Decimal second)
            => first > second || first == second;
        static public bool operator >(Decimal first, Decimal second)
            => first.up * second.down > second.up * first.down;

        public enum Parts
        {
            NUMERATOR,  // 0
            DENOMINATOR // 1
        }

        public int this[Parts index]
        {
            get => index == Parts.NUMERATOR ? up : (int)down;
            set
            {
                if (index == Parts.NUMERATOR)
                {
                    up = value;
                }
                else
                {
                    down = (uint)value;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Decimal other)
            {
                return Math.Abs(this.up / this.down - other.up / other.down) < double.Epsilon;
            }
            return false;
        }

        public override string ToString()
            => $"{up} / {down}";
    }

    class Point2D
    {
        public int x, y;
        public Point2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point2D() { }

        public Point2D(Point2D other) : this(other.x, other.y) { }
    }

    class Point3D : Point2D
    {
        public int t;

        public Point3D(int x, int y, int t) : base(x, y)
        {
            this.t = t;
        }

        public Point3D() : base() { }

        public Point3D(Point3D other) : this(other.x, other.y, other.t) { }

        public Point3D(Point2D other) : this(other.x, other.y, 0) { }

        /**
         *  implicit - оператор неявного преобразования 
         *      public static implicit operator TARGET_TYPE(ORIGINAL_TYPE value)
         *      
         *      TARGET_TYPE target = value; // где тип данных переменной value = ORIGINAL_TYPE
         *  explicit - оператор явного преобразования
         *      public static explicit operator TARGET_TYPE(ORIGINAL_TYPE value)
         *      
         *      TARGET_TYPE target = (TARGET_TYPE)value; // где тип данных переменной value = ORIGINAL_TYPE
         */
        public static implicit operator Point3D(int x)
        {
            return new Point3D(x, 0, 0);
        }
    }

    class Poligon
    {
        List<Point2D> points = new List<Point2D>();

        public Point2D this[int index]
        {
            get => new Point2D(points[index]);
            set => points[index] = new Point2D(value);
        }
    }

    class PoligonArray : List<Point2D>
    {

    }

    internal class Program
    {
        static T Division<T>(T a, T b) where T : INumber<T>
        {
            if (b != T.Zero)
                return a / b;
            throw new Exception();
        }

        static void Main(string[] args)
        {
            {
                Point3D point3D = new Point3D(1, 1, 1);
                Point2D point2D = new Point2D(point3D);
                Point3D copyPoint2D = new Point3D(point2D);
                Point3D point3D1 = 3;
            }

            {
                List<Decimal> decimals = new List<Decimal>();
                Random random = new Random();
                for (int i = 0; i < 15; ++i)
                {
                    Decimal @decimal = new Decimal(random.Next(10, 25), (uint)random.Next(5, 30));
                    decimals.Add(@decimal);

                    Console.WriteLine($"{@decimal} = {@decimal.up / (float)@decimal.down}");
                }

                Decimal @decimalMax = decimals.First();
                foreach (Decimal @decimal in decimals)
                {
                    if (@decimal > @decimalMax) @decimalMax = @decimal;
                }
                Console.WriteLine($"MAX = {decimalMax}");

                //Минимальное значение
                Decimal @decimalMin = decimals.First();
                foreach (Decimal @decimal in decimals)
                {
                    if (@decimal < @decimalMin) @decimalMin = @decimal;
                }
                Console.WriteLine($"MIN = {decimalMin}");

                //Сумма значений дробей
                Decimal sum = new Decimal(0, 1); //Начальная точка
                foreach (Decimal @decimal in decimals)
                {
                    sum += @decimal;
                }
                Console.WriteLine($"SUM = {sum}");

                Console.WriteLine();
            }

            {
                Decimal first = new Decimal(1, 2), second = new Decimal(5, 2), third = new Decimal(12, 24);
                Console.WriteLine($"{first} + 7 = {first + 7}");
                Console.WriteLine($"{first} + {second} = {first + second}");
                Console.WriteLine($"{first} - {second} = {first - second}");
                Console.WriteLine($"{first} * {second} = {first * second}");
                Console.WriteLine($"{first} : {second} = {first / second}");
                Console.WriteLine($"{second} == {third} -> {second == third}");
                Console.WriteLine();
                Console.WriteLine($"{first} - 3 = {first - 3}");
                Console.WriteLine($"7 - {first} = {7 - first}");
                Console.WriteLine($"{first} * 4 = {first * 4}");
                Console.WriteLine($"5 * {first} = {5 * first}");
                Console.WriteLine($"{first} : 2 = {first / 2}");
                Console.WriteLine($"10 : {first} = {10 / first}");
                Console.WriteLine();
                Console.WriteLine($"{first[Decimal.Parts.NUMERATOR]} / {first[Decimal.Parts.DENOMINATOR]}");
            }
        }
    }
}
#endif //MAIN