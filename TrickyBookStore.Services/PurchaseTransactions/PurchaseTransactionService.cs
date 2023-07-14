using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using System.Linq;
namespace TrickyBookStore.Services.PurchaseTransactions
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        IBookService BookService { get; }

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }


        public IList<PurchaseTransaction> GetPurchaseTransactionsByMonth(long customerId, int month, int year)
        {
            List<PurchaseTransaction> allTrans = TrickyBookStore.Services.Store.PurchaseTransactions.Data.ToList();
            allTrans = allTrans.Where(tr => tr.CustomerId == customerId 
                                    && tr.CreatedDate.Month == month
                                    && tr.CreatedDate.Year == year).ToList();

            foreach(PurchaseTransaction item in allTrans)
            {
                item.Book = BookService.GetBook(item.BookId);
            }
            return allTrans;
        }
    }
}
