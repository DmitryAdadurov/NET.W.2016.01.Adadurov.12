using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.LogicSet;
using Task3.LogicBook.Exceptions;

namespace Task3.LogicBook
{
    public class BookListService
    {
        private Set<Book> _storage;

        public BookListService() : this(null, EqualityComparer<Book>.Default)
        {
        }

        public BookListService(IEnumerable<Book> books) : this(books, EqualityComparer<Book>.Default)
        {
        }

        public BookListService(IEnumerable<Book> books, IEqualityComparer<Book> equalityComparer)
        {
            if (equalityComparer == null)
                throw new ArgumentNullException(nameof(equalityComparer));

            if (books == null || books.Count() == 0)
            {
                _storage = new Set<Book>(equalityComparer, 4);
            }
            else
            {
                _storage = new Set<Book>(equalityComparer, books.Count());

                foreach (Book item in books)
                {
                    AddBook(item);
                }
            }
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            foreach (Book item in _storage)
            {
                if (item.Equals(book))
                {
                    throw new BookListServiceAddExistingItemException();
                }
            }
            _storage.Insert(book);
        }

        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            bool wasFound = false;
            foreach (Book item in _storage)
            {
                if (item.Equals(book))
                {
                    wasFound = true;
                    break;
                }
            }

            if (wasFound)
            {
                _storage.Remove(book);
            }
            else
            {
                throw new BookListServiceRemoveNotExistingItemException();
            }
        }

        public Book FindBookByTag(Predicate<Book> match)
        {
            foreach (Book item in _storage)
            {
                if (match(item))
                {
                    return item;
                }
            }
            return null;
        }

        public IEnumerable<Book> SortBookByTag()
        {
            return SortBookByTag(Comparer<Book>.Default, true);
        }
        public IEnumerable<Book> SortBookByTag(IComparer<Book> comparer)
        {
            return SortBookByTag(comparer, true);
        }

        public IEnumerable<Book> SortBookByTag(Comparison<Book> comparer)
        {
            return SortBookByTag(comparer, true);
        }

        public IEnumerable<Book> SortBookByTag(Comparison<Book> comparer, bool asc)
        {
            SortAdapter sa = new SortAdapter(comparer);
            return SortBookByTag(sa, asc);
        }

        public IEnumerable<Book> SortBookByTag(IComparer<Book> comparer, bool asc)
        {
            if (asc)
            {
                return _storage.OrderBy(t => t, comparer);
            }
            else
            {
                return _storage.OrderByDescending(t => t, comparer);
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        public void Load(string filePath)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    Book b = new Book(reader.ReadInt64());
                    b.Author = reader.ReadString();
                    b.Edition = reader.ReadInt32();
                    b.Name = reader.ReadString();
                    b.PageCount = reader.ReadInt32();
                    b.Publisher = reader.ReadString();
                    b.Year = reader.ReadInt32();
                    _storage.Insert(b);
                }
            }
        }

        public void Save(string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.OpenOrCreate)))
            {
                foreach (Book item in _storage)
                {
                    writer.Write(item.ISBN);
                    writer.Write(item.Author);
                    writer.Write(item.Edition);
                    writer.Write(item.Name);
                    writer.Write(item.PageCount);
                    writer.Write(item.Publisher);
                    writer.Write(item.Year);
                }
            }
        }
    }

    #region Sort Adapter Class
    public class SortAdapter : IComparer<Book>
    {
        private readonly Comparison<Book> _comparer;

        public SortAdapter(Comparison<Book> comparer)
        {
            _comparer = comparer;
        }

        public int Compare(Book x, Book y)
        {
            return _comparer(x, y);
        }
    }
    #endregion
}
