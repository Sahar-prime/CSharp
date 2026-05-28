//#define MAIN

#if MAIN
using System.Text;

namespace _28._05
{
    class Game
    {
        int width = 6;
        int height = 6;

        int x = 0, y = 0;
        int lastX = -1, lastY = -1;

        int targetX = -1, targetY = -1;
        int lastTargetX = -1, lastTargetY = -1;

        int score = 0;
        int lastScore = -1;

        bool isRunning = true;
        Random random = new Random();

        public void SpawnTarget()
        {
            while (isRunning)
            {
                targetX = random.Next(0, width);
                targetY = random.Next(0, height);

                Thread.Sleep(3000);
            }
        }

        public void LisenseKeyboar()
        {
            while (isRunning)
            {
                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(10);
                    continue;
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (x > 0) x--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (x < width - 1) x++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (y > 0) y--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (y < height - 1) y++;
                        break;

                    case ConsoleKey.Escape:
                        Exit();
                        break;
                }
            }
        }

        public void Draw()
        {
            Console.CursorVisible = false;
            StringBuilder frame = new StringBuilder();

            string horizontalBorder = "+";
            for (int i = 0; i < width; i++)
            {
                horizontalBorder += "---+";
            }

            while (isRunning)
            {
                // Проверяем, поймал ли игрок цель
                if (x == targetX && y == targetY)
                {
                    score++;

                    // Мгновенно переносим цель в новое место
                    targetX = random.Next(0, width);
                    targetY = random.Next(0, height);
                }

                // Обновляем экран, если изменились координаты игрока, цели или количество очков
                if (x != lastX || y != lastY || targetX != lastTargetX || targetY != lastTargetY || score != lastScore)
                {
                    lastX = x;
                    lastY = y;
                    lastTargetX = targetX;
                    lastTargetY = targetY;
                    lastScore = score;

                    frame.Clear();

                    for (int row = 0; row < height; row++)
                    {
                        frame.AppendLine(horizontalBorder);

                        frame.Append("|");
                        for (int col = 0; col < width; col++)
                        {
                            if (col == x && row == y)
                            {
                                frame.Append(" x |");
                            }
                            else if (col == targetX && row == targetY)
                            {
                                frame.Append(" o |");
                            }
                            else
                            {
                                frame.Append("   |");
                            }
                        }
                        frame.AppendLine();
                    }
                    frame.AppendLine(horizontalBorder);

                    // Выводим очки под игровым полем
                    frame.AppendLine($"Очки: {score}");

                    Console.SetCursorPosition(0, 0);
                    Console.Write(frame.ToString());
                }

                Thread.Sleep(15);
            }
        }

        List<Thread> threads = new List<Thread>();
        public void Start()
        {
            threads.Add(new Thread(LisenseKeyboar));
            threads.Add(new Thread(Draw));
            threads.Add(new Thread(SpawnTarget));

            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        public void Exit()
        {
            isRunning = false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
#endif //MAIN