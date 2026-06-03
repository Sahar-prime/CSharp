//#define H_W_5

#if H_W_5
namespace BookListApp
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }

        public Book(string title, string author)
        {
            Title = string.IsNullOrWhiteSpace(title) ? "Без названия" : title;
            Author = string.IsNullOrWhiteSpace(author) ? "Неизвестный автор" : author;
        }

        public override string ToString() => $"«{Title}» — {Author}";
    }
    public class ReadingList
    {
        private readonly List<Book> _books = new();

        public int Count => _books.Count;

        public static ReadingList operator +(ReadingList list, Book book)
        {
            if (list == null || book == null) return list;

            if (!list.Contains(book.Title))
            {
                list._books.Add(book);
                Console.WriteLine($"Добавлено: {book}");
            }
            else
                Console.WriteLine($"Книга «{book.Title}» уже есть в списке!");

            return list;
        }

        public static ReadingList operator -(ReadingList list, string bookTitle)
        {
            if (list == null || string.IsNullOrWhiteSpace(bookTitle)) return list;

            var bookToRemove = list._books.Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (bookToRemove != null)
            {
                list._books.Remove(bookToRemove);
                Console.WriteLine($"Удалено: {bookToRemove}");
            }
            else
                Console.WriteLine($"Книга с названием «{bookTitle}» не найдена.");

            return list;
        }

        public static bool operator true(ReadingList list) => list?._books.Count > 0;
        public static bool operator false(ReadingList list) => list?._books.Count == 0;

        public bool Contains(string bookTitle) =>
            _books.Exists(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

        public Book this[int index] => _books[index];

        public string this[string bookTitle] =>
            _books.Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))?.Author ?? "Книга не найдена";

        public void PrintList()
        {
            Console.WriteLine("\n--- ВАШ СПИСОК КНИГ ---");
            if (this)
                for (int i = 0; i < _books.Count; i++)
                    Console.WriteLine($"{i + 1}. {_books[i]}");
            else
                Console.WriteLine("Список пуст.");
            Console.WriteLine("-----------------------\n");
        }
    }

    public class Journal
    {
        private string _title;
        private int _employeeCount;

        // Свойства для доступа к полям
        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? "Без названия" : value;
        }

        public int EmployeeCount
        {
            get => _employeeCount;
            set => _employeeCount = value < 0 ? 0 : value; // Количество сотрудников не может быть отрицательным
        }

        public Journal(string title, int employeeCount)
        {
            Title = title;
            EmployeeCount = employeeCount;
        }

        public static Journal operator +(Journal journal, int amount)
        {
            if (journal == null) throw new ArgumentNullException(nameof(journal));
            return new Journal(journal.Title, journal.EmployeeCount + amount);
        }

        public static Journal operator -(Journal journal, int amount)
        {
            if (journal == null) throw new ArgumentNullException(nameof(journal));
            return new Journal(journal.Title, journal.EmployeeCount - amount);
        }

        public static bool operator ==(Journal left, Journal right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.EmployeeCount == right.EmployeeCount;
        }

        public static bool operator !=(Journal left, Journal right)
        {
            return !(left == right);
        }

        public static bool operator <(Journal left, Journal right)
        {
            if (left == null || right == null) throw new ArgumentNullException();
            return left.EmployeeCount < right.EmployeeCount;
        }

        public static bool operator >(Journal left, Journal right)
        {
            if (left == null || right == null) throw new ArgumentNullException();
            return left.EmployeeCount > right.EmployeeCount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Journal otherJournal)
            {
                return this.EmployeeCount == otherJournal.EmployeeCount;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return EmployeeCount.GetHashCode();
        }

        public override string ToString()
        {
            return $"Журнал: \"{Title}\", Сотрудников: {EmployeeCount}";
        }
    }

    public class Shop
    {
        // Закрытые поля
        private string _name;
        private double _area;

        // Свойства для доступа к полям
        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? "Без названия" : value;
        }

        public double Area
        {
            get => _area;
            set => _area = value < 0 ? 0 : value; // Площадь не может быть отрицательной
        }

        public Shop(string name, double area)
        {
            Name = name;
            Area = area;
        }

        public static Shop operator +(Shop shop, double value)
        {
            if (shop == null) throw new ArgumentNullException(nameof(shop));
            return new Shop(shop.Name, shop.Area + value);
        }

        public static Shop operator -(Shop shop, double value)
        {
            if (shop == null) throw new ArgumentNullException(nameof(shop));
            return new Shop(shop.Name, shop.Area - value);
        }

        public static bool operator ==(Shop left, Shop right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) return false;
            return left.Area == right.Area;
        }

        public static bool operator !=(Shop left, Shop right)
        {
            return !(left == right);
        }

        public static bool operator <(Shop left, Shop right)
        {
            if (left == null || right == null) throw new ArgumentNullException();
            return left.Area < right.Area;
        }
        public static bool operator >(Shop left, Shop right)
        {
            if (left == null || right == null) throw new ArgumentNullException();
            return left.Area > right.Area;
        }

        public override bool Equals(object obj)
        {
            if (obj is Shop otherShop)
            {
                return this.Area == otherShop.Area;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Area.GetHashCode();
        }

        public override string ToString()
        {
            return $"Магазин: \"{Name}\", Площадь: {Area} кв.м.";
        }
    }

    class Program
    {
        static void Main()
        {
            {
                var myReadingList = new ReadingList();

                myReadingList += new Book("1984", "Джордж Оруэлл");
                myReadingList += new Book("Преступление и наказание", "Фёдор Достоевский");
                myReadingList += new Book("Мастер и Маргарита", "Михаил Булгаков");
                myReadingList += new Book("1984", "Джордж Оруэлл");

                myReadingList.PrintList();

                string searchTitle = "1984";
                Console.WriteLine($"Есть ли «{searchTitle}» в списке? Ответ: {myReadingList.Contains(searchTitle)}");

                Console.WriteLine($"Первая книга в списке (индекс 0): {myReadingList[0]}");

                string bookToFind = "Мастер и Маргарита";
                Console.WriteLine($"Автор книги «{bookToFind}»: {myReadingList[bookToFind]}");

                myReadingList -= "Преступление и наказание";
                myReadingList.PrintList();
            }

            {
                Journal j1 = new Journal("Tech", 10);
                Journal j2 = new Journal("Science", 15);

                j1 = j1 + 5; // 10 + 5 = 15
                Console.WriteLine($"Журналы равны: {j1 == j2}"); // True
                Console.WriteLine($"j1 > j2: {j1 > j2}"); // False
                Console.WriteLine();
            }

            { 
                Shop s1 = new Shop("Бутик", 50.0);
                Shop s2 = new Shop("Склад", 100.0);

                s1 = s1 + 50.0; // 50 + 50 = 100
                Console.WriteLine($"Магазины равны: {s1.Equals(s2)}"); // True
                Console.WriteLine($"s2 < s1: {s2 < s1}"); // False
            }
        }
    }
}
#endif //H_W_5