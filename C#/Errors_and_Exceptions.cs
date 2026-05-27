//#define MAIN

#if MAIN
using System.Numerics;

namespace C_
{
    internal class Program
    {
        static T Divizion<T>(T a, T b) where T : INumber<T> 
        {
            if (b != T.Zero) 
            {
                return a / b;
            }
            // throw(бросить) - блок, в котором мы указываем, что дальше пойдет исключение
            // Exception(Исключение) - специальный класс, который прекращает действие программы в случии, если его не обработать
            throw new DivideByZeroException("Ошибка: Деление на 0");
        }

        /*
         * Ошибка - проблема известная на этапе компиляции
         *      Компиляция - это процесс перевода текста программы в машинный код(язык компьютера)
         * Исключение - проблема, которая случается в рантайме
         *      Рантайм - момент, когда программа работает
         */
        static void Main()
        {
            int a, b;
            a = int.Parse(Console.ReadLine());
            b = int.Parse(Console.ReadLine());

            if (b != 0)
            {
                Console.WriteLine(a / b);
            }
            else
            {
                Console.WriteLine("Divizion by zero!");
            }

            //try - ключевое слово, которое объявляет блок, в котором программа должна отлавливать вызванные исключения
            try
            {
                int res = Divizion(a, b);
                Console.WriteLine($"{a} : {b} = {res}");

                string number = "111!";
                Console.WriteLine(int.Parse(number) + 1);
            }
            // catch - блок, который вызывается в случае, если в соседнем try было вызвано исключение
            // catch можно перегружать, т.е. создать разные обработки для разных исключений
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"{ex.Message} | MAYBE: {a}:{b}={(a > 0 ? "Inf" : "-Inf")}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"FormatException: {ex.Message}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Unknown Exception: {ex.Message}");
            }
        }
    }
}
#endif //MAIN