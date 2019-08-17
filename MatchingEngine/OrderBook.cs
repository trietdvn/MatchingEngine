using System.Collections.Generic;
using System.Linq;

namespace MatchingEngine
{
    public class OrderBook
    {
        List<Order> _bidOrders;
        List<Order> _askOrders;

        public List<Order> BidOrders {
            get {
                return _bidOrders.OrderByDescending(p => p.Price).ToList();
            }
        }

        public List<Order> AskOrders {
            get
            {
                return _askOrders.OrderBy(p => p.Price).ToList();
            }
        }

        public OrderBook() {
            _bidOrders = new List<Order>();
            _askOrders = new List<Order>();
        }

        public void AddBidOrder(Order order)
        {
            _bidOrders.Add(order);
        }

        public void AddAskOrder(Order order)
        {
            _askOrders.Add(order);
        }

        public void RemoveBidOrder(string orderId)
        {
            var removedOrder = _bidOrders.FirstOrDefault(p => p.Id == orderId);
            _bidOrders.Remove(removedOrder);
        }

        public void RemoveAskOrder(string orderId)
        {
            var removedOrder = _askOrders.FirstOrDefault(p => p.Id == orderId);
            _askOrders.Remove(removedOrder);
        }
    }
}