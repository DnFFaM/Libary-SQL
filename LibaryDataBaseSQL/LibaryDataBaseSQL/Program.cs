using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LibaryDataBaseSQL.Repository;

namespace LibaryDataBaseSql
{
    class Program
    {

        static void Main(string[] args)
        {
            bool Loop = true;
            string textToMenu;

            while (Loop == true)
            {
            start:

                textToMenu = ("\nSQL Library Application !!! \n\n");
                textToMenu += ("\n Type 1 For Search Menu \n");
                textToMenu += ("\n Type 2 For Create Menu \n");
                textToMenu += ("\n Type 3 For Delete Menu \n\n");
                Console.Write("{0}", textToMenu);
                string? figur = Console.ReadLine();
                try
                {

                    if (figur == "1")
                    {
                        string textToSearchMenu;

                        textToSearchMenu = ("\nSearch Menu !!! \n\n");
                        textToSearchMenu += ("\n Type 1 To Search For Author \n");
                        textToSearchMenu += ("\n Type 2 To Search For Book \n");
                        textToSearchMenu += ("\n Type 3 To Search For Borrower \n");
                        textToSearchMenu += ("\n Else 0 To Go Back To Main Menu \n");
                        Console.WriteLine("{0}", textToSearchMenu);
                        string? Searchfigur = Console.ReadLine();


                        if (Searchfigur == "1")
                        {
                            Console.WriteLine("\nEnter an Author Name to search for:");
                            string authorName = Console.ReadLine();
                            bool authorFound;
                            AuthorReadWriteFunctions authorRWF = new AuthorReadWriteFunctions();
                            authorRWF.SearchAuthor(authorName, out authorFound);

                            if (authorFound)
                            {
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }

                        if (Searchfigur == "2")
                        {
                            Console.WriteLine("\nEnter a Book Name to search for:");
                            string BookName = Console.ReadLine();
                            bool bookFound;

                            BookReadWriteFunctions bookrwf = new BookReadWriteFunctions();
                            bookrwf.SearchBook(BookName, out bookFound);
                            if (bookFound)
                            {
                                Console.WriteLine("\nBooks: ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("\nNo Book found With The Name " + BookName + ".");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }

                        if (Searchfigur == "3")
                        {
                            Console.WriteLine("\nEnter a Borrower Name to search for:");
                            string BorrworName = Console.ReadLine();
                            bool borrowFound;

                            BorrowerReadWriteFunctions borrowerrwf = new BorrowerReadWriteFunctions();
                            borrowerrwf.SearchBorrower(BorrworName, out borrowFound);
                            if (borrowFound)
                            {
                                Console.WriteLine("\nBorrowers: ");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else
                            {
                                Console.WriteLine("\nNo Borrower found With The Name " + BorrworName + ".");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }

                        if (Searchfigur == "0")
                        {
                            goto start;
                        }
                    }
                    if (figur == "2")
                    {
                        string textToCreateMenu;

                        textToCreateMenu = ("\nCreate Menu !!! \n\n");
                        textToCreateMenu += ("\n Type 1 To Add a New Author \n");
                        textToCreateMenu += ("\n Type 2 To Add a New Book \n");
                        textToCreateMenu += ("\n Type 3 To Add a New Borrower \n");
                        textToCreateMenu += ("\n Type 0 To Go Back To Main Menu\n");
                        Console.WriteLine(textToCreateMenu);
                        string? Createfigur = Console.ReadLine();


                        if (Createfigur == "1")
                        {
                            Console.WriteLine("\nEnter The Author Name To Add a New Author:");
                            string AuthorNameC = Console.ReadLine();
                            bool authorCreate;

                            AuthorReadWriteFunctions Authorrwf = new AuthorReadWriteFunctions();
                            Authorrwf.CreateAuthor(AuthorNameC, out authorCreate);
                            if (authorCreate == true)
                            {
                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                            }
                        }

                        if (Createfigur == "2")
                        {
                            try
                            {
                                Console.WriteLine("\nEnter The Book Name To Add it:");
                                string bookName = Console.ReadLine();

                                Console.WriteLine("\nEnter An Author Id To Link The Book With:");
                                int authorId = int.Parse(Console.ReadLine());

                                BookReadWriteFunctions bookRWF = new BookReadWriteFunctions();
                                bookRWF.CreateBook(bookName, authorId);

                                var key = Console.ReadKey().Key;
                                Console.Clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }

                        if (Createfigur == "3")
                        {
                            Console.WriteLine("\nEnter The Borrower Name To Add a Borrwer:");
                            string BorrworNameC = Console.ReadLine();

                            Console.WriteLine("\nEnter An Book Id To Link The Book With:");
                            int Book_Id = int.Parse(Console.ReadLine());

                            BorrowerReadWriteFunctions borrowerrwf = new BorrowerReadWriteFunctions();
                            borrowerrwf.CreateBorrower(BorrworNameC, Book_Id);
                        }

                        if (Createfigur == "0")
                        {
                            goto start;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"There is something wrong in our application, please look at this message: {ex.Message}");
                    Console.ReadKey();
                    Console.Clear();
                    goto start;
                }
            }
        }
    }
}
