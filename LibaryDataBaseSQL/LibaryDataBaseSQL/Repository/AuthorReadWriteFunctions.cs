using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDataBaseSQL.Repository
{
    public class AuthorReadWriteFunctions
    {
        static string connectionString = @"SERVER = 192.168.23.201,1433\SQLEXPRESS; DATABASE = Bib; USER ID = Adel; PASSWORD = Passw0rd";

        public void SearchAuthor(string AuthorName, out bool authorFound)
        {
            authorFound = false;
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Auth = @Author", sqlconn);
                    cmd.Parameters.AddWithValue("@Author", AuthorName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        authorFound = true;
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

        public void CreateAuthor(string AuthorNameC)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO Author VALUES (@FullName)", sqlconn);
                    cmd.Parameters.AddWithValue("@Author", AuthorNameC);

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
