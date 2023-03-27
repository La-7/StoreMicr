using Ordering.Domain.Entities;

namespace Ordering.Application.Interfaces.Persistence
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrderByUserName(string userName);
    }
}
