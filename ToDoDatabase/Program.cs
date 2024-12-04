using Azure;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace ToDoDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Låt användaren blocka av en uppgift som genomförd genom att uppge ID för uppgiften

            Console.WriteLine("Vilken Uppgift vill du bocka av?");
            int TaskID = int.Parse(Console.ReadLine());

            string connectionString = "Server = (localdb)\\MSSQLLocalDB;Database=ToDoDatabase;Trusted_Connection=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Console.WriteLine("Databasen är öppen");
                    string sqlQuery = "UPDATE Tasks SET Status = @TaskStatus WHERE TaskID = @TaskID";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {

                        command.Parameters.AddWithValue("@TaskStatus", "Avslutad");
                        command.Parameters.AddWithValue("@TaskID", TaskID);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected);

                    }

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }

            //Låt användaren lägg till nya uppgifter genom att mata in informationen om uppgiften.

            Console.WriteLine("Vad ska uppggiten heta?");
            string TaskName = Console.ReadLine();

            Console.WriteLine("Beskriv Uppgiften");
            string TaskDescription = Console.ReadLine();

            Console.WriteLine("Ange Dead line");
            string TaskDeadLine = Console.ReadLine();

            Console.WriteLine("Ange Status");
            string TaskStatus = Console.ReadLine();

            Console.WriteLine("Ange Categori (1= Jobb, 2= Hem, 3= Studier)");
            int TaskCategory = int.Parse(Console.ReadLine());

            string connectionString = "Server = (localdb)\\MSSQLLocalDB;Database=ToDoDatabase;Trusted_Connection=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Console.WriteLine("Databasen är öppen");
                    string sqlQuery = "INSERT INTO Tasks (Title, Description, Deadline, Status, CategoryID) " +
                                      "VALUES (@TaskName, @TaskDescription, @TaskDeadline, @TaskStatus, @TaskCategory)";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {

                        command.Parameters.AddWithValue("@TaskName", TaskName);
                        command.Parameters.AddWithValue("@TaskDescription", TaskDescription);
                        command.Parameters.AddWithValue("@TaskDeadline", TaskDeadLine);
                        command.Parameters.AddWithValue("@TaskStatus", TaskStatus);
                        command.Parameters.AddWithValue("@TaskCategory", TaskCategory);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected);

                    }

                }

            }

            catch (Exception ex)
            {

            }

            //Hämta och presentera alla kategorier med tillhörande uppgifter från databasen

            string connectionString = "Server = (localdb)\\MSSQLLocalDB;Database=ToDoDatabase;Trusted_Connection=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Databasen är öppen");
                    string sqlQury = "SELECT Tasks.Title, Categories.Name FROM Tasks JOIN Categories ON Tasks.CategoryID = Categories.ID";

                    using (SqlCommand command = new SqlCommand(sqlQury, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Tasks");
                            Console.WriteLine("---------------------------");
                            //läsa från databasen
                            while (reader.Read())
                            {
                                Console.WriteLine($"Category Name {reader["Name"]}" +
                                    $" Task Name {reader["Title"]}");
                            }

                        }
                    }

                }

            }

            catch (Exception ex)
            {

            }
        }
    }
}
