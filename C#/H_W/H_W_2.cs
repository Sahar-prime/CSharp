//#define H_W_2
using System;

#if H_W_2
//Задание 1
public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public string EmailAddress { get; set; }

    public EmailSender(string emailAddress)
    {
        EmailAddress = emailAddress;
    }

    public void Send(string message)
    {
        Console.WriteLine($"Отправка {message} на адрес {EmailAddress}");
    }
}

public class SmsSender : IMessageSender
{
    public string PhoneNumber { get; set; }

    public SmsSender(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void Send(string message)
    {
        Console.WriteLine($"Отправка {message} на номер {PhoneNumber}");
    }
}

//Задание 2
public class Rectangle
{
    public readonly int Width;
    public readonly int Height;

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Area => Width * Height;
}

//Задание 3
public interface ISwitchable
{
    void TurnOn();
    void TurnOff();
}

public interface IDimmable : ISwitchable
{
    void SetBrightness(int level);
}

public class SmartLamp : IDimmable
{
    private int _currentBrightness;

    public int CurrentBrightness
    {
        get => _currentBrightness;
        set
        {
            if (value < 0)
                _currentBrightness = 0;
            else if (value > 100)
                _currentBrightness = 100;
            else
                _currentBrightness = value;
        }
    }

    public void TurnOn()
    {
        CurrentBrightness = 100;
        Console.WriteLine("Лампа включена.");
    }

    public void TurnOff()
    {
        CurrentBrightness = 0;
        Console.WriteLine("Лампа выключена.");
    }

    public void SetBrightness(int level)
    {
        CurrentBrightness = level;
        Console.WriteLine($"Яркость установлена на {CurrentBrightness}%");
    }
}

//Задание 4
public class BankAccount
{
    private decimal _balance;
    public readonly string AccountNumber;

    public decimal Balance => _balance;

    public BankAccount(string accountNumber, decimal initialBalance = 0)
    {
        AccountNumber = accountNumber;
        _balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            _balance += amount;
        else
            Console.WriteLine("Сумма депозита должна быть положительной.");
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Сумма снятия должна быть положительной.");
            return;
        }

        if (_balance >= amount)
            _balance -= amount;
        else
            Console.WriteLine("Недостаточно средств на счёте.");
    }
}

class Program
{
    static void Main()
    {
        // Задание 1
        IMessageSender emailSender = new EmailSender("user@example.com");
        emailSender.Send("Привет!");

        IMessageSender smsSender = new SmsSender("+1234567890");
        smsSender.Send("Привет!");

        // Задание 2
        Rectangle rectangle = new Rectangle(5, 10);
        Console.WriteLine($"Площадь прямоугольника: {rectangle.Area}");

        // Задание 3
        SmartLamp lamp = new SmartLamp();
        lamp.TurnOn();
        lamp.SetBrightness(50);
        lamp.TurnOff();

        // Задание 4
        BankAccount account = new BankAccount("123456789", 1000);
        account.Deposit(500);
        account.Withdraw(200);
        Console.WriteLine($"Баланс: {account.Balance}");
    }
}
#endif //H_W_2