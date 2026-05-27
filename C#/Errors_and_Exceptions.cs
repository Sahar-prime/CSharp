//#define MAIN

#if MAIN
using System.Globalization;
using System.Numerics;

namespace _27._05
{

    class ConvertStringToNumberException : FormatException
    {
        public string Target { get; protected set; }
        public Type ConvertType { get; protected set; }


        public ConvertStringToNumberException(
            string? message,
            string target,
            Type convertType,
            Exception? innerException) : base(message, innerException)
        {
            Target = target;
            ConvertType = convertType;
        }

        public ConvertStringToNumberException(string target, Type convertType, Exception? innerException)
            : this(
                $"Ошибка: невозможно сконвертировать '{target}' к типу {convertType.FullName}",
                target, convertType, innerException)
        { }

        public ConvertStringToNumberException(string target, Type convertType)
            : this(target, convertType, null) { }


        public ConvertStringToNumberException(string? message, string target, Type convertType)
            : this(message, target, convertType, null) { }
    }
    internal class Program
    {
        static T ReadLineNumber<T>(TextReader In) where T : INumber<T>, IMinMaxValue<T>
        {
            string minValueText = T.MinValue.ToString();
            string maxValueText = T.MaxValue.ToString();
            string target = In.ReadLine();

            if (string.IsNullOrWhiteSpace(target))
                throw new ConvertStringToNumberException(target, typeof(T));

            if (!T.TryParse(target, NumberStyles.Number, null, out T parsedValue))
                throw new ConvertStringToNumberException(target, typeof(T));

            if (parsedValue < T.MinValue || parsedValue > T.MaxValue)
                throw new ConvertStringToNumberException(
                    $"Ошибка: значение '{target}' выходит за пределы диапазона типа {typeof(T).Name}.",
                    target,
                    typeof(T)
                );

            if (target.CompareTo(minValueText) < 0 || target.CompareTo(maxValueText) > 0)
                throw new ConvertStringToNumberException(
                    $"Ошибка: значение '{target}' выходит за пределы диапазона типа {typeof(T).Name}.",
                    target,
                    typeof(T)
                );

            return parsedValue;
        }

        static T ReadLineNumber<T>() where T : INumber<T>, IMinMaxValue<T> => ReadLineNumber<T>(Console.In);

        static T Divizion<T>(T a, T b) where T : INumber<T>
        {
            if (b != T.Zero)
            {
                return a / b;
            }
            // throw(англ. Бросить) - блок в котором мы указываем что дальше пойдет исключение
            // Exception(фнгл. Исключение) - специальный класс, который прекрашает действие программы в случаех если его не обработать
            throw new DivideByZeroException("Ошибка деления на 0");
        }

        /**
         *  Ошибка - Проблема известная на этапе компиляции
         *      Компиляция - это процесс перерода текста программы на язык компьтера(машиный код)
         *  Исключение - Проблема которая случается в рантайме
         *      рантайм - момент когда программа работает
         */
        static void Main()
        {
            try
            {
                int number = ReadLineNumber<int>();
                Console.WriteLine($"Вы ввели корректное число: {number}");
            }
            catch (ConvertStringToNumberException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }


            int a, b;
            a = int.Parse(Console.ReadLine());

            b = int.Parse(Console.ReadLine());

            if (b != 0)
            {
                Console.WriteLine(a / b);
            }
            else
            {
                Console.WriteLine("Divizion by zero");
            }

            // try(англ. Попробовать) - ключевое слово, которое объявляет блок, в котором программа должна отлавливать вызваные исключения
            try
            {
                int result = Divizion(a, b);
                Console.WriteLine($"{a} : {b} = {result}");

                string number = "111!";
                Console.WriteLine(int.Parse(number) + 1);

            }
            // catch(англ. Отловить) - блок, который вызывается, в случае, если в соседнем try было вызвано исключение
            // catch - можно перегружать, т.е создать разные обработчики для разный искючений
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"{ex.Message} | MAYBE: {a}/{b} = {(a > 0 ? "Inf" : "-Inf")}");
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