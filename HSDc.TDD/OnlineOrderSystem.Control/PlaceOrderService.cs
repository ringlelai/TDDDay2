using HSDc.OnlineOrderSystem.Control.Dao;
using HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder;
using System.Collections.Generic;

namespace HSDc.OnlineOrderSystem.Control
{
    public class PlaceOrderService
    {
        private IPlaceOrderDao dao;

        public IPlaceOrderDao Dao
        {
            private get {
                if (dao == null)
                    dao = new PlaceOrderDao();
                return dao; }
            set { this.dao = value; }
        }

        public List<ProductStocking> RetrieveAllProducts(string userName)
        {
            var products = Dao.RetrieveAllProductStocking();
            products.ForEach(p => p = ProcessUserBoughtProduct(p, userName));
            return products;
        }

        private ProductStocking ProcessUserBoughtProduct(ProductStocking product, string userName)
        {
            var quantity = Dao.GetUserBoughtQuantity(product.ProductID, userName);
            product.UserHasBought = (quantity > 0);
            return product;
        }
    }
}
