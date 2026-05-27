//#define H_W_4
using Loger;

#if H_W_4
class Program
{
    static void Main()
    {
        // Путь к INI-файлу и лог-файлу
        string configPath = "config.ini";
        string logPath = "app.log";

        // Создаём логгер
        Logger logger = new Logger(configPath, logPath);

        // Записываем логи
        logger.Log(Logger.LogType.Info, "test testy", "Программа запущена.");
        logger.Log(Logger.LogType.Error, "test testy", "Ошибка при загрузке данных.");
        logger.Log(Logger.LogType.Warning, "test testy", "Низкий уровень памяти.");

        Console.WriteLine("Логи записаны в файл app.log");
    }
}
#endif //H_W_4