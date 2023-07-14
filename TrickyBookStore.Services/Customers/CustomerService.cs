using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;
using System.Linq;

namespace TrickyBookStore.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        ISubscriptionService SubscriptionService { get; }

        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }

        public Customer GetCustomerById(long id)
        {
            List<Customer> lsCustomer = Store.Customers.Data.ToList();
            return lsCustomer.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Subscription> GetSubscriptionCustomer(long idCustomer)
        {
            Customer customer = this.GetCustomerById(idCustomer);
            return SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToList()).ToList();
        }

    }
}
