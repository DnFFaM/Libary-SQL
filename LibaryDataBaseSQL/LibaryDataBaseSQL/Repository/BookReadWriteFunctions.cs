using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDataBaseSQL.Repository
{
    public class BookReadWriteFunctions
    {
        static string connectionString = @"SERVER = 192.168.23.201,1433\SQLEXPRESS; DATABASE = Bib; USER ID = Adel; PASSWORD = Passw0rd";

        public void SearchBook(string BookName)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Title = @Title", sqlconn);
                    cmd.Parameters.AddWithValue("@Title", BookName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Author: {0} | Title: {1} | Borrower: {2}", reader["Auth"], reader["Title"], reader["Borrower"]);
                    }
                    reader.Close();
                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }

        public void CreateBook(string BookNameC)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Book VALUES (@Title)" ,sqlconn);
                    cmd.Parameters.AddWithValue("@Title", BookNameC);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Author: {0} | Title: {1} | Borrower: {2}", reader["Auth"], reader["Title"], reader["Borrower"]);
                    }
                    reader.Close();
                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }
    }
}
