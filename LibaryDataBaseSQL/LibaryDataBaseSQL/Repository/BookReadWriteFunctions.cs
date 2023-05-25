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
        public void SearchBook(string bookName, out bool bookFound)
        {
            bookFound = false;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Title = @Title", sqlConn);
                    cmd.Parameters.AddWithValue("@Title", bookName);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            bookFound = true;
                            string author = reader["Auth"].ToString();
                            string title = reader["Title"].ToString();
                            string borrower = reader["Borrower"].ToString();

                            if (string.IsNullOrEmpty(borrower))
                            {
                                Console.WriteLine("Author: {0} | Title: {1}", author, title);
                            }
                            else
                            {
                                Console.WriteLine("Author: {0} | Title: {1} | Borrower: {2}", author, title, borrower);
                            }
                        }
                    }
                    else
                    {
                        reader.Close();

                        cmd = new SqlCommand("SELECT * FROM Book WHERE Title = @Title", sqlConn);
                        cmd.Parameters.AddWithValue("@Title", bookName);

                        reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                bookFound = true;
                                string title = reader["Title"].ToString();
                                string authorId = reader["Author_Id"].ToString();

                                Console.WriteLine("\nTitle: {0} | Author Id: {1}", title, authorId);
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo books found with the title '{0}'.", bookName);
                        }
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
        public void CreateBook(string bookName, int authorId)
        {
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConn.Open();

                    string authorName = GetAuthorName(sqlConn, authorId); 

                    SqlCommand cmd = new SqlCommand("INSERT INTO Book (Title, Author_Id) VALUES (@Title, @Author_Id);", sqlConn);
                    cmd.Parameters.AddWithValue("@Title", bookName);
                    cmd.Parameters.AddWithValue("@Author_Id", authorId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("\nBook created successfully. Author: {0}", authorName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void DeleteBook(string bookName, out bool bookDeleted)
        {
            bookDeleted = false;
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Check if the book is rented
                    SqlCommand rentedCmd = new SqlCommand("SELECT * FROM RentedBooks WHERE Title = @Title AND Borrower IS NOT NULL", sqlConn);
                    rentedCmd.Parameters.AddWithValue("@Title", bookName);

                    SqlDataReader rentedReader = rentedCmd.ExecuteReader();
                    if (rentedReader.HasRows)
                    {
                        // Book is rented and has a borrower, cannot delete
                        Console.WriteLine("\nThe book '{0}' is currently rented and cannot be deleted.", bookName);
                        rentedReader.Close();
                        return;
                    }
                    rentedReader.Close();

                    // Check if the book exists in the Book table
                    SqlCommand bookCmd = new SqlCommand("SELECT * FROM Book WHERE Title = @Title", sqlConn);
                    bookCmd.Parameters.AddWithValue("@Title", bookName);

                    SqlDataReader bookReader = bookCmd.ExecuteReader();
                    if (bookReader.HasRows)
                    {
                        // Book found, delete it
                        bookReader.Close();

                        SqlCommand deleteCmd = new SqlCommand("DELETE FROM Book WHERE Title = @Title", sqlConn);
                        deleteCmd.Parameters.AddWithValue("@Title", bookName);

                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("\nThe book '{0}' has been deleted.", bookName);
                            bookDeleted = true;
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo books found with the title '{0}'.", bookName);
                    }

                    bookReader.Close();
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
        public string GetAuthorName(SqlConnection connection, int authorId)
        {
            SqlCommand cmd = new SqlCommand("SELECT FullName FROM Author WHERE Id = @AuthorId", connection);
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetString(0); 
                }
                else
                {
                    throw new Exception("\nAuthor not found.");
                }
            }
        }
    }
}
