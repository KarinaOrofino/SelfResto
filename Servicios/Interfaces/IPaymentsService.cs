
using KO.Entities;

namespace KO.Services.Interfaces
{
    public interface IPaymentsService : IBaseService
    {
        public void RequestBill(Order order, Payment payment);

    }
}

