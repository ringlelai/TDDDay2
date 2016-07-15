namespace HSDc.OnlineOrderSystem.Control.Dao
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineOrderContext : DbContext
    {
        public OnlineOrderContext()
            : base("name=OnlineOrderContext")
        {
        }

        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductStock> ProductStock { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductStock)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
