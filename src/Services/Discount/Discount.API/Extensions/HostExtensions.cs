using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            var retryCount = retry ?? 0;
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILogger<TContext>>();
                var conf = service.GetRequiredService<IConfiguration>();

                try
                {
                    logger.LogInformation("Migrating postgresql database...");
                    using var connection =
                        new NpgsqlConnection(conf.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand { Connection = connection };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE coupon(Id SERIAL PRIMARY KEY,
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    command.ExecuteNonQuery();

                    command.CommandText =
                        "INSERT INTO coupon (ProductName, Description, Amount) VALUES ('Name 1', 'Name 1 Discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migration is completed");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the db");

                    if (retryCount < 5)
                    {
                        retryCount++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retry);
                    }
                }

                return host;
            }
        }
    }
}
