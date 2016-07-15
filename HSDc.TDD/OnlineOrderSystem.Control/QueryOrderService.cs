using HSDc.OnlineOrderSystem.Control.Dao;
using HSDc.OnlineOrderSystem.Control.Dto.QueryOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDc.OnlineOrderSystem.Control
{
    public class QueryOrderService
    {
        public List<OrderInfo> QueryOrders(string MemberName)
        {
            QueryOrderDao dao = new QueryOrderDao();
            return dao.RetrieveOrders(MemberName);
        }

        public OrderDetail QueryOrderDetail(string OrderNumber)
        {
            QueryOrderDao dao = new QueryOrderDao();
            var order = dao.QueryOrder(OrderNumber);
            long total = 0;
            var orderItems = dao.QueryOrderItems(OrderNumber);
            orderItems.ForEach(oi => oi.SubTotal = oi.Quantity * oi.Price);
            orderItems.ForEach(oi => total += oi.SubTotal);
            order.TotalAmount = total;
            order.ExpectedDate = order.DeliveryDate.AddDays(2);
            order.Items = orderItems;
            return order;
        }
    }
}
