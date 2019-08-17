using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MatchingEngine.Enums;

namespace MatchingEngine.UnitTests
{
    [TestClass]
    public class ProcessorTests
    {
        [TestMethod]
        public void ProcessBuyLimit_PickBidOrder_FullFilled()
        {
            // Arrange
            var orderbook = new OrderBook();
            var bidOrder = new Order(OrderSide.Buy, 100, 1);

            // Act
            var processor = new Processor();
            var trades = processor.ProcessBuyLimit(bidOrder, orderbook);

            // Assert
            Assert.AreEqual(trades.Count, 0);

            Assert.AreEqual(orderbook.BidOrders[0].Id, bidOrder.Id);
            Assert.AreEqual(orderbook.BidOrders[0].Price, bidOrder.Price);
            Assert.AreEqual(orderbook.BidOrders[0].Amount, bidOrder.Amount);
            Assert.AreEqual(orderbook.BidOrders[0].Side, bidOrder.Side);
        }

        [TestMethod]
        public void ProcessBuyLimit_PickBidOrder_PartialFilled()
        {
            // Arrange
            var price = 100;
            var partialFilledAmount = 0.4;
            var fullAmount = 1;
            var askOrder = new Order(OrderSide.Sell, price, partialFilledAmount);
            var orderbook = new OrderBook();
            orderbook.AddAskOrder(askOrder);
            var bidOrder = new Order(OrderSide.Buy, price, fullAmount);

            // Act
            var processor = new Processor();
            var trades = processor.ProcessBuyLimit(bidOrder, orderbook);

            // Assert
            Assert.AreEqual(trades.Count, 1);
            Assert.AreEqual(trades[0].Price, bidOrder.Price);
            Assert.AreEqual(trades[0].Amount, partialFilledAmount);
            Assert.AreEqual(trades[0].TakerOrderId, bidOrder.Id);
            Assert.AreEqual(trades[0].MakerOrderId, askOrder.Id);

            Assert.AreEqual(orderbook.BidOrders.Count, 1);
            Assert.AreEqual(orderbook.BidOrders[0].Id, bidOrder.Id);
            Assert.AreEqual(orderbook.BidOrders[0].Price, bidOrder.Price);
            Assert.AreEqual(orderbook.BidOrders[0].Amount, fullAmount - partialFilledAmount);
            Assert.AreEqual(orderbook.BidOrders[0].Side, bidOrder.Side);
        }

        [TestMethod]
        public void ProcessSellLimit_PickAskOrder_FullFilled()
        {
            // Arrange
            var orderbook = new OrderBook();
            var askOrder = new Order(OrderSide.Sell, 100, 1);

            // Act
            var processor = new Processor();
            var trades = processor.ProcessSellLimit(askOrder, orderbook);

            // Assert
            Assert.AreEqual(trades.Count, 0);

            Assert.AreEqual(orderbook.AskOrders[0].Id, askOrder.Id);
            Assert.AreEqual(orderbook.AskOrders[0].Price, askOrder.Price);
            Assert.AreEqual(orderbook.AskOrders[0].Amount, askOrder.Amount);
            Assert.AreEqual(orderbook.AskOrders[0].Side, askOrder.Side);
        }
    }
}
