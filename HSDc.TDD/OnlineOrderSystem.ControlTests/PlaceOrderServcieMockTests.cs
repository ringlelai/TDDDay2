using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HSDc.OnlineOrderSystem.Control;
using HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder;
using System.Collections.Generic;
using Moq;
using HSDc.OnlineOrderSystem.Control.Dao;

namespace OnlineOrderSystem.ControlTests
{
    [TestClass()]
    public class PlaceOrderServiceMockTests
    {
        private PlaceOrderService service;

        [TestInitialize]
        public void SetUp()
        {
            service = new PlaceOrderService();
            var PlaceOrderDaoMock = new Mock<IPlaceOrderDao>();
            PlaceOrderDaoMock.Setup(pom => pom.RetrieveAllProductStocking()).Returns(new List<ProductStocking>()
                {
                    new ProductStocking { ProductID = "BOOK0001", ProductName = "UML團隊流程開發與管理", ProductQuantity = 1 },
                    new ProductStocking { ProductID = "BOOK0002", ProductName = "數讀5", ProductQuantity = 10 },
                    new ProductStocking { ProductID = "BOOK0003", ProductName = "罪與罰", ProductQuantity = 2 }
                });
            PlaceOrderDaoMock.Setup(pom => pom.GetUserBoughtQuantity(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((pid, uname) => ProcessBoughtQuantity(pid, uname));
            service.Dao = PlaceOrderDaoMock.Object;
        }

        private long ProcessBoughtQuantity(string pid, string uname)
        {
            if (uname == "ringle.lai@gmail.com")
            {
                if (pid == "BOOK0001")
                    return 3;
                if (pid == "BOOK0002")
                    return 1;
                return 0;
            }
            return 0;
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
}
