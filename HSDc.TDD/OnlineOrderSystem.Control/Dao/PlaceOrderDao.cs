using HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder;
using System.Collections.Generic;
using System.Linq;

namespace HSDc.OnlineOrderSystem.Control.Dao
{
    public interface IPlaceOrderDao
    {
        List<ProductStocking> RetrieveAllProductStocking();
        long GetUserBoughtQuantity(string ProductID, string UserName);
    }

    public class PlaceOrderDao : IPlaceOrderDao
    {
        public List<ProductStocking> RetrieveAllProductStocking()
        {
            OnlineOrderContext db = new OnlineOrderContext();
            var data = (from ps in db.ProductStock
                        join p in db.Product on ps.ProductId equals p.ProductId
                        select new ProductStocking { ProductID = p.ProductOfficeId, ProductName = p.ProductName, ProductQuantity = ps.CurrentStockQty }).ToList();
            db.Dispose();
            return data;
        }

        public long GetUserBoughtQuantity(string ProductID, string UserName)
        {
            OnlineOrderContext db = new OnlineOrderContext();
            var quantityList = (from oi in db.OrderItem
                            join o in db.Order on oi.OrderId equals o.OrderId
                            join p in db.Product on oi.ProductId equals p.ProductId
                            join m in db.Member on o.MemberId equals m.MemberId
                            where p.ProductOfficeId == ProductID &&
                                  m.UserName == UserName
                            select oi.Quantity).ToList();
            if ((quantityList == null) || (quantityList.Count == 0))
                return 0;
            long q = 0;
            quantityList.ForEach(quan => q += quan);
            return q;
        }
    }
}
