using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;
namespace TrickyBookStore.AppConsole
{
    public static class Unity
    {
        
        public static IServiceCollection collections { get; set; } = new ServiceCollection();
        public static void AddServices()
        {
            collections = collections
                .AddSingleton<IBookService, BookService>()
                .AddSingleton<ICustomerService, CustomerService>()
                .AddSingleton<IPaymentService, PaymentService>()
                .AddSingleton<IPurchaseTransactionService, PurchaseTransactionService>()
                .AddSingleton<ISubscriptionService, SubscriptionService>();
        }
    }
}
