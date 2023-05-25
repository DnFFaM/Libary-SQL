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
        public void CreateBorrower(string BorrowerName, int BookId)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                borrowerCreate(BorrowerName, BookId, sqlConn);
            }
        }
        private static void borrowerCreate(string BorrowerName, int BookId, SqlConnection sqlConn)
        {
            try
            {
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO Borrower (FullName, Book_Id) VALUES (@FullName, @Book_Id);", sqlConn);
                cmd.Parameters.AddWithValue("@FullName", BorrowerName);
                cmd.Parameters.AddWithValue("@Book_Id", BookId);

                cmd.ExecuteNonQuery();
                Console.WriteLine("\nBorrower Created Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteBorrower(string borrowerName, out bool borrowerDeleted)
        {
            borrowerDeleted = false;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Search for the borrower in the RentedBooks table
                    SqlCommand searchCmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Borrower = @Borrower", sqlConn);
                    searchCmd.Parameters.AddWithValue("@Borrower", borrowerName);

                    SqlDataReader reader = searchCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Borrower found, delete their records
                        reader.Close();

                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM RentedBooks WHERE Borrower = @Borrower", sqlConn);
                        deleteCmd.Parameters.AddWithValue("@Borrower", borrowerName);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Borrower '{0}' and their rented books have been deleted.", borrowerName);
                            borrowerDeleted = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found for borrower '{0}'.", borrowerName);
                    }

                    reader.Close();
                    sqlConn.Close();
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
