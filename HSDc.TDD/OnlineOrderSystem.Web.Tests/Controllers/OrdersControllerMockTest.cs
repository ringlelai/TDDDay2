using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineOrderSystem.Web.Controllers;
using System.Web.Mvc;
using Moq;
using System.Security.Principal;

namespace OnlineOrderSystem.Web.Tests.Controllers
{
    /// <summary>
    /// OrdersControllerMockTest 的摘要描述
    /// </summary>
    [TestClass]
    public class OrdersControllerMockTest
    {

        private OrdersController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new OrdersController();
            var mockIdentity = new Mock<IIdentity>();
            mockIdentity.Setup(i => i.Name).Returns("ringle.lai@gmail.com");
            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(i => i.Identity).Returns(mockIdentity.Object);
            controller.CurrentUser = mockPrincipal.Object;
        }

        [TestMethod()]
        public void ProductSelectionTest()
        {
            ViewResult result = controller.ProductSelection() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
