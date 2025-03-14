using System;

public class Program
{
    public static void Main(string[] args)
    {
        var bookstore = new Bookstore();

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("1. Реєстрація");
            Console.WriteLine("2. Вхід");
            Console.WriteLine("3. Додати книгу");
            Console.WriteLine("4. Переглянути всі книги");
            Console.WriteLine("5. Вийти");
            Console.Write("Виберіть опцію: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterUser(bookstore);
                    break;
                case "2":
                    LoginUser(bookstore);
                    break;
                case "3":
                    AddBook(bookstore);
                    break;
                case "4":
                    bookstore.ViewAllBooks();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Невірна опція. Спробуйте ще раз.");
                    break;
            }
        }

        Console.WriteLine("Програма завершена.");
    }

    private static void RegisterUser(Bookstore bookstore)
    {
        Console.Write("Введіть логін: ");
        string username = Console.ReadLine();

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        if (bookstore.RegisterUser(username, password))
            Console.WriteLine("Реєстрація успішна!");
        else
            Console.WriteLine("Користувач з таким логіном вже існує.");

        Console.ReadKey();
    }

    private static void LoginUser(Bookstore bookstore)
    {
        Console.Write("Введіть логін: ");
        string username = Console.ReadLine();

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        if (bookstore.UserExists(username, password))
            Console.WriteLine("Вхід успішний!");
        else
            Console.WriteLine("Невірний логін або пароль.");

        Console.ReadKey();
    }

    private static void AddBook(Bookstore bookstore)
    {
        Console.Write("Назва книги: ");
        string title = Console.ReadLine();

        int authorId = ReadIntInput("ID автора");
        int publisherId = ReadIntInput("ID видавництва");
        int genreId = ReadIntInput("ID жанру");
        int pages = ReadIntInput("Кількість сторінок");
        int year = ReadIntInput("Рік видання");

        Console.Write("Собівартість книги: ");
        decimal cost = decimal.Parse(Console.ReadLine());

        Console.Write("Ціна продажу: ");
        decimal salePrice = decimal.Parse(Console.ReadLine());

        Console.Write("Чи є книга продовженням іншої? (true/false): ");
        bool isSequel = bool.Parse(Console.ReadLine());

        int sequelBookId = 0;
        if (isSequel)
        {
            sequelBookId = ReadIntInput("ID попередньої книги");
        }

        bookstore.AddBook(title, authorId, publisherId, genreId, pages, year, cost, salePrice, isSequel, sequelBookId);
        Console.WriteLine("Книга успішно додана!");

        Console.ReadKey();
    }

    private static int ReadIntInput(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (int.TryParse(Console.ReadLine(), out value))
                return value;

            Console.WriteLine("Некоректне значення. Введіть число.");
        }
    }
}
