using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDataBaseSQL.Repository
{
    public class BorrowerReadWriteFunctions
    {
        static string connectionString = @"SERVER = 192.168.23.201,1433\SQLEXPRESS; DATABASE = Bib; USER ID = Adel; PASSWORD = Passw0rd";

        public void SearchBorrower(string BorrworName, out bool borrowFound)
        {
            borrowFound = false;
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Borrower = @Borrower", sqlconn);
                    cmd.Parameters.AddWithValue("@Borrower", BorrworName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        borrowFound = true;
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

        public void CreateBorrower(string BorrworNameC, int Book_Id)
        {
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                borrowerCreate(BorrworNameC, sqlconn);
            }
        }

        private void borrowerCreate(string borrworNameC, SqlConnection sqlconn)
        {
            throw new NotImplementedException();
        }

        private static void borrowerCreate(string BorrworNameC, int Book_Id, SqlConnection sqlConn)
        {
            try
            {
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Borrower (FullName, Book_Id) VALUES (@FullName, @Book_Id);", sqlConn);
                cmd.Parameters.AddWithValue("@FullName", BorrworNameC);
                cmd.Parameters.AddWithValue("@Book_Id", Book_Id);

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nBorrower Created Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
