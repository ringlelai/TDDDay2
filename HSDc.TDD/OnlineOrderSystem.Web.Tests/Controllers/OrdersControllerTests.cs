using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Principal;
using System.Web.Mvc;
using System;

namespace OnlineOrderSystem.Web.Controllers.Tests
{
    [TestClass()]
    public class OrdersControllerTests
    {
        private OrdersController controller;

        [TestInitialize]
        public void SetUp()
        {
            controller = new OrdersController();
            controller.CurrentUser = new PrincipalStub(new IdentityStub("ringle.lai@gmail.com"));
        }

        [TestMethod()]
        public void ProductSelectionTest()
        {
            ViewResult result = controller.ProductSelection() as ViewResult;
            Assert.IsNotNull(result);
        }
    }

    public class PrincipalStub : IPrincipal
    {
        private IIdentity identity;
        public PrincipalStub(IdentityStub identity)
        {
            this.identity = identity;
        }
        IIdentity IPrincipal.Identity
        {
            get
            {
                return identity;
            }
        }

        bool IPrincipal.IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }

    public class IdentityStub : IIdentity
    {
        private string name;

        public IdentityStub(string name)
        {
            this.name = name;
        }

        string IIdentity.AuthenticationType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        bool IIdentity.IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        string IIdentity.Name
        {
            get
            {
                return this.name;
            }
        }
    }
}