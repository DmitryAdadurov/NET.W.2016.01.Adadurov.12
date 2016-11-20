using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public class BookListStorage : IBookStorage
    {
        private LinkedList<Book> _storage;

        public BookListStorage()
        {
            _storage = new LinkedList<Book>();
        }

        public BookListStorage(IEnumerable<Book> books)
        {
            _storage = new LinkedList<Book>();
            foreach (Book item in books)
            {
                BookListService.AddBook(this, item);
            }
        }

        public void Add(Book book)
        {
            _storage.AddLast(book);
        }

        public IEnumerator<Book> GetEnumerator()
        {
            LinkedListNode<Book> current = _storage.First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        public void Remove(Book book)
        {
            _storage.Remove(book);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
