using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=First;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        Console.WriteLine("DB opened");

            //        //var query = @"INSERT INTO Students VALUES (@FirstName, @LastName, @BirthDate)";

            //        //using (var cmd = new SqlCommand(query, connection)) 
            //        //{
            //        //    cmd.Parameters.AddWithValue("@FirstName", "Ivan");
            //        //    cmd.Parameters.AddWithValue("@LastName", "Ivanov");
            //        //    cmd.Parameters.AddWithValue("@BirthDate", new DateTime(2000,12,12));

            //        //    int rowsAffected = cmd.ExecuteNonQuery();
            //        //    Console.WriteLine(rowsAffected);
            //        //}

            //        //using (var cmd = connection.CreateCommand())
            //        //{

            //        //}

            //        //var query = @"SELECT * FROM Students";
            //        //using (var cmd = new  SqlCommand(query, connection))
            //        //using (var reader = cmd.ExecuteReader()) 
            //        //{
            //        //    while (reader.Read())
            //        //    {
            //        //        Console.WriteLine($"ID:{reader["Id"]}\tName:{reader["FirstName"]} {reader["LastName"]}\tBirthDate:{reader["BirthDate"]}");
            //        //    }
            //        //}
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"Error: {ex.Message}");
            //    }

            //}

            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StudentsGrades;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection opened successfully!");
                    string query;
                    while (true)
                    {
                        Console.WriteLine("Choose an option from below to execute (Enter a digit from 1 to 11):");
                        Console.WriteLine("\t1 - Show all info from DB." +
                                          "\n\t2 - Show full names of students." +
                                          "\n\t3 - Show all average grades." +
                                          "\n\t4 - Show full names of students with minmal grade of certain level." +
                                          "\n\t5 - Show subjects with minimal average grades." +
                                          "\n\t6 - Show minimal average grade." +
                                          "\n\t7 - Show maximal average grade." +
                                          "\n\t8 - Show number of stidents with minimal average grade at mathematics." +
                                          "\n\t9 - Show number of stidents with maximal average grade at mathematics." +
                                          "\n\t10 - Show number of stidents in each group." +
                                          "\n\t11 - Show average grade of each group." +
                                          "\n\tAny other key to exit.");
                        int.TryParse(Console.ReadLine(), out int result);
                        switch (result)
                        {
                            case 1:
                                query = @"SELECT * FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"Full name: {reader["FullName"]}" +
                                                         $"\nGroup name:{reader["GroupName"]}" +
                                                         $"\nYear average grade:{reader["YearAverageGrade"]}" +
                                                         $"\nMinimal average grade:{reader["MinAverageGradeSubj"]}" +
                                                         $"\nMaximal average grade:{reader["MaxAverageGradeSubj"]}");
                                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                                    }
                                }
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                break;
                            case 2:
                                query = @"SELECT FullName FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"Full name: {reader["FullName"]}");
                                    }
                                }
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                break;
                            case 3:
                                query = @"SELECT YearAverageGrade FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"Average grade: {reader["YearAverageGrade"]}");
                                    }
                                }
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                break;
                            case 4:
                                Console.WriteLine("Enter the average grade as a lowest possible to sort students: ");
                                int.TryParse(Console.ReadLine(), out int minimal);
                                query = $"SELECT FullName, YearAverageGrade FROM Grades WHERE YearAverageGrade > {minimal}";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["FullName"]}");
                                    }
                                }
                                break;
                            case 5:

                                query = $"SELECT DISTINCT MinAverageGradeSubj FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["MinAverageGradeSubj"]}");
                                    }
                                }
                                break;
                            case 6:
                                query = "SELECT MAX(YearAverageGrade) AS maxGrade FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    reader.Read();
                                    Console.WriteLine(reader["maxGrade"]);
                                    reader.Close();
                                }
                                break;
                            case 7:
                                query = "SELECT MIN(YearAverageGrade) AS minGrade FROM Grades";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    reader.Read();
                                    Console.WriteLine(reader["minGrade"]);
                                    reader.Close();
                                }
                                break;
                            case 8:
                                query = "SELECT COUNT(*) AS count FROM Grades WHERE MinAverageGradeSubj = 'Mathematics'";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    reader.Read();
                                    Console.WriteLine(reader["count"]);
                                    reader.Close();
                                }
                                break;
                            case 9:
                                query = "SELECT COUNT(*) AS count FROM Grades WHERE MaxAverageGradeSubj = 'Mathematics'";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    reader.Read();
                                    Console.WriteLine(reader["count"]);
                                    reader.Close();
                                }
                                break;
                            case 10:
                                query = $"SELECT COUNT(*) AS count, GroupName AS [group] " +
                                                 $"FROM Grades GROUP BY GroupName";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["group"]} - {reader["count"]}");
                                    }
                                }
                                break;
                            case 11:
                                query = $"SELECT GroupName AS [group], AVG(YearAverageGrade) AS avg_grade FROM Grades GROUP BY GroupName";
                                using (var cmd = new SqlCommand(query, connection))
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{reader["group"]} - {reader["avg_grade"]}");
                                    } 
                                }
                                break;
                            default:
                                return;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }
    }
}
