using System;

// KeepIt
namespace TrickyBookStore.Services.Payment
{
    public interface IPaymentService
    {
        double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);
        double GetPaymentAmountByMonth(long customerId, int month, int year);
    }
}
