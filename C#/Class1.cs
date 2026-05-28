//#define MAIN

#if MAIN
namespace _28._05
{
    class Game
    {
        int x = 0, y = 0;

        public void LisenseKeyboar()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (x == 1) x = 0;
                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            if (x == 0) x = 1;
                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (y == 1) y = 0;
                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (y == 0) y = 1;
                            break;
                        }
                }
            }
        }

        public void Draw()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("+-+-+");
                if (y == 0)
                {
                    Console.WriteLine(x == 0 ? "|x| |" : "| |x|");
                }
                else
                {
                    Console.WriteLine("| | |");
                }
                Console.WriteLine("+-+-+");
                if (y == 1)
                {
                    Console.WriteLine(x == 0 ? "|x| |" : "| |x|");
                }
                else
                {
                    Console.WriteLine("| | |");
                }
                Console.WriteLine("+-+-+");
            }
        }

        List<Thread> threads = new List<Thread>();
        public void Start()
        {
            threads.Add(new Thread(LisenseKeyboar));
            threads.Add(new Thread(Draw));

            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        public void Exit()
        {
            foreach (Thread t in threads)
            {
                t.Abort();
            }
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