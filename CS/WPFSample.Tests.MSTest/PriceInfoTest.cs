using AssetPriceModule.Common;
using AssetPriceModule.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace WPFSample.Tests.MSTest
{
    [TestClass]
    public class PriceInfoTest
    {
        private IPriceInfoViewModel priceInfo;
        [TestInitialize]
        public void CreatePriceInfo()
        {
            var repo = new MockRepository();
            priceInfo = repo.CreateMock<PriceInfoViewModel>();
            priceInfo.AssetName = "Oil";
            priceInfo.Price = (decimal)12.5;
            priceInfo.Price = (decimal)100.5;
            priceInfo.Price = (decimal)77.5;
            priceInfo.Price = (decimal)103.5;
            priceInfo.Price = (decimal)10.5;
        }


        [TestMethod]
        public void  TestQueueLength()
        {
            //SY:  Test case for AVG price..
            // few more test cases can add Check latest price.
           // Price Momentum


            Assert.AreEqual(priceInfo.PriceHistory.Count,5);
        }


        [TestMethod]
        public void TestCurrentPrice()
        {
           
           Assert.AreEqual(priceInfo.Price, (decimal)10.5);
        }

        [TestMethod]
        public void TestPriceMomentum()
        {

            Assert.AreEqual(priceInfo.PriceMomentum, Momentum.Down);
        }

        [TestMethod]
        public void TestAvgPriceMomentum()
        {

            Assert.AreEqual(priceInfo.AvgPriceMomentum, Momentum.Down);
        }


        [TestMethod]
        public void TestAvgPrice()
        {
           
            Assert.AreEqual(priceInfo.AvgPrice, (decimal)60.9);
        }
    }
}
