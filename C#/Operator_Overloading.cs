//#define MAIN

#if MAIN
using System.Numerics;

namespace _01._06
{
    class Decimal
    {
        uint down;
        int up;

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

        public override string ToString()
        {
            return $"{up} / {down}";
        }
    }

    internal class Program
    {
        static T Division<T>(T a, T b) where T : INumber<T>
        {
            if (b != T.Zero)
                return a / b;
            throw new Exception();
        }

        static void Main()
        {
            Decimal first = new Decimal(1, 2), second = new Decimal(5, 2);
            Console.WriteLine($"{first} + 7 = {first + 7}");
            Console.WriteLine($"{first} + {second} = {first + second}");
            Console.WriteLine($"{first} - {second} = {first - second}");
            Console.WriteLine($"{first} * {second} = {first * second}");
            Console.WriteLine($"{first} : {second} = {first / second}");
            Console.WriteLine();
            Console.WriteLine($"{first} - 3 = {first - 3}");
            Console.WriteLine($"7 - {first} = {7 - first}");
            Console.WriteLine($"{first} * 4 = {first * 4}");
            Console.WriteLine($"5 * {first} = {5 * first}");
            Console.WriteLine($"{first} : 2 = {first / 2}");
            Console.WriteLine($"10 : {first} = {10 / first}");
        }
    }
}
#endif //MAIN