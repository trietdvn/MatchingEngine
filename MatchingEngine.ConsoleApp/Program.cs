using System;
using System.Collections.Generic;

namespace MatchingEngine.ConsoleApp
{
    class Program
    {
        static DateTime startTime;

        static void Main(string[] args)
        {
            startTime = DateTime.Now;

            var orderBook = new OrderBook();

            // simulate 1000 transactions
            RandomizeOrder(orderBook);

            Console.ReadLine();
        }

        static void RandomizeOrder(OrderBook orderBook)
        {
            var processor = new Processor();
            var rnd = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var orderSide = rnd.Next(2) == 0 ? Enums.OrderSide.Sell : Enums.OrderSide.Buy;
                var price = rnd.Next(100, 103);
                var amount = rnd.Next(1, 10);
                var order = new Order(orderSide, price, amount);

                var trades = new List<Trade>();
                if (orderSide == Enums.OrderSide.Buy)
                    trades = processor.ProcessBuyLimit(order, orderBook);
                else if (orderSide == Enums.OrderSide.Sell)
                    trades = processor.ProcessSellLimit(order, orderBook);

                Console.WriteLine(string.Format("Side: {0}\t OrderId: {1}\t Price: {2}\t Amount: {3}\t",
                     orderSide.ToString(), order.Id, order.Price, order.Amount));
                Console.WriteLine(string.Format("Execute time: {0} ms, OrderBookCount: {1}\t TradeCount: {2}",
                    (DateTime.Now - startTime).TotalMilliseconds, orderBook.BidOrders.Count + orderBook.AskOrders.Count, trades.Count));
            }
        }
    }
}
