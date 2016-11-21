using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.LogicBook.Exceptions
{
    public class BookListServiceAddExistingItemException : Exception
    {
        public BookListServiceAddExistingItemException() : this("Attempt to add item that already exist.")
        {
        }

        public BookListServiceAddExistingItemException(string message) : base(message)
        {
        }

        public BookListServiceAddExistingItemException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class BookListServiceRemoveNotExistingItemException : Exception
    {
        public BookListServiceRemoveNotExistingItemException() : this("Attempt to remove item that not exist.")
        {

        }

        public BookListServiceRemoveNotExistingItemException(string message) : base(message)
        {
        }

        public BookListServiceRemoveNotExistingItemException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
