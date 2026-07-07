//#define MAIN

#if MAIN
namespace C_
{
    internal class Asynchrony
    {
        static async Task<int> findMax(List<int> list, int start, int length) =>
            await Task.Run(
                () =>
                {
                    int max = list[start];
                    for (int i = 0; i < length; ++i)
                    {
                        if (list[start + i] > max)
                        {
                            max = list[start + i];
                        }
                    }
                    return max;
                }
            );

        static void Print(string msg)
        {
            Thread.Sleep(1000);
            Console.WriteLine(msg);
        }

        static async Task AsyncPrint(string msg) 
        {
            await Task.Delay(1000); // Имитация сложного/долгого действия
            Console.WriteLine(msg);
        }

        static async Task Main()
        {
            {
                List<int> ints = new List<int>(300_000_000);
                for (int i = 0; i < ints.Capacity; ++i)
                {
                    ints.Add(new Random().Next());
                }

                DateTime start = DateTime.Now;
                int result = await findMax(ints, 0, ints.Count);
                DateTime end = DateTime.Now;

                Console.WriteLine($"" +
                    $"Найден максимальный элемент = {result}\n" +
                    $"Время поиска = {end.Ticks - start.Ticks}\n");

                start = DateTime.Now;
                Task<int>[] tasks = new Task<int>[300];
                for (int i = 0; i < tasks.Length; ++i)
                {
                    tasks[i] = findMax(ints, i * 1000_000, 1000_000);
                }

                Task<int>.WaitAll(tasks);

                result = await findMax(tasks.
                    Select(task => task.Result).
                    ToList(), 0, tasks.Length);
                end = DateTime.Now;

                Console.WriteLine($"" +
                    $"Найден максимальный элемент = {result}\n" +
                    $"Время поиска = {end.Ticks - start.Ticks}\n");

            }

            {
                for (int i = 0; i < 10; i++)
                    AsyncPrint("Async_Print");

                Thread.Sleep(2000);

                for (int i = 0; i < 10; i++)
                    Print("Print");
            }
        }
    }
}
#endif //MAIN