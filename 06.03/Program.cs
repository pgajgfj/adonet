using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StorageDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                CreateTables(connection);

                
                InsertData(connection);


                UpdateData(connection);


                DeleteData(connection);
            }
        }

        static void CreateTables(SqlConnection connection)
        {
            using (var command = new SqlCommand("IF OBJECT_ID('Products', 'U') IS NULL CREATE TABLE Products (Id INT PRIMARY KEY, Name NVARCHAR(50), TypeId INT, SupplierId INT, Quantity INT, Cost DECIMAL(10, 2), DeliveryDate DATETIME)", connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("IF OBJECT_ID('Types', 'U') IS NULL CREATE TABLE Types (Id INT PRIMARY KEY, Name NVARCHAR(50))", connection))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("IF OBJECT_ID('Suppliers', 'U') IS NULL CREATE TABLE Suppliers (Id INT PRIMARY KEY, Name NVARCHAR(50))", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        static void InsertData(SqlConnection connection)
        {
            using (var command = new SqlCommand("INSERT INTO Types (Name) VALUES (@Name)", connection))
            {
                command.Parameters.AddWithValue("@Name", "Тип 1");
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("INSERT INTO Suppliers (Name) VALUES (@Name)", connection))
            {
                command.Parameters.AddWithValue("@Name", "Постачальник 1");
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("INSERT INTO Products (Name, TypeId, SupplierId, Quantity, Cost, DeliveryDate) VALUES (@Name, @TypeId, @SupplierId, @Quantity, @Cost, @DeliveryDate)", connection))
            {
                command.Parameters.AddWithValue("@Name", "Товар 1");
                command.Parameters.AddWithValue("@TypeId", 1);
                command.Parameters.AddWithValue("@SupplierId", 1);
                command.Parameters.AddWithValue("@Quantity", 10);
                command.Parameters.AddWithValue("@Cost", 10.5m);
                command.Parameters.AddWithValue("@DeliveryDate", DateTime.Now);
                command.ExecuteNonQuery();
            }
        }

        static void UpdateData(SqlConnection connection)
        {
            using (var command = new SqlCommand("UPDATE Products SET Name = @Name, Quantity = @Quantity WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.Parameters.AddWithValue("@Name", "Товар 1 (оновлено)");
                command.Parameters.AddWithValue("@Quantity", 15);
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("UPDATE Types SET Name = @Name WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.Parameters.AddWithValue("@Name", "Тип 1 (оновлено)");
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("UPDATE Suppliers SET Name = @Name WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.Parameters.AddWithValue("@Name", "Постачальник 1 (оновлено)");
                command.ExecuteNonQuery();
            }
        }

        static void DeleteData(SqlConnection connection)
        {
            using (var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("DELETE FROM Types WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.ExecuteNonQuery();
            }

            using (var command = new SqlCommand("DELETE FROM Suppliers WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                command.ExecuteNonQuery();
            }
        }
    }
}