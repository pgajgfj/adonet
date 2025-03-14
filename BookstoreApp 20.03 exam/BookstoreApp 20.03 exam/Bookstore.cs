using System;
using System.Data.SqlClient;

public class Bookstore
{
    private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Bookstore;Trusted_Connection=True;";

    public bool RegisterUser(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    public bool UserExists(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                return (int)command.ExecuteScalar() > 0;
            }
        }
    }

    public void AddBook(string title, int authorId, int publisherId, int genreId, int pages, int year, decimal cost, decimal salePrice, bool isSequel, int sequelBookId)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Books (Title, AuthorId, PublisherId, GenreId, Pages, Year, Cost, SalePrice, IsSequel, SequelBookId) " +
                           "VALUES (@Title, @AuthorId, @PublisherId, @GenreId, @Pages, @Year, @Cost, @SalePrice, @IsSequel, @SequelBookId)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@AuthorId", authorId);
                command.Parameters.AddWithValue("@PublisherId", publisherId);
                command.Parameters.AddWithValue("@GenreId", genreId);
                command.Parameters.AddWithValue("@Pages", pages);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@Cost", cost);
                command.Parameters.AddWithValue("@SalePrice", salePrice);
                command.Parameters.AddWithValue("@IsSequel", isSequel);
                command.Parameters.AddWithValue("@SequelBookId", sequelBookId == 0 ? DBNull.Value : (object)sequelBookId);

                command.ExecuteNonQuery();
            }
        }
    }

    public void ViewAllBooks()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Title, Pages, Year, SalePrice FROM Books";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                Console.WriteLine("\n--- Список усіх книг ---");
                while (reader.Read())
                {
                    Console.WriteLine($"Назва: {reader["Title"]}, Сторінок: {reader["Pages"]}, Рік: {reader["Year"]}, Ціна: {reader["SalePrice"]} грн");
                }
            }
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
        Console.ReadKey();
    }

    public void SearchBooks(string title = null, string author = null, string genre = null)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Books WHERE " +
                           "(Title LIKE @Title OR @Title IS NULL) AND " +
                           "(AuthorId IN (SELECT Id FROM Authors WHERE Name LIKE @Author) OR @Author IS NULL) AND " +
                           "(GenreId IN (SELECT Id FROM Genres WHERE Name LIKE @Genre) OR @Genre IS NULL)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Title", title != null ? $"%{title}%" : (object)DBNull.Value);
                command.Parameters.AddWithValue("@Author", author != null ? $"%{author}%" : (object)DBNull.Value);
                command.Parameters.AddWithValue("@Genre", genre != null ? $"%{genre}%" : (object)DBNull.Value);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Title"]}, Рік: {reader["Year"]}, Ціна: {reader["SalePrice"]} грн");
                    }
                }
            }
        }
    }
}
