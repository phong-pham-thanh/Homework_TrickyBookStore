using System;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services;
using System.Collections.Generic;

namespace TrickyBookStore.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        ICustomerService CustomerService { get; }
        IPurchaseTransactionService PurchaseTransactionService { get; }

        public PaymentService(ICustomerService customerService, 
            IPurchaseTransactionService purchaseTransactionService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            throw new NotImplementedException();
        }

        public double GetPaymentAmountByMonth(long customerId, int month, int year)
        {
            List<Subscription> subCustomer = CustomerService.GetSubscriptionCustomer(customerId);
            Subscription highestSub = subCustomer.OrderBy(sub => sub.Priority).FirstOrDefault();
            double amountSub = 0;
            foreach(Subscription item in subCustomer)
            {
                amountSub += item.PriceDetails["FixPrice"];
            }

            List<PurchaseTransaction> allTrans = PurchaseTransactionService.GetPurchaseTransactionsByMonth(customerId, month, year).ToList();

            if (highestSub.Priority != 1)
                return amountSub + GetPaymentTransNotAddicted(allTrans, highestSub);
            else
                return amountSub + GetPaymentTransAddicted(allTrans, subCustomer);
        }

        private double GetPaymentTransNotAddicted(List<PurchaseTransaction> allTrans, Subscription highestSub)
        {
            List<PurchaseTransaction> transOldBook = allTrans.Where(tr => tr.Book.IsOld).ToList();
            List<PurchaseTransaction> transNewBook = allTrans.Where(tr => !tr.Book.IsOld).OrderBy(tr => tr.CreatedDate).ToList();
            int chargeRead = 0;
            int discountBuy = 0;
            double amountRead = 0;
            double amountBuy = 0;
            double limitDiscount = 3;

            if(highestSub == null)
            {
                foreach (PurchaseTransaction item in allTrans)
                {
                    amountRead += item.Book.Price;
                }
            }
            else
            {
                switch (highestSub.Priority)
                {
                    case 0:
                        chargeRead = 0;
                        discountBuy = 15;
                        break;
                    case 2:
                        chargeRead = 5;
                        discountBuy = 5;
                        break;
                    case 3:
                        chargeRead = 10;
                        discountBuy = 0;
                        break;
                }


                foreach (PurchaseTransaction itemOld in transOldBook)
                {
                    amountRead += (itemOld.Book.Price * (100 - chargeRead)) / 100;
                }
                int count = 0;
                foreach (PurchaseTransaction itemNew in transNewBook)
                {
                    if (count < limitDiscount)
                    {
                        amountBuy += (itemNew.Book.Price * (100 - discountBuy)) / 100;
                    }
                    else
                    {
                        amountBuy += itemNew.Book.Price;
                    }
                    count++;
                }
            }

            
            return amountRead + amountBuy;
        }
    
        private double GetPaymentTransAddicted(List<PurchaseTransaction> allTrans, List<Subscription> subCustomer)
        {
            List<int> lsIdCategorySub = subCustomer.Select(sub => sub.BookCategoryId)
                                       .ToList()
                                       .ConvertAll(id => id ?? -1);

            List<PurchaseTransaction> transInCategory = allTrans.FindAll(tr => lsIdCategorySub.Contains(tr.Book.CategoryId));
            List<PurchaseTransaction> transNewBook = transInCategory.Where(tr => !tr.Book.IsOld).OrderBy(tr => tr.CreatedDate).ToList();
            List<PurchaseTransaction> transOldBook = transInCategory.Where(tr => tr.Book.IsOld).OrderBy(tr => tr.CreatedDate).ToList();
            int discountBuy = 15;
            double amountBuy = 0;
            int limitDiscount = 3;
            int count = 0;
            foreach (PurchaseTransaction itemNew in transNewBook)
            {
                if (count < limitDiscount)
                {
                    amountBuy += (itemNew.Book.Price*(100-discountBuy))/100;
                }
                else
                {
                    amountBuy += itemNew.Book.Price;
                }
                count++;
            }
            foreach (PurchaseTransaction itemNew in transOldBook)
            {
                amountBuy += itemNew.Book.Price;
            }


            List<PurchaseTransaction> transNotInCategory = allTrans.FindAll(tr => !lsIdCategorySub.Contains(tr.Book.CategoryId));
            double resultNotInCategory = this.GetPaymentTransNotAddicted(transNotInCategory, subCustomer.FindAll(sub => sub.Priority > 1).OrderBy(sub => sub.Priority).FirstOrDefault());
            return amountBuy + resultNotInCategory;
        }
    }
}
