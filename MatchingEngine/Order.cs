using static MatchingEngine.Enums;

namespace MatchingEngine
{
    public class Order
    {
        public Order(OrderSide side, double price, double amount)
        {
            Id = System.Guid.NewGuid().ToString();
            Price = price;
            Amount = amount;
            Side = side;
        }

        public string Id { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public OrderSide Side { get; set; }
    }
}
