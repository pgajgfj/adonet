using System;
using System.Linq;

namespace LibraryApp
{
    class Program
    {
        static void Main()
        {
            DbInitializer.Seed();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("📚 Бібліотека");
                Console.WriteLine("1. Показати всі книги");
                Console.WriteLine("2. Додати книгу");
                Console.WriteLine("3. Видалити книгу");
                Console.WriteLine("0. Вихід");
                Console.Write("👉 Виберіть дію: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowBooks();
                        break;
                    case "2":
                        AddBook();
                        break;
                    case "3":
                        DeleteBook();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("❌ Невідомий вибір, спробуйте ще раз.");
                        break;
                }
            }
        }


        static void ShowBooks()
        {
            using (var db = new LibraryContext())
            {
                var books = db.Books.ToList();
                if (books.Count == 0)
                {
                    Console.WriteLine("📭 Немає книг у бібліотеці.");
                }
                else
                {
                    Console.WriteLine("\n📖 Список книг:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"[{book.Id}] {book.Title} - {book.PageCount} стор. ({book.Language})");
                    }
                }
            }
            Console.WriteLine("\n🔄 Натисніть Enter, щоб повернутися...");
            Console.ReadLine();
        }


        static void AddBook()
        {
            using (var db = new LibraryContext())
            {
                Console.Write("\n📌 Введіть назву книги: ");
                string title = Console.ReadLine();

                Console.Write("📄 Введіть кількість сторінок: ");
                int pages = int.Parse(Console.ReadLine());

                Console.Write("🌍 Введіть мову книги: ");
                string language = Console.ReadLine();

                Console.Write("✍ Введіть ім'я автора: ");
                string authorName = Console.ReadLine();

                var author = db.Authors.FirstOrDefault(a => a.Name == authorName);
                if (author == null)
                {
                    author = new Author { Name = authorName };
                    db.Authors.Add(author);
                    db.SaveChanges();
                }

               
                var book = new Book { Title = title, PageCount = pages, Language = language, AuthorId = author.Id };
                db.Books.Add(book);
                db.SaveChanges();

                Console.WriteLine("✅ Книга додана успішно!");
            }
            Console.WriteLine("\n🔄 Натисніть Enter, щоб повернутися...");
            Console.ReadLine();
        }

        
        static void DeleteBook()
        {
            using (var db = new LibraryContext())
            {
                Console.Write("\n❌ Введіть ID книги для видалення: ");
                int id = int.Parse(Console.ReadLine());

                var book = db.Books.Find(id);
                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                    Console.WriteLine("✅ Книга видалена!");
                }
                else
                {
                    Console.WriteLine("❌ Книга не знайдена.");
                }
            }
            Console.WriteLine("\n🔄 Натисніть Enter, щоб повернутися...");
            Console.ReadLine();
        }
    }
}
