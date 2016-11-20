using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public class BookListService
    {
        public static void AddBook(IBookStorage storage, Book book)
        {
            foreach (Book item in storage)
            {
                if (item.Equals(book))
                {
                    throw new ArgumentOutOfRangeException(nameof(book));
                }
            }
            storage.Add(book);
        }

        public static void RemoveBook(IBookStorage storage, Book book)
        {
            bool wasFound = false;
            foreach (Book item in storage)
            {
                if (item.Equals(book))
                {
                    wasFound = true;
                    break;
                }
            }

            if (wasFound)
            {
                storage.Remove(book);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(book));
            }
        }

        public static Book FindBookByTag(IBookStorage storage, Predicate<Book> match)
        {
            foreach (Book item in storage)
            {
                if (match(item))
                {
                    return item;
                }
            }
            return null;
        }

        public static IBookStorage SortBookByTag(IBookStorage storage)
        {
            return SortBookByTag(storage, Comparer<Book>.Default, true);
        }
        public static IBookStorage SortBookByTag(IBookStorage storage, IComparer<Book> comparer)
        {
            return SortBookByTag(storage, comparer, true);
        }
        public static IBookStorage SortBookByTag(IBookStorage storage, IComparer<Book> comparer, bool asc)
        {
            if (asc)
            {
                return new BookListStorage(storage.OrderBy(t => t, comparer));
            }
            else
            {
                return new BookListStorage(storage.OrderByDescending(t => t, comparer));
            }
        }
    }
}
