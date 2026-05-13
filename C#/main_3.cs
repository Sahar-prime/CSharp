//#define Main

#if Main
namespace _27._04
{
    /*
     * 1. Написать функцию, которая находит количества элементов в List, значения которых меньше чем среднее значение
     *  *Работа с любыми цифрами
     * 2. Создать класс, Student у которого есть поле ID - тип поля ID должен определять через шаблон
     *  * и так же добавить ещё несколько полей от себя на выбор
     *  
     *  */
    class Task1
    {
        public static int CountSmallerThanAverage<T>(List<T> numbers)
        {
            if (numbers == null || numbers.Count == 0) return 0;
            List<double> doubleNumbers = numbers.Select(n => Convert.ToDouble(n)).ToList();

            double average = doubleNumbers.Average();

            int count = doubleNumbers.Count(x => x < average);

            Console.WriteLine("Среднее значение: " + average);
            return count;
        }
    }
    class Student<T>
    {
        public T ID { get; set; }
        public string Name { get; set; }
        public string Faculty { get; set; }
        public double AverageGrade { get; set; }

        public Student(T id, string name, string faculty, double grade)
        {
            ID = id;
            Name = name;
            Faculty = faculty;
            AverageGrade = grade;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("ID: " + ID + " (" + ID.GetType().Name + "), Имя: " + Name + ", Балл: " + AverageGrade);
        }
    }

    interface IObserver
    {
        void getMessage(string msg);
    }
    class Subscruber : IObserver
    {
        public virtual void getMessage(string msg)
        {
            Console.WriteLine($"У вас новое сообщение: '{msg}'");
        }
    }

    class PremiumSubscruber : Subscruber
    {
        public override void getMessage(string msg)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            base.getMessage(msg);
            Console.ForegroundColor = temp;
        }
    }

    class Player : IObserver
    {
        public void getMessage(string msg)
        {
            Console.WriteLine($"У вас новое уведомление: '{msg}'");
        }
    }

    interface IObservable<T> where T : IObserver
    {
        void addObserver(T observer);
        void removeObserver(T observer);
    }

    class Blogger : IObservable<Subscruber>
    {
        List<Subscruber> observers = new List<Subscruber>();
        public void addObserver(Subscruber observer)
        {
            observers.Add(observer);
        }

        public void removeObserver(Subscruber observer)
        {
            observers.Remove(observer);
        }

        public void sendMessage(string msg)
        {
            foreach (Subscruber observer in observers)
            {
                observer.getMessage(msg);
            }
        }
    }


    internal class Program
    {

        /**
        * Создать функцию, которая конвертирует одну коллекцию в другую
        * Параметрами функции выступает коллекция, которую хотим конвертировать
        * и у функции должен быть дженерик - тип коллекции, к которому хотим привести
        * 
        */
        static ResultType ConvertCollections<
            ResultType,
            T>
            (ICollection<T> collection) where ResultType : ICollection<T>, new()
        {
            ResultType result = new ResultType();

            foreach (T item in collection)
            {
                result.Add(item);
            }

            return result;
        }


        /**
         *  new()   - У данного типа есть конструктор, но только конструктор поумолчанию
         *  class   - Объект объязательно должен быть экземпляром класса
         *  notnull - Объект должен быть не нулевым
         *  struct  - Все остальное, кроме классов
         */
        static void tempGenerics<T>() where T : class, new()
        {
            T data = new T();
        }

        static void printList<T>(List<T> data)
        {
            // T data = new T(); Ошибка, т.к в настройках дженерика не указали, что у объекта есть
            //      конструктор поумолчанию
            foreach (T item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        static void reversePrintList<T>(IEnumerable<T> items)
        {
            foreach (T item in items.Reverse())
            {
                Console.WriteLine(item);
            }
        }

        class Temp
        {

        }
        //static T sumList<T>(List<T> data) where T : INumber<T>
        //{
        //    T sum = T.Zero;
        //    foreach (T item in data)
        //    {
        //        sum += item;
        //    }
        //    return sum;
        //}

        static void Main(string[] args)
        {
            PremiumSubscruber premiumSubscruber = new PremiumSubscruber();
            Subscruber subscruber = new Subscruber();
            Player player = new Player();

            List<int> d = new List<int>();

            ConvertCollections<SortedSet<int>, int>(d);

            Blogger logger = new Blogger();
            // logger.addObserver(player); ошибка, наш класс Logger ожидает подписчиков только в виде Subscruber
            logger.addObserver(subscruber);
            logger.addObserver(premiumSubscruber);

            logger.sendMessage("Hello World!");

            printList(new List<int> { 1, 2, 333 });

            printList(new List<bool> { true, false });

            // sumLits(new List<Temp>()); ERROR, т.к тип  T не является реализацией интерфейса INumber
            //Console.WriteLine($"sum = {sumList(new List<int> { 1, 2, 3 })}");
            //Console.WriteLine($"sum = {sumList(new List<double> { 1.1, 2.2, 3.3 })}");


            //ПР
            List<int> intList = new List<int> { 2, 4, 6, 8, 10 };
            int result1 = Task1.CountSmallerThanAverage(intList);
            Console.WriteLine("Меньше среднего (для int): " + result1);

            // Пример со студентами с разными типами ID
            Student<int> student1 = new Student<int>(101, "Иван", "ИТ", 4.5);
            Student<string> student2 = new Student<string>("A-202", "Мария", "Экономика", 4.8);

            student1.DisplayInfo();
            student2.DisplayInfo();
        }
    } 
}
#endif //Main