//#define H_W_6

#if H_W_6
namespace C_
{
    internal class H_W_6
    {
        public struct Book
        {
            public string Title { get; set; }
            public string Author { get; set; }

            public Book(string title, string author)
            {
                Title = title;
                Author = author;
            }
        }
        public class Student
        {
            public string Name { get; set; }
            public Book FavoriteBook { get; set; }

            private static int count = 0;
            public static int Count => count;

            public Student(string name, Book favoriteBook)
            {
                Name = name;
                FavoriteBook = favoriteBook;
                count++; // Увеличиваем счетчик при создании нового студента
            }
        }

        class Program
        {
            static void Main()
            {
                Console.WriteLine("--- Система учета студентов ---");
                Console.WriteLine($"Начальное количество студентов в системе: {Student.Count}\n");

                // Создаем студентов
                Student student1 = new Student("Иван", new Book("Хоббит", "Дж. Р. Р. Толкин"));
                Console.WriteLine($"Создан студент {student1.Name}.");
                Console.WriteLine($"Текущее количество студентов в системе: {Student.Count}\n");

                Student student2 = new Student("Анна", new Book("Война и мир", "Л. Н. Толстой"));
                Console.WriteLine($"Создан студент {student2.Name}.");
                Console.WriteLine($"Текущее количество студентов в системе: {Student.Count}\n");

                // Эксперимент с копированием
                Console.WriteLine("--- Эксперимент с копированием ---");
                Console.WriteLine($"Оригинальный студент: {student1.Name}, его любимая книга: \"{student1.FavoriteBook.Title}\" автора {student1.FavoriteBook.Author}.\n");

                // Копируем студента (ссылочный тип)
                Student student1Copy = student1;
                student1Copy.Name = "Петр";

                // Копируем книгу (тип-значение)
                Book bookCopy = student1.FavoriteBook;
                bookCopy.Title = "Властелин Колец";

                Console.WriteLine("...Копируем данные и вносим изменения...");
                Console.WriteLine($"Изменяем имя у копии студента на 'Петр'.");
                Console.WriteLine($"Изменяем название у копии книги на 'Властелин Колец'.\n");

                Console.WriteLine("Результат после изменений:");
                Console.WriteLine($"Имя оригинального студента (student1): {student1.Name}");
                Console.WriteLine($"Название любимой книги оригинального студента (student1.FavoriteBook): {student1.FavoriteBook.Title}");

                Console.WriteLine("\nВывод: Имя студента изменилось, так как классы копируются по ссылке.");
                Console.WriteLine("Вывод: Книга не изменилась, так как структуры копируются по значению.");
            }
        }
    }
}
#endif //H_W_6