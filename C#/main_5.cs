//#define MAIN

#if MAIN
using System.Xml.Serialization;

namespace C_
{
    public enum GameResult
    {
        InProgress,
        Win,
        Lose,
        Draw
    }

    public class Statistics
    {
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Unfinished { get; set; } = 0;
    }

    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point() { }

        public override bool Equals(object? obj)
        {
            if (obj is Point other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }

        // ОБЯЗАТЕЛЬНО: Хэш-код должен быть одинаковым для точек с равными x и y
        public override int GetHashCode()
        {
            // Метод HashCode.Combine доступен в .NET Core / .NET 5+
            return HashCode.Combine(x, y);

            // Если вы работаете в старом .NET Framework, используйте эту строку вместо верхней:
            // return (x.GetHashCode() * 397) ^ y.GetHashCode();
        }
    }

    public class Company
    {
        // Уровень 1
        public string Name;
        public int EstablishedYear;
        public string[] CoreTechnologies; // Массив простых типов (Уровень 1)
        public Department[] Departments;
    }

    public class Department
    {
        // Уровень 2
        public string DepartmentName;
        public string HeadManager;
        public int[] ProjectBudgets;     // Массив простых типов (Уровень 2)
        public Team[] Teams;             // Массив объектов (Уровень 2)
    }

    public class Team
    {
        // Уровень 3
        public string TeamName;
        public bool IsRemote;
        public string[] ActiveSprints;   // Массив простых типов (Уровень 3)
        public Employee[] Employees;     // Массив объектов (Уровень 3)
    }

    public class Employee
    {
        // Уровень 4
        public string FullName;
        public string Role;
        public double Salary;
        public string[] Skills;
    }

    public class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public List<int> MathGrades { get; set; } = new List<int>();
        public List<int> InfGrades { get; set; } = new List<int>();
    }

    public class GameState
    {
        public HashSet<Point> selfPosition = new HashSet<Point>();
        public HashSet<Point> enemyPosition = new HashSet<Point>();
    }

    internal class main_5
    {
        private static readonly string STATS_FILE = "statistics.xml";

        static bool CheckWin(HashSet<Point> positions)
        {
            Point[][] winCombinations = new Point[][]
            {
                new Point[] { new Point(0,0), new Point(1,0), new Point(2,0) },
                new Point[] { new Point(0,1), new Point(1,1), new Point(2,1) },
                new Point[] { new Point(0,2), new Point(1,2), new Point(2,2) },
                new Point[] { new Point(0,0), new Point(0,1), new Point(0,2) },
                new Point[] { new Point(1,0), new Point(1,1), new Point(1,2) },
                new Point[] { new Point(2,0), new Point(2,1), new Point(2,2) },
                new Point[] { new Point(0,0), new Point(1,1), new Point(2,2) },
                new Point[] { new Point(2,0), new Point(1,1), new Point(0,2) }
            };

            foreach (var combination in winCombinations)
            {
                if (combination.All(p => positions.Contains(p)))
                {
                    return true;
                }
            }
            return false;
        }
        static bool CheckDraw(GameState gameState)
        {
            return gameState.selfPosition.Count + gameState.enemyPosition.Count == 9;
        }
        static GameResult CheckGameState(GameState gameState)
        {
            if (CheckWin(gameState.selfPosition))
                return GameResult.Win;
            if (CheckWin(gameState.enemyPosition))
                return GameResult.Lose;
            if (CheckDraw(gameState))
                return GameResult.Draw;
            return GameResult.InProgress;
        }
        static Statistics LoadStatistics()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Statistics));
            if (File.Exists(STATS_FILE))
            {
                using (FileStream fs = new FileStream(STATS_FILE, FileMode.Open, FileAccess.Read))
                {
                    return (Statistics)serializer.Deserialize(fs);
                }
            }
            return new Statistics();
        }
        static void SaveStatistics(Statistics stats)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Statistics));
            using (FileStream fs = new FileStream(STATS_FILE, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, stats);
            }
        }
        static void UpdateStatistics(GameResult result)
        {
            Statistics stats = LoadStatistics();
            switch (result)
            {
                case GameResult.Win:
                    stats.Wins++;
                    break;
                case GameResult.Lose:
                    stats.Losses++;
                    break;
                case GameResult.Draw:
                    stats.Draws++;
                    break;
            }
            SaveStatistics(stats);
        }
        static void ShowStatistics()
        {
            Statistics stats = LoadStatistics();
            Console.Clear();
            Console.WriteLine("=== СТАТИСТИКА ===");
            Console.WriteLine($"Победы: {stats.Wins}");
            Console.WriteLine($"Поражения: {stats.Losses}");
            Console.WriteLine($"Ничьи: {stats.Draws}");
            Console.WriteLine($"Незавершенные партии: {stats.Unfinished}");
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        static GameResult StartOrContinueGame(GameState gameState)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== КРЕСТИКИ-НОЛИКИ ===");
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                // Отрисовка поля
                for (int y = 0; y < 3; y++)
                {
                    Console.Write(" ");
                    for (int x = 0; x < 3; x++)
                    {
                        Point currentPoint = new Point(x, y);
                        if (gameState.selfPosition.Contains(currentPoint))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("X");
                        }
                        else if (gameState.enemyPosition.Contains(currentPoint))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("O");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                        Console.ResetColor();

                        if (x < 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(" │ ");
                            Console.ResetColor();
                        }
                    }
                    Console.WriteLine();

                    if (y < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("───┼───┼───");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();

                // Проверка состояния игры
                GameResult result = CheckGameState(gameState);
                if (result != GameResult.InProgress)
                {
                    switch (result)
                    {
                        case GameResult.Win:
                            Console.WriteLine("Вы выиграли!");
                            break;
                        case GameResult.Lose:
                            Console.WriteLine("Вы проиграли!");
                            break;
                        case GameResult.Draw:
                            Console.WriteLine("Ничья!");
                            break;
                    }
                    UpdateStatistics(result);
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                    Console.ReadKey();
                    return result;
                }

                // Выход из игры
                Console.Write("Хотите ли вы выйти из игры?(Y/N): ");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "y")
                {
                    Console.Write("Хотите ли вы сохранить игру?(Y/N): ");
                    input = Console.ReadLine()?.Trim().ToLower();
                    if (input == "y")
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameState));
                        if (!Directory.Exists("saves"))
                        {
                            Directory.CreateDirectory("saves");
                        }
                        using (FileStream fs = new FileStream($"saves/{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.xml", FileMode.CreateNew, FileAccess.Write))
                        {
                            xmlSerializer.Serialize(fs, gameState);
                        }
                        Statistics stats = LoadStatistics();
                        stats.Unfinished++;
                        SaveStatistics(stats);
                    }
                    return GameResult.InProgress;
                }

                // Ход игрока
                Console.WriteLine("Введите координаты своей фигуры:");
                int nx, ny;

                while (true)
                {
                    Console.Write("X (0-2): ");
                    if (int.TryParse(Console.ReadLine(), out nx) && nx >= 0 && nx <= 2)
                        break;
                    Console.WriteLine("Некорректное значение. Введите число от 0 до 2.");
                }

                while (true)
                {
                    Console.Write("Y (0-2): ");
                    if (int.TryParse(Console.ReadLine(), out ny) && ny >= 0 && ny <= 2)
                    {
                        Point newPoint = new Point(nx, ny);
                        if (!gameState.selfPosition.Contains(newPoint) && !gameState.enemyPosition.Contains(newPoint))
                            break;
                        Console.WriteLine("Эта клетка уже занята. Попробуйте снова.");
                    }
                    else
                    {
                        Console.WriteLine("Некорректное значение. Введите число от 0 до 2.");
                    }
                }

                gameState.selfPosition.Add(new Point(nx, ny));

                // Проверка после хода игрока
                result = CheckGameState(gameState);
                if (result != GameResult.InProgress)
                {
                    switch (result)
                    {
                        case GameResult.Win:
                            Console.WriteLine("Вы выиграли!");
                            break;
                        case GameResult.Draw:
                            Console.WriteLine("Ничья!");
                            break;
                    }
                    UpdateStatistics(result);
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню...");
                    Console.ReadKey();
                    return result;
                }

                // Ход врага
                Random random = new Random();
                do
                {
                    nx = random.Next(0, 3);
                    ny = random.Next(0, 3);
                } while (gameState.selfPosition.Contains(new Point(nx, ny)) || gameState.enemyPosition.Contains(new Point(nx, ny)));

                gameState.enemyPosition.Add(new Point(nx, ny));
            }
        }

        static void StartOrContinueGame()
        {
            StartOrContinueGame(new GameState());
        }

        static GameState? GetGameSave()
        {
            DirectoryInfo saveDirectory = new DirectoryInfo("saves");
            if (saveDirectory.Exists)
            {
                FileInfo[] savesFiles = saveDirectory.GetFiles();
                if (savesFiles.Length != 0)
                {
                    Console.WriteLine("Доступные сохранения:");
                    for (int i = 0; i < savesFiles.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. \t{savesFiles[i].Name}");
                    }

                    int choice;
                    while (true)
                    {
                        Console.Write($"Выберите сохранение (1-{savesFiles.Length}): ");
                        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= savesFiles.Length)
                            break;
                        Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    }

                    FileInfo selectedSave = savesFiles[choice - 1];
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameState));
                    using (FileStream fs = selectedSave.OpenRead())
                    {
                        return (GameState)xmlSerializer.Deserialize(fs);
                    }
                }
            }
            Console.WriteLine("Сохранения не найдены.");
            Console.ReadKey();
            return null;
        }

        static void Game()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. \tНачать новую игру");
                Console.WriteLine("2. \tПродолжить сохранённую игру");
                Console.WriteLine("3. \tПросмотреть статистику");
                Console.WriteLine("4. \tВыход");

                Console.Write("\nВаш выбор: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            StartOrContinueGame();
                            break;
                        case 2:
                            GameState? state = GetGameSave();
                            if (state != null)
                            {
                                StartOrContinueGame(state);
                            }
                            break;
                        case 3:
                            ShowStatistics();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    Console.ReadKey();
                }
            }
        }

        static void Main()
        {
            {
                Point point = new Point(-99, 100);
                //Сериализация - приведение к строке
                //Десериализация - из строки к объекту

                XmlSerializer xmlserializer = new XmlSerializer(typeof(Point));
                //Serialize - запись объекта
                /*
                 *Stream - поток куда записать XML
                 *Object - объект для записи
                 */
                using (FileStream fs = new FileStream("point.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        xmlserializer.Serialize(sw, point);
                    }
                }
            }

            {
                Company company = GetCompany();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));

                using (FileStream fileStream = new FileStream("company.xml", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        xmlSerializer.Serialize(streamWriter, company);
                    }
                }
            }

            {
                Random rand = new Random();
                using (FileStream fileStream = new FileStream("data_1.txt", FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        // Цикл для генерации и записи 5 чисел
                        for (int i = 0; i < 5; i++)
                        {
                            // rand.Next(100) генерирует числа от 0 до 99
                            int zeroToNintyNine = rand.Next(100);
                            streamWriter.WriteLine(zeroToNintyNine);
                        }
                    }
                }
            }

            {
                Student student = GetStudent();
                XmlSerializer xml = new XmlSerializer(typeof(Student));

                using (FileStream fs = new FileStream("student.xml", FileMode.Create, FileAccess.Write))
                {
                    xml.Serialize(fs, student);
                }
            }

            {
                XmlSerializer x = new XmlSerializer(typeof(Point));

                using (FileStream f = new FileStream("point.xml", FileMode.OpenOrCreate, FileAccess.Read)) 
                {
                    using (StreamReader sr = new StreamReader(f))
                    {
                        if (x.Deserialize(sr) is Point point) 
                        {
                            Console.WriteLine($"Точки из point.xml = ({point.x}, {point.y})");
                        }
                    }
                }
                Console.ReadKey();
            }

            CrudPoints();
            Game();
        }
        static void CrudPoints()
        {
            const string FILE_PATH = "points.xml";

            XmlSerializer pointsSerializer = new XmlSerializer(typeof(Point[]));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("работа с точками");

                Console.WriteLine("1. Просмотр");
                Console.WriteLine("2. Добавление");
                Console.WriteLine("3. Выйти");

                int change = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (change)
                {
                    case 1:
                        {
                            using (FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate, FileAccess.Read))
                            {
                                using (StreamReader rs = new StreamReader(fs))
                                {
                                    if (rs.EndOfStream)
                                    {
                                        Console.WriteLine("Пустой список");
                                    }
                                    else if (pointsSerializer.Deserialize(rs) is Point[] points)
                                    {
                                        foreach (Point point in points)
                                        {
                                            Console.WriteLine($"({point.x}, {point.y})");
                                        }
                                    }
                                }
                            }
                            break;
                        }

                    case 2:
                        {
                            int x, y;

                            Console.Write("Введите координату X: ");
                            x = int.Parse(Console.ReadLine());

                            Console.Write("Введите координату Y: ");
                            y = int.Parse(Console.ReadLine());

                            Point point = new Point(x, y);

                            Point[] points = [];
                            using (FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate, FileAccess.Read))
                            {
                                using (StreamReader rs = new StreamReader(fs))
                                {
                                    if (!rs.EndOfStream && pointsSerializer.Deserialize(rs) is Point[] temp)
                                    {
                                        points = temp as Point[];

                                    }
                                    points = points.Append(point).ToArray();
                                }
                            }

                            using (FileStream fs = new FileStream(FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                using (StreamWriter ws = new StreamWriter(fs))
                                {
                                    pointsSerializer.Serialize(ws, points);
                                }
                            }
                            break;
                        }

                    case 3:
                        return;

                    default:
                        Console.WriteLine("Некорректный ввод.");
                        break;
                }
                Console.ReadKey();
            }
        }
        static Student GetStudent()
        {
            return new Student
            {
                Name = "Сергей",
                LastName = "Васильевич",
                Number = "D321",
                MathGrades = new List<int> { 4, 5, 3, 4, 4 },
                InfGrades = new List<int> { 5, 5, 5, 5, 4 }
            };
        }
        static Company GetCompany()
        {
            return new Company
            {
                // Уровень 1
                Name = "CyberNova Games",
                EstablishedYear = 2021,
                CoreTechnologies = new string[] { "C#", "Unity", "Unreal Engine", "Blender" },
                Departments = new Department[]
                {
                    new Department
                    {
                        // Уровень 2
                        DepartmentName = "Game Development",
                        HeadManager = "Alex Mercer",
                        ProjectBudgets = new int[] { 450000, 1200000, 85000 },
                        Teams = new Team[]
                        {
                            new Team
                            {
                                // Уровень 3
                                TeamName = "Team Alpha (Core Gameplay)",
                                IsRemote = true,
                                ActiveSprints = new string[] { "SP-102_Physics", "SP-103_UI_Refactor" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        // Уровень 4
                                        FullName = "Dmitry Petrov",
                                        Role = "Lead Gameplay Programmer",
                                        Salary = 115000.00,
                                        Skills = new string[] { "C#", "Vector Math", "Optimization", "Git" }
                                    },
                                    new Employee
                                    {
                                        FullName = "Elena Rostova",
                                        Role = "Senior 3D Animator",
                                        Salary = 95000.00,
                                        Skills = new string[] { "Maya", "Rigging", "Motion Capture" }
                                    }
                                }
                            },
                            new Team
                            {
                                TeamName = "Team Beta (Multiplayer)",
                                IsRemote = false,
                                ActiveSprints = new string[] { "SP-089_Matchmaking" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        FullName = "Marcus Vance",
                                        Role = "Network Engineer",
                                        Salary = 130000.50,
                                        Skills = new string[] { "C++", "Photon", "gRPC", "Docker" }
                                    }
                                }
                            }
                        }
                    },
                    new Department
                    {
                        DepartmentName = "Marketing & PR",
                        HeadManager = "Sarah Connor",
                        ProjectBudgets = new int[] { 30000, 15000 },
                        Teams = new Team[]
                        {
                            new Team
                            {
                                TeamName = "SMM & Community",
                                IsRemote = true,
                                ActiveSprints = new string[] { "SP-201_Trailer_Release" },
                                Employees = new Employee[]
                                {
                                    new Employee
                                    {
                                        FullName = "Anna Lee",
                                        Role = "Community Manager",
                                        Salary = 60000.00,
                                        Skills = new string[] { "Copywriting", "Discord API", "Public Relations" }
                                    }
                                }
                            }
                        }
                    }
                }
            };

        }
    }
}
#endif //MAIN