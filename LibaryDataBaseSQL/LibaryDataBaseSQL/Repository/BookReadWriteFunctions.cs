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
