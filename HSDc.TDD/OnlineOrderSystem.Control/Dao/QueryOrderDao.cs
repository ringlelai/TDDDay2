using HSDc.OnlineOrderSystem.Control.Dto.QueryOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDc.OnlineOrderSystem.Control.Dao
{
    public class QueryOrderDao
    {
        public List<OrderInfo> RetrieveOrders(string MemberName)
        {
            OnlineOrderContext db = new OnlineOrderContext();
            var data = (from o in db.Order
                        join m in db.Member on o.MemberId equals m.MemberId
                        where m.UserName == MemberName
                        select new OrderInfo { OrderNumber = o.OrderId, OrderDate = o.OrderDate, DeliveryDate = o.DeliveryDate }).ToList();
            db.Dispose();
            return data;
        }

        public OrderDetail QueryOrder(string OrderNumber)
        {
            OnlineOrderContext db = new OnlineOrderContext();
            var data = (from o in db.Order
                        join m in db.Member on o.MemberId equals m.MemberId
                        where o.OrderId == OrderNumber
                        select new OrderDetail { OrderNumber = o.OrderId, OrderDate = o.OrderDate, DeliveryDate = o.DeliveryDate, MemberName = m.MemberName }).SingleOrDefault();
            db.Dispose();
            return data;
        }

        public List<HSDc.OnlineOrderSystem.Control.Dto.QueryOrder.OrderItem> QueryOrderItems(string OrderNumber)
        {
            OnlineOrderContext db = new OnlineOrderContext();
            var data = (from oi in db.OrderItem
                        join p in db.Product on oi.ProductId equals p.ProductId
                        where oi.OrderId == OrderNumber
                        select new HSDc.OnlineOrderSystem.Control.Dto.QueryOrder.OrderItem { BookName = p.ProductName, Quantity = oi.Quantity, Price = oi.Price }).ToList();
            db.Dispose();
            return data;
        }
    }
}
