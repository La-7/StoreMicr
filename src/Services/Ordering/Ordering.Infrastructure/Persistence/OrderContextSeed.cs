using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task Seed(StorageContext context, ILogger<OrderContextSeed> logger)
        {
            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(GetPredefinedOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Order data is successfully seeded to context");
            }
        }

        private static IEnumerable<Order> GetPredefinedOrders()
        {
            return new List<Order>
            {
                new() { UserName = "admin", FirstName = "Ruslan", LastName = "V", EmailAddress = "theyneedr@gmail.com", 
                    AddressLine = "Odesa", Country = "Ukraine", TotalPrice = 500, State = "MA", ZipCode = "12345",
                    CardName = "Name", CardNumber = "Number", Expiration = "Expiration", CVV = "CVV", PaymentMethod = 1
                }
            };
        }
    }
}
