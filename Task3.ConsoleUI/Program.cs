using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.LogicBook;
using NLog;
using System.IO;

namespace Task3.ConsoleUI
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            BookListService bls = new BookListService();
            
            string choise;
            do
            {
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Remove book");
                Console.WriteLine("3. Search book");
                Console.WriteLine("4. Sort storage");
                Console.WriteLine("5. From file");
                Console.WriteLine("6. To file");
                Console.WriteLine("7. Show storage");
                Console.WriteLine("0. Exit");
                choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        AddBook(bls);
                        break;
                    case "2":
                        RemoveBook(bls);
                        break;
                    case "3":
                        SearchBook(bls);
                        break;
                    case "4":
                        SortStorage(bls);
                        break;
                    case "5":
                        FromFile(bls);
                        break;
                    case "6":
                        ToFile(bls);
                        break;
                    case "7":
                        ShowStorage(bls);
                        break;
                }

            } while (choise != "0");
        }

        static void ToFile(BookListService bls)
        {
            Console.WriteLine("Path to file:");
            string path = Console.ReadLine();
            try
            {
                bls.Save(path);
            }
            catch (IOException ex)
            {
                logger.Error(ex.Message);
            }
            ShowStorage(bls);
            Console.ReadKey();
        }

        static void FromFile(BookListService bls)
        {
            Console.WriteLine("Path to file: ");
            string path = Console.ReadLine();
            try
            {
                bls.Load(path);
            }
            catch (IOException ex)
            {
                logger.Error(ex.Message);
            }
            ShowStorage(bls);
            Console.ReadKey();
        }

        static void SortStorage(BookListService bls)
        {
            Console.WriteLine("Before");
            ShowStorage(bls);
            Console.WriteLine("After");
            bls = new BookListService(bls.SortBookByTag((t1, t2) => t1.CompareTo(t2)));
            ShowStorage(bls);
        }

        static void SearchBook(BookListService bls)
        {
            Console.WriteLine("ISBN: ");
            long isbn = ReadLong();
            Book b = bls.FindBookByTag( t => t.ISBN == isbn);
            if (b == null)
                Console.WriteLine("Not found.");
            else
                Console.WriteLine(b.ToString());
            Console.ReadKey();
        }

        static void RemoveBook(BookListService bls)
        {
            ShowStorage(bls);
            Console.WriteLine("Number of remove rec: ");
            int i = ReadInt();
            int j = 0;
            foreach (Book item in bls)
            {
                if (j == i)
                {
                    try
                    {
                        bls.RemoveBook(item);
                    }
                    catch (LogicBook.Exceptions.BookListServiceRemoveNotExistingItemException ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.Message);
                    }
                }
                j++;
            }
            ShowStorage(bls);
        }

        static void ShowStorage(BookListService bls)
        {
            int i = 0;
            foreach (Book item in bls)
            {
                Console.WriteLine($"#{i++}.   {item.ToString()}");
            }
        }

        static void AddBook(BookListService bls)
        {
            Console.Write("ISBN: ");
            Book b = new Book(ReadLong());
            Console.Write("Author: ");
            b.Author = Console.ReadLine();
            Console.Write("Name: ");
            b.Name = Console.ReadLine();
            Console.Write("Year: ");
            b.Year = ReadInt();
            Console.Write("Publisher: ");
            b.Publisher = Console.ReadLine();
            try
            {
                bls.AddBook(b);
            }
            catch (LogicBook.Exceptions.BookListServiceAddExistingItemException ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }

    #region Input Service
        static long ReadLong()
        {
            long num;
            string strNum;
            do
            {
                strNum = Console.ReadLine();
            } while (!long.TryParse(strNum, out num));
            return num;
        }

        static int ReadInt()
        {
            int num;
            string strNum;
            do
            {
                strNum = Console.ReadLine();
            } while (!int.TryParse(strNum, out num));
            return num;
        }
    #endregion

    }
}
