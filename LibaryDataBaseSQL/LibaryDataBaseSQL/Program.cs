using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LibaryDataBaseSql
{
    class Program
    {
        static string connectionString = @"SERVER = 192.168.23.201,1433\SQLEXPRESS; DATABASE = Bib; USER ID = Adel; PASSWORD = Passw0rd";
        static SqlConnection sqlconn = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            using (sqlconn)
            {
                try
                {
                    Console.WriteLine("Open SqlConn");
                    sqlconn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM RentedBooks", sqlconn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("Author: {0} | Title: {1} | Borrower: {2}", reader["Auth"], reader["Title"], reader["Borrower"]);
                    }
                    reader.Close();
                    sqlconn.Close();
                    Console.WriteLine("Closed SqlConn");
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
