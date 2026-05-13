//#define MAIN

#if MAIN
namespace _13._05
{
    class Color : IComparable<Color>
    {
        float wavelength;

        public float Wavelength
        {
            get => wavelength;
        }
        public Color(float wavelength)
        {
            this.wavelength = wavelength;
        }

        public int CompareTo(Color? other)
        {
            return wavelength.CompareTo(other.wavelength);
        }

        public override string ToString()
        {
            return $"λ = {wavelength}ns";
        }
    }

    interface IText
    {
        string GetText();
    }

    class Text : IText
    {
        string msg;

        public Text(string msg)
        {
            this.msg = msg;
        }

        public string GetText()
        {
            return msg;
        }
    }

    interface ITextDecorator : IText { }

    class UpperCaseText<T> : ITextDecorator where T : PrintableText
    {
        T text;
        public UpperCaseText(T text)
        {
            this.text = text;
        }

        public string GetText()
        {
            return text.GetText().ToUpper();
        }
    }

    class PrintableText : Text
    {
        public PrintableText(string msg) : base(msg) { }
    }

    class TechnicalText : Text
    {
        public TechnicalText(string msg) : base(msg) { }
    }

    internal class Program
    {
        static T Max<T>(T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0) return a;
            return b;
        }


        static long ConverBytyToMByte(long bytes)
        {
            return bytes / (long)Math.Pow(2, 20);
        }

        /**
         *  Функция, котора принимает путь до папки(ввиде строки)
         *   Если такой путь существует, то нужно будет показать содердимое папки
         *   Если не сущенствует, то показать сообщение об этом (bool Directory.Exsist(string path))
         * 
         */
        static void ShowDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var item in Directory.GetFileSystemEntries(path))
                    Console.WriteLine(item);
            }
            else
                Console.WriteLine("Папки не существует");
        }

        static void PrintDirectories(DirectoryInfo directory)
        {
            Console.Clear();
            Console.WriteLine(directory.FullName);


            DirectoryInfo[] directories = directory.GetDirectories();
            for (int i = 0; i < directories.Length; ++i)
            {
                DirectoryInfo directoryInfo = directories[i];
                Console.WriteLine($"#{i}.\t{directoryInfo.Name}");
            }
            Console.WriteLine("-1.\t<-Назад");

            Console.Write("Выбирите номер папки: ");
            int change = int.Parse(Console.ReadLine());
            if (change == -1)
            {
                PrintDirectories(directory.Parent);
            }
            else
            {
                PrintDirectories(directories[change]);
            }
        }
        static void Main(string[] args)
        {
            ShowDirectory(@"C:\Users");

            // FileInfo - информация о файле
            // DirectoryInfo - информация о директории(о папке)
            // DriveInfo - Информация о дисках

            // Получить все диски на компьютере
            DriveInfo[] drivers = DriveInfo.GetDrives();

            for (int i = 0; i < drivers.Length; ++i)
            {
                DriveInfo driver = drivers[i];
                Console.WriteLine($"#{i}");
                Console.WriteLine("------------------");
                Console.WriteLine($"Название:\t{driver.Name}");
                Console.WriteLine($"Формат:\t\t{driver.DriveFormat}");
                Console.WriteLine($"Тип:\t\t{driver.DriveType}");
                Console.WriteLine($"Объем:\t\t{ConverBytyToMByte(driver.TotalSize)}Мб");
                Console.WriteLine($"Свободно:\t{ConverBytyToMByte(driver.TotalFreeSpace)}Мб");
            }

            Console.Write("Выбирите номер диска: ");
            int change = int.Parse(Console.ReadLine());
            DriveInfo changeDrive = drivers[change];

            PrintDirectories(new DirectoryInfo(changeDrive.Name));
            Console.ReadKey();
        }
    }
}
#endif //MAIN