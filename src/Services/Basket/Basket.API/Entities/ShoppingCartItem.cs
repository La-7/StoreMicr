namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
    }
}
