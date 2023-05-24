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
        public void SearchAuthor(string authorName, out bool authorFound)
        {
            authorFound = false;
            using (SqlConnection sqlconn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlconn.Open();

                    // Search in the RentedBooks table
                    SqlCommand rentedBooksCmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Auth = @Author", sqlconn);
                    rentedBooksCmd.Parameters.AddWithValue("@Author", authorName);

                    SqlDataReader rentedBooksReader = rentedBooksCmd.ExecuteReader();

                    while (rentedBooksReader.Read())
                    {
                        authorFound = true;
                        Console.WriteLine("Author: {0} | Title: {1} | Borrower: {2}", rentedBooksReader["Auth"], rentedBooksReader["Title"], rentedBooksReader["Borrower"]);
                    }

                    rentedBooksReader.Close();

                    if (!authorFound)
                    {
                        // Search in the NonRentedBooks table
                        SqlCommand nonRentedBooksCmd = new SqlCommand("SELECT BookId FROM NonRentedBooks WHERE Auth = @Author AND BookId IS NULL", sqlconn);
                        nonRentedBooksCmd.Parameters.AddWithValue("@Author", authorName);

                        SqlDataReader nonRentedBooksReader = nonRentedBooksCmd.ExecuteReader();

                        if (nonRentedBooksReader.HasRows)
                        {
                            authorFound = true;
                            Console.WriteLine("\nAuthor found in NonRentedBooks table:");

                            while (nonRentedBooksReader.Read())
                            {
                                int bookId = (int)nonRentedBooksReader["BookId"];
                                string bookTitle = GetBookTitle(sqlconn, bookId);
                                Console.WriteLine("Book Attached: {0}", bookTitle);
                            }
                        }

                        nonRentedBooksReader.Close();
                    }

                    if (!authorFound)
                    {
                        // Search in the Author table
                        SqlCommand authorCmd = new SqlCommand("SELECT * FROM Author WHERE FullName = @Author", sqlconn);
                        authorCmd.Parameters.AddWithValue("@Author", authorName);

                        SqlDataReader authorReader = authorCmd.ExecuteReader();

                        if (authorReader.Read())
                        {
                            authorFound = true;
                            Console.WriteLine("\nAuthor Found in Author Table");
                        }
                        else
                        {
                            Console.WriteLine("\nNo Author Found");
                        }

                        authorReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private string GetBookTitle(SqlConnection sqlconn, int bookId)
        {
            string bookTitle = string.Empty;

            SqlCommand cmd = new SqlCommand("SELECT Title FROM Book WHERE Book_Id = @BookId", sqlconn);
            cmd.Parameters.AddWithValue("@BookId", bookId);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                bookTitle = (string)reader["Title"];
            }

            reader.Close();

            return bookTitle;
        }
        public void CreateAuthor(string authorName, out bool authorCreate)
        {
            authorCreate = false;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    int authorId = CreateAuthorRecord(authorName, sqlConn);
                    authorCreate = true;
                    Console.WriteLine($"Created a new author with Name: {authorName}, And With ID: {authorId}.");
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
        public static int CreateAuthorRecord(string authorName, SqlConnection sqlConn)
        {
            sqlConn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO Author (FullName) OUTPUT INSERTED.Id VALUES (@FullName);", sqlConn);
            cmd.Parameters.AddWithValue("@FullName", authorName);

            int authorId = (int)cmd.ExecuteScalar();

            sqlConn.Close();

            return authorId;
        }


    }
}
