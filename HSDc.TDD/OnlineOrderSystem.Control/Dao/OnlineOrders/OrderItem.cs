using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSDc.OnlineOrderSystem.Control.Dao
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string OrderId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }

        [Required]
        public long Quantity { get; set; }

        [Required]
        public long Price { get; set; }
    }
}
