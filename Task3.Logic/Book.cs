using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public class Book : IEquatable<Book>, IComparable<Book>, IComparable
    {
        public long ISBN { get; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Edition { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public string Genre { get; set; }

        public Book(long isbn)
        {
            ISBN = isbn;
        }
        

        public override string ToString()
        {
            return $"{Author}, {Name} {Edition.ToString()} edition, {Year}, {Publisher}";
        }

        public bool Equals(Book other)
        {
            if (this.ISBN == other.ISBN)
            {
                return true;
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Book bookObj = obj as Book;
            if (bookObj == null)
                return false;
            else
                return Equals(bookObj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(Book other)
        {
            if (this.ISBN > other.ISBN)
                return -1;
            else if (this.ISBN < other.ISBN)
                return 1;
            else
                return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            Book bookObj = obj as Book;
            if (bookObj == null)
                return 1;
            else
                return CompareTo(bookObj);
        }
    }
}
