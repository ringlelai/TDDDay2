using HSDc.OnlineOrderSystem.Control;
using HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;

namespace OnlineOrderSystem.Web.Controllers
{
    public class OrdersController : Controller
    {
        private PlaceOrderService service;
        private IPrincipal user;
        public IPrincipal CurrentUser
        {
            private get
            {
                if (User == null)
                    return user;
                return User;
            }
            set
            {
                this.user = value;
            }
        }

        public OrdersController()
        {
            service = new PlaceOrderService();
        }

        [Authorize]
        public ActionResult ProductSelection()
        {
            List<ProductStocking> model = service.RetrieveAllProducts(CurrentUser.Identity.Name);
            model.ForEach(p => p.UserHasBoughtStr = p.UserHasBought ? "是" : "否");
            return View(model);
        }

        [Authorize]
        public ActionResult QueryMemberOrder()
        {
            return View();
        }

        [Authorize]
        public ActionResult QueryMemberOrders()
        {
            QueryOrderService orderService = new QueryOrderService();
            var orders = orderService.QueryOrders(User.Identity.Name);
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult QueryOrderDetails(string OrderNumber)
        {
            QueryOrderService orderService = new QueryOrderService();
            var order = orderService.QueryOrderDetail(OrderNumber);
            return View(order);
        }
    }
}