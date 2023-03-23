using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var hasAnyProduct = productCollection.Find(x => true).Any();
            if (!hasAnyProduct)
                productCollection.InsertManyAsync(GetPredefinedProducts());
        }

        private static IEnumerable<Product> GetPredefinedProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Name 1",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product1.png",
                    Price = 1000.00M,
                    Category = "Category 1"
                },
                new Product()
                {
                    Name = "Name 2",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product2.png",
                    Price = 1000.00M,
                    Category = "Category 2"
                },
                new Product()
                {
                    Name = "Name 3",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product3.png",
                    Price = 1000.00M,
                    Category = "Category 3"
                },
                new Product()
                {
                    Name = "Name 4",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product4.png",
                    Price = 1000.00M,
                    Category = "Category 4"
                },
                new Product()
                {
                    Name = "Name 5",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product5.png",
                    Price = 1000.00M,
                    Category = "Category 5"
                },
                new Product()
                {
                    Name = "Name 6",
                    Summary = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam sollicitudin auctor elit vitae convallis.",
                    ImageFile = "product6.png",
                    Price = 1000.00M,
                    Category = "Category 6"
                }
            };
        }
    }
}
