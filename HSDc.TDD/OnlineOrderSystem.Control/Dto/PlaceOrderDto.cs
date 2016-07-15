using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDc.OnlineOrderSystem.Control.Dto.PlaceOrder
{
    public class ProductStocking
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public bool UserHasBought { get; set; }
        public string UserHasBoughtStr { get; set; }
        public int ProductQuantity { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var objProduct = (ProductStocking)obj;

            return objProduct.ProductID.Equals(ProductID) &&
                objProduct.ProductName.Equals(ProductName) &&
                objProduct.UserHasBought.Equals(UserHasBought) &&
                objProduct.ProductQuantity.Equals(ProductQuantity);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                var strHashCode = !string.IsNullOrEmpty(ProductID) ? ProductID.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ strHashCode;
                strHashCode = !string.IsNullOrEmpty(ProductName) ? ProductName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ strHashCode;
                hashCode = (hashCode * 397) ^ UserHasBought.GetHashCode();
                hashCode = (hashCode * 397) ^ ProductQuantity.GetHashCode();
                return hashCode;
            }
        }
    }
}
