namespace MatchingEngine
{
    public class Trade
    {
        public string TakerOrderId { get; set; }
        public string MakerOrderId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }

        public Trade(string takerOrderId, string makerOrderId, double amount, double price)
        {
            TakerOrderId = takerOrderId;
            MakerOrderId = makerOrderId;
            Amount = amount;
            Price = price;
        }
    }
}
