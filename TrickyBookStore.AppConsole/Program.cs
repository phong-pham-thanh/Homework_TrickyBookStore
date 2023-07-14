using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;

namespace TrickyBookStore.AppConsole
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Unity.AddServices();

            var _customerService = Unity.collections
                .BuildServiceProvider()
                .GetService<ICustomerService>();
            var _paymentService = Unity.collections
                .BuildServiceProvider()
                .GetService<IPaymentService>();

            while (true)
            {
                //Console.WriteLine("Enter id:");
                //int idCustomer = Int32.Parse(Console.ReadLine());

                //Console.WriteLine("Enter Month:");
                //int month = Int32.Parse(Console.ReadLine());

                //Console.WriteLine("Enter Year:");
                //int year = Int32.Parse(Console.ReadLine());
                int idCustomer = 5;
                int month = 3;
                int year = 2018;
                Console.WriteLine(_paymentService.GetPaymentAmountByMonth(idCustomer, month, year));
            }
        }
    }
}
