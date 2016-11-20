using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public interface IBookStorage : IEnumerable<Book>
    {
        void Add(Book book);
        void Remove(Book book);
    }
}
