﻿using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                {
                   { "FixPrice", 50 }
                }
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 0 }
                }
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 0,
                PriceDetails = new Dictionary<string, double>
                {
                   { "FixPrice", 200 }
                }
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                  { "FixPrice", 75 }
                }              
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted,Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 75 },
                    //....
                },
                BookCategoryId = 1
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                    { "FixPrice", 75 },
                    //....
                },
                BookCategoryId = 3
            }
        };
    }
}
