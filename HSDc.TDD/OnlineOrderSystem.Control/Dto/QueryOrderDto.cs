using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDc.OnlineOrderSystem.Control.Dto.QueryOrder
{
    public class OrderInfo
    {
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class OrderDetail : OrderInfo
    {
        public DateTime ExpectedDate { get; set; }
        public string MemberName { get; set; }
        public long TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public string BookName { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public long SubTotal { get; set; }
    }
}
