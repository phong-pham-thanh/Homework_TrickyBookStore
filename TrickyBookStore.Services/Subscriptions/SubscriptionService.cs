using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using System.Linq;

namespace TrickyBookStore.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        public IList<Subscription> GetSubscriptions(List<int> ids)
        {
            return ids.Select(id => Store.Subscriptions.Data.Where(sub => sub.Id == id).FirstOrDefault()).ToList();
        }


    }
}
