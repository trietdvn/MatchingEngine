using System.Collections.Generic;
using System.Linq;

namespace MatchingEngine
{
    public class Processor
    {
        public List<Trade> ProcessBuyLimit(Order order, OrderBook orderBook)
        {
            var trades = new List<Trade>();

            if (order.Side != Enums.OrderSide.Buy)
            {
                return trades;
            }

            var matchingAskOrders = orderBook.AskOrders.Where(p => p.Price == order.Price).ToList();
            foreach(var askOrder in matchingAskOrders)
            {
                // fill the entire order
                if (askOrder.Amount >= order.Amount)
                {
                    trades.Add(new Trade(order.Id, askOrder.Id, order.Amount, askOrder.Price));
                    askOrder.Amount -= order.Amount;
                    if (askOrder.Amount == 0)
                    {
                        orderBook.RemoveAskOrder(askOrder.Id);
                    }
                    return trades;
                }

                // fill a partial order and continue
                if (askOrder.Amount < order.Amount)
                {
                    trades.Add(new Trade(order.Id, askOrder.Id, askOrder.Amount, askOrder.Price));
                    order.Amount -= askOrder.Amount;
                    orderBook.RemoveAskOrder(askOrder.Id);
                }
            }

            // finally add the remaining order to the list
            orderBook.AddBidOrder(order);
            return trades;
        }

        public List<Trade> ProcessSellLimit(Order order, OrderBook orderBook)
        {
            var trades = new List<Trade>();

            if (order.Side != Enums.OrderSide.Sell)
            {
                return trades;
            }

            var matchingBidOrders = orderBook.BidOrders.Where(p => p.Price == order.Price).ToList();
            foreach (var bidOrder in matchingBidOrders)
            {
                // fill the entire order
                if (bidOrder.Amount >= order.Amount)
                {
                    trades.Add(new Trade(order.Id, bidOrder.Id, order.Amount, bidOrder.Price));
                    bidOrder.Amount -= order.Amount;
                    if (bidOrder.Amount == 0)
                    {
                        orderBook.RemoveBidOrder(bidOrder.Id);
                    }
                    return trades;
                }

                // fill a partial order and continue
                if (bidOrder.Amount < order.Amount)
                {
                    trades.Add(new Trade(order.Id, bidOrder.Id, bidOrder.Amount, bidOrder.Price));
                    order.Amount -= bidOrder.Amount;
                    orderBook.RemoveBidOrder(bidOrder.Id);
                }
            }

            // finally add the remaining order to the list
            orderBook.AddAskOrder(order);
            return trades;
        }
    }
}
