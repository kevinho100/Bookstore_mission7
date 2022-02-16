using System;
using System.Linq;

namespace Bookstore.Models
{
    public interface IBooksRepository
    {
        IQueryable<Books> Books { get; }
    }
}
