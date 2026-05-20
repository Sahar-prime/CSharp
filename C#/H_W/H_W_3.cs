//#define H_W_3

#if H_W_3
class TaskManager
{
    private static List<string> tasks = new List<string>();
    private const string FileName = "tasks.txt";

    static void Main()
    {
        LoadTasks();

        while (true)
        {
            Console.WriteLine("\n--- Менеджер задач ---");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Посмотреть задачи");
            Console.WriteLine("3. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private static void LoadTasks()
    {
        if (File.Exists(FileName))
        {
            tasks = new List<string>(File.ReadAllLines(FileName));
        }
    }

    private static void AddTask()
    {
        Console.Write("Введите новую задачу: ");
        string newTask = Console.ReadLine();
        tasks.Add(newTask);
        File.WriteAllLines(FileName, tasks);
        Console.WriteLine("Задача добавлена.");
    }

    private static void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Список задач пуст.");
            return;
        }

        Console.WriteLine("\n--- Список задач ---");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }
}
#endif //H_W_3