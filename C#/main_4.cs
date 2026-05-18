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
         * Получить случайная папку(директорию на компьютере)
         *      1. Случайно выбрать диск
         *      2. Перемешать вглубить файловой системы по приницу рандома(
         *          50% то что вы из родительской папки, перейдете в дочернюю,
         *          50% то что вы из родительской папки выберите случайную
         *         )
         */
        static DirectoryInfo GetRandomDirectory()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            Random rand = new Random();
            DriveInfo randomDrive = drives[rand.Next(drives.Length)];

            DirectoryInfo currentDir = new DirectoryInfo(randomDrive.Name);

            //Перемещаемся вглубь файловой системы
            while (true)
            {
                try
                {
                    // Получаем список подпапок текущей директории
                    DirectoryInfo[] subDirectories = currentDir.GetDirectories();

                    // Если дочерних папок нет, глубже идти физически некуда
                    if (subDirectories.Length == 0) break;

                    // Принцип рандома 50% / 50%
                    if (rand.Next(0, 2) == 0)
                    {
                        // 50% — остаемся в текущей родительской папке (завершаем выбор)
                        break;
                    }
                    else
                    {
                        // 50% — переходим из родительской папки в случайную дочернюю
                        currentDir = subDirectories[rand.Next(subDirectories.Length)];
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Защита от системных папок, к которым у программы нет прав доступа
                    break;
                }
            }

            return currentDir;
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
            Console.WriteLine(directory.FullName);

            DirectoryInfo[] directories = directory.GetDirectories();
            for (int i = 0; i < directories.Length; ++i)
            {
                DirectoryInfo directoryInfo = directories[i];
                Console.WriteLine($"#{i}.\t{directoryInfo.Name}");
            }
        }

        static DirectoryInfo ChangeDirectory(DirectoryInfo directory)
        {
            Console.Clear();
            PrintDirectories(directory);

            DirectoryInfo[] directories = directory.GetDirectories();

            Console.WriteLine("-1.\t<-Назад");
            Console.WriteLine("-2.\t<-Выбор");

            Console.Write("Выбирите номер папки: ");
            int change = int.Parse(Console.ReadLine());

            switch (change)
            {
                case -1:
                    return ChangeDirectory(directory.Parent);
                case -2:
                    return directory;
                default:
                    if (0 <= change && change < directories.Length)
                        return ChangeDirectory(directories[change]);
                    break;
            }

            return directory;
        }

        static DirectoryInfo ChangeDirectory()
        {
            // Получить все диски на компьютере
            DriveInfo[] drivers = DriveInfo.GetDrives();
            PrintDrivers();

            Console.Write("Выбирите номер диска: ");
            int change = int.Parse(Console.ReadLine());
            DriveInfo changeDrive = drivers[change];

            return ChangeDirectory(new DirectoryInfo(changeDrive.Name));
        }

        static void DeleteInDirectory(DirectoryInfo directory)
        {
            Console.Clear();

            PrintDirectories(directory);

            Console.WriteLine("-1.\t<-Назад");

            DirectoryInfo[] directories = directory.GetDirectories();

            Console.Write("Выбирите номер папки: ");
            int change = int.Parse(Console.ReadLine());

            if (change == -1)
            {
                WorkDirectories(directory);
            }
            else if (0 <= change && change < directories.Length)
            {
                DirectoryInfo deleteDirectory = directories[change];
                if (deleteDirectory.GetDirectories().Length == 0)
                    deleteDirectory.Delete();
                else
                {
                    Console.WriteLine($"Нельзя удалить папку {deleteDirectory.FullName} так как она не пустая");
                }

                Console.ReadKey();
                WorkDirectories(directory);
            }
        }

        static void AddInDirectory(DirectoryInfo directory)
        {
            Console.Clear();

            Console.WriteLine("Введите путь до новый папки: ");
            Console.Write($"{directory.FullName}\\");

            string name = Console.ReadLine();

            string path = $"{directory.FullName}\\{name}";
            Console.Write($"Дейсвительно ли выхотите сохранить папку '{path}'(Y/N): ");

            string change = Console.ReadLine();
            if (change.ToLower() == "y")
            {
                Directory.CreateDirectory(path);
            }

            WorkDirectories(directory);
        }

        static void MoveFromDirectory(DirectoryInfo directory)
        {
            Console.Clear();

            PrintDirectories(directory);

            Console.WriteLine("-1.\t<-Назад");

            DirectoryInfo[] directories = directory.GetDirectories();

            Console.Write("Выбирите номер папки: ");
            int change = int.Parse(Console.ReadLine());

            if (change == -1)
            {
                WorkDirectories(directory);
            }
            else if (0 <= change && change < directories.Length)
            {
                DirectoryInfo toDirectory = ChangeDirectory();
                DirectoryInfo changeDirectory = directories[change];
                changeDirectory.MoveTo($"{toDirectory.FullName}\\{changeDirectory.Name}");
                WorkDirectories(toDirectory);
            }
        }

        static void WorkDirectories(DirectoryInfo directory)
        {
            Console.Clear();

            PrintDirectories(directory);

            DirectoryInfo[] directories = directory.GetDirectories();

            Console.WriteLine("-1.\t<-Назад");
            Console.WriteLine("-2.\t<-Удалить");
            Console.WriteLine("-3.\t<-Добавить");
            Console.WriteLine("-4.\t<-Переместить");

            Console.Write("Выбирите номер папки: ");
            int change = int.Parse(Console.ReadLine());

            switch (change)
            {
                case -1:
                    WorkDirectories(directory.Parent);
                    break;
                case -2:
                    DeleteInDirectory(directory);
                    break;
                case -3:
                    AddInDirectory(directory);
                    break;
                case -4:
                    MoveFromDirectory(directory);
                    break;
                default:
                    if (0 <= change && change < directories.Length)
                        WorkDirectories(directories[change]);
                    break;
            }
        }

        static void PrintDrivers()
        {
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
        }

        static void Main(string[] args)
        {
            {
                /*
                * FileMode - режим работы с файлом
                *          Open - открыть (если файла нет, то будет ошибка)
                *          OpenOrCreate - открыть или создать (если файла нет), при открытии файл будет очищен
                *          Create - создать и сразу открыть файл(Если файл есть, то будет ошибка)
                *          Append - Открыть файл на добавление
                *          CreateNew - Создать новый файл
                *          Truncate - Открыть существующий файл и очистить его
                */

                //Я хочу рабоать на чтение, т.е. не все варианты мне подходят, Open
                using (FileStream fileStream = new FileStream("data.txt", FileMode.Truncate, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        Console.Write($"введите содержимое для файла({fileStream.Name}): ");
                        streamWriter.WriteLine(Console.ReadLine());
                    }
                }

                using (FileStream fileStream = new FileStream("data.txt", FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        Console.WriteLine($"Содержимое файла({fileStream.Name}\n___________\n");
                        while (!streamReader.EndOfStream)
                        {
                            string content = streamReader.ReadLine();
                            Console.Write(content);
                        }
                    }
                }
                Console.WriteLine();
            }

            {
                DirectoryInfo randomFolder = GetRandomDirectory();
                Console.WriteLine($"Случайная папка: {randomFolder.FullName}");
                Console.ReadKey();
            }

            {
                Console.WriteLine();
                ShowDirectory(@"C:\Users");
                Console.WriteLine();
            }

            {
                // FileInfo - информация о файле
                // DirectoryInfo - информация о директории(о папке)
                // DriveInfo - Информация о дисках

                // Получить все диски на компьютере
                DriveInfo[] drivers = DriveInfo.GetDrives();
                PrintDrivers();

                Console.Write("Выбирите номер диска: ");
                int change = int.Parse(Console.ReadLine());
                DriveInfo changeDrive = drivers[change];

                WorkDirectories(new DirectoryInfo(changeDrive.Name));
                Console.ReadKey();
            }
        }
    }
}
#endif //MAIN