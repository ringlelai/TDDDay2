using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder;
using HSDc.OnlineOrderSystem.Control.Dao;
using System;
using System.Linq;

namespace HSDc.OnlineOrderSystem.Control.Tests
{
    [TestClass()]
    public class PlaceOrderServiceTests
    {
        private PlaceOrderService service;

        [TestInitialize]
        public void SetUp()
        {
            service = new PlaceOrderService();
            service.Dao = new PlaceOrderDaoStub();
        }
        [TestMethod()]
        public void RetrieveAllProductsTest()
        {
            string input = "ringle.lai@gmail.com";
            List<ProductStocking> expectedResult = BuildRetrieveAllProductsExpected();
            var realResult = service.RetrieveAllProducts(input);
            CollectionAssert.AreEqual(expectedResult, realResult);
        }

        private List<ProductStocking> BuildRetrieveAllProductsExpected()
        {
            List<ProductStocking> products = new List<ProductStocking>();
            products.Add(new ProductStocking { ProductID = "BOOK0001", ProductName = "UML團隊流程開發與管理", UserHasBought = true, ProductQuantity = 1 });
            products.Add(new ProductStocking { ProductID = "BOOK0002", ProductName = "數讀5", UserHasBought = true, ProductQuantity = 10 });
            products.Add(new ProductStocking { ProductID = "BOOK0003", ProductName = "罪與罰", UserHasBought = false, ProductQuantity = 2 });
            return products;
        }
    }

    public class PlaceOrderDaoStub : IPlaceOrderDao
    {
        private static List<UserBought> UserBoughts = new List<UserBought>()
        {
            new UserBought { UserName = "ringle.lai@gmail.com", HasBought = 3, ProductId = "BOOK0001" },
            new UserBought { UserName = "ringle.lai@gmail.com", HasBought = 1, ProductId = "BOOK0002" },
            new UserBought { UserName = "ringle.lai@gmail.com", HasBought = 0, ProductId = "BOOK0003" }
        };

        private static List<ProductStocking> Products = new List<ProductStocking>()
        {
            new ProductStocking { ProductID = "BOOK0001", ProductName = "UML團隊流程開發與管理", ProductQuantity = 1 },
            new ProductStocking { ProductID = "BOOK0002", ProductName = "數讀5", ProductQuantity = 10 },
            new ProductStocking { ProductID = "BOOK0003", ProductName = "罪與罰", ProductQuantity = 2 }
        };

        long IPlaceOrderDao.GetUserBoughtQuantity(string ProductID, string UserName)
        {
            return UserBoughts.Where(ub => ub.UserName == UserName && ub.ProductId == ProductID).Select(ub => ub.HasBought).SingleOrDefault();
        }

        List<ProductStocking> IPlaceOrderDao.RetrieveAllProductStocking()
        {
            return Products;
        }
    }

    internal class UserBought
    {
        internal string UserName { get; set; }
        internal string ProductId { get; set; }
        internal long HasBought { get; set; }
    }
}