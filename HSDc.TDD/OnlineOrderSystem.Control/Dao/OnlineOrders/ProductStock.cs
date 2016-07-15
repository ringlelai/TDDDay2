namespace HSDc.OnlineOrderSystem.Control.Dao
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductStock")]
    public partial class ProductStock
    {
        [Key]
        public int ProductStockId { get; set; }

        public int ProductId { get; set; }

        public int CurrentStockQty { get; set; }

        public virtual Product Product { get; set; }
    }
}
