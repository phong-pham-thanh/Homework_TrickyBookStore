using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using System.Linq;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        public IList<Book> GetBooks(List<long> ids)
        {
            return ids.Select(id => this.GetBook(id)).ToList();
        }

        public Book GetBook(long id)
        {
            return Store.Books.Data.ToList().Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
