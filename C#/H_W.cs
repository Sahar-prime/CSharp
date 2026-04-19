//#define H_W
using System;
using System.Globalization;

#if H_W
class Program
{
    // Задание 1: Fizz Buzz
    static void FizzBuzz()
    {
        Console.WriteLine("\n=== Задание 1: Fizz Buzz ===");
        Console.Write("Введите число от 1 до 100: ");
        int number = int.Parse(Console.ReadLine());

        if (number < 1 || number > 100)
        {
            Console.WriteLine("Ошибка: число должно быть в диапазоне от 1 до 100.");
            return;
        }

        if (number % 15 == 0) Console.WriteLine("Fizz Buzz");
        else if (number % 3 == 0) Console.WriteLine("Fizz");
        else if (number % 5 == 0) Console.WriteLine("Buzz");
        else Console.WriteLine(number);
    }

    // Задание 2: Процент от числа
    static void CalculatePercentage()
    {
        Console.WriteLine("\n=== Задание 2: Процент от числа ===");
        Console.Write("Введите значение: ");
        double value = double.Parse(Console.ReadLine());
        Console.Write("Введите процент: ");
        double percent = double.Parse(Console.ReadLine());
        Console.WriteLine($"Результат: {value * percent / 100}");
    }

    // Задание 3: Формирование числа из цифр
    static void FormNumberFromDigits()
    {
        Console.WriteLine("\n=== Задание 3: Формирование числа из цифр ===");
        Console.Write("Введите первую цифру: ");
        int d1 = int.Parse(Console.ReadLine());
        Console.Write("Введите вторую цифру: ");
        int d2 = int.Parse(Console.ReadLine());
        Console.Write("Введите третью цифру: ");
        int d3 = int.Parse(Console.ReadLine());
        Console.Write("Введите четвёртую цифру: ");
        int d4 = int.Parse(Console.ReadLine());
        Console.WriteLine($"Сформированное число: {d1}{d2}{d3}{d4}");
    }

    // Задание 4: Обмен цифр в шестизначном числе
    static void SwapDigitsInNumber()
    {
        Console.WriteLine("\n=== Задание 4: Обмен цифр в шестизначном числе ===");
        Console.Write("Введите шестизначное число: ");
        string numStr = Console.ReadLine();

        if (numStr.Length != 6)
        {
            Console.WriteLine("Ошибка: число должно быть шестизначным.");
            return;
        }

        Console.Write("Введите номер первого разряда (1-6): ");
        int pos1 = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Введите номер второго разряда (1-6): ");
        int pos2 = int.Parse(Console.ReadLine()) - 1;

        char[] digits = numStr.ToCharArray();
        char temp = digits[pos1];
        digits[pos1] = digits[pos2];
        digits[pos2] = temp;
        Console.WriteLine($"Результат: {new string(digits)}");
    }

    // Задание 5: Определение сезона и дня недели
    static void GetSeasonAndDayOfWeek()
    {
        Console.WriteLine("\n=== Задание 5: Определение сезона и дня недели ===");
        Console.Write("Введите дату в формате dd.MM.yyyy: ");

        if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            // Определение сезона
            string season;
            int month = date.Month;
            if (month == 12 || month == 1 || month == 2)
                season = "Winter";
            else if (month >= 3 && month <= 5)
                season = "Spring";
            else if (month >= 6 && month <= 8)
                season = "Summer";
            else
                season = "Autumn";

            // Определение дня недели
            string dayOfWeek = date.ToString("dddd", CultureInfo.GetCultureInfo("en-US"));

            Console.WriteLine($"{season} {dayOfWeek}");
        }
        else
        {
            Console.WriteLine("Ошибка: неверный формат даты.");
        }
    }

    // Задание 6: Конвертация температуры
    static void ConvertTemperature()
    {
        Console.WriteLine("\n=== Задание 6: Конвертация температуры ===");
        Console.Write("Введите температуру: ");
        double temp = double.Parse(Console.ReadLine());
        Console.Write("Введите направление (1 - F→C, 2 - C→F): ");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
            Console.WriteLine($"Результат: {(temp - 32) * 5 / 9:F2}");
        else if (choice == 2)
            Console.WriteLine($"Результат: {temp * 9 / 5 + 32:F2}");
        else
            Console.WriteLine("Ошибка: неверный выбор.");
    }

    // Задание 7: Четные числа в диапазоне
    static void PrintEvenNumbersInRange()
    {
        Console.WriteLine("\n=== Задание 7: Четные числа в диапазоне ===");
        Console.Write("Введите начало диапазона: ");
        int start = int.Parse(Console.ReadLine());
        Console.Write("Введите конец диапазона: ");
        int end = int.Parse(Console.ReadLine());

        if (start > end)
        {
            int temp = start;
            start = end;
            end = temp;
        }

        Console.WriteLine($"Четные числа от {start} до {end}:");
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
                Console.Write($"{i} ");
        }
        Console.WriteLine();
    }

    // Главный метод
    static void Main()
    {
        FizzBuzz();
        CalculatePercentage();
        FormNumberFromDigits();
        SwapDigitsInNumber();
        GetSeasonAndDayOfWeek();
        ConvertTemperature();
        PrintEvenNumbersInRange();
    }
}
#endif //H_W