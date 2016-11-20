using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Logic;

namespace Task3.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BookListStorage bls = new BookListStorage();
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

        static void ToFile(BookListStorage bls)
        {
            Console.WriteLine("Path to file:");
            string path = Console.ReadLine();
            BookStorageIO.StorageWriter(bls, path);
            ShowStorage(bls);
            Console.ReadKey();
        }

        static void FromFile(BookListStorage bls)
        {
            Console.WriteLine("Path to file: ");
            string path = Console.ReadLine();
            BookStorageIO.StorageReader(bls, path);
            ShowStorage(bls);
            Console.ReadKey();
        }

        static void SortStorage(BookListStorage bls)
        {
            Console.WriteLine("Before");
            ShowStorage(bls);
            Console.WriteLine("After");
            IBookStorage ibs = bls;
            bls = (BookListStorage)BookListService.SortBookByTag(ibs);
            ShowStorage(bls);
        }

        static void SearchBook(BookListStorage bls)
        {
            Console.WriteLine("ISBN: ");
            long isbn = ReadLong();
            Book b = BookListService.FindBookByTag(bls, t => t.ISBN == isbn);
            if (b == null)
                Console.WriteLine("Not found.");
            else
                Console.WriteLine(b.ToString());
            Console.ReadKey();
        }

        static void RemoveBook(BookListStorage bls)
        {
            ShowStorage(bls);
            Console.WriteLine("Number of remove rec: ");
            int i = ReadInt();
            int j = 0;
            foreach (Book item in bls)
            {
                if (j == i)
                {
                    BookListService.RemoveBook(bls, item);
                }
                j++;
            }
            ShowStorage(bls);
        }

        static void ShowStorage(BookListStorage bls)
        {
            int i = 0;
            foreach (Book item in bls)
            {
                Console.WriteLine($"#{i++}.   {item.ToString()}");
            }
        }

        static void AddBook(BookListStorage bls)
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
            BookListService.AddBook(bls, b);
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
