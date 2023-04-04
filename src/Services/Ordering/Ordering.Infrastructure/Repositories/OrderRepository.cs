using Microsoft.EntityFrameworkCore;
using Ordering.Application.Interfaces.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(StorageContext context) 
            : base(context) { }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            var orders = await _context.Orders.Where(x => x.UserName == userName).ToListAsync();
            return orders;
        }
    }
}
