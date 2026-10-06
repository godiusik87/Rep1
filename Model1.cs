using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DetMir
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Entities")
        {
        }

        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<OrderComposition> OrderCompositions { get; set; }
        public virtual DbSet<Placement> Placements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacture>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Manufacture)
                .HasForeignKey(e => e.Manufacture2);

            modelBuilder.Entity<Order>()
                .HasOptional(e => e.OrderComposition)
                .WithRequired(e => e.Order);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Point)
                .HasForeignKey(e => e.ID_item);

            modelBuilder.Entity<Product>()
                .Property(e => e.Unit)
                .IsFixedLength();

            modelBuilder.Entity<Provider>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Provider)
                .HasForeignKey(e => e.Provide2);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.role1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.ID_client);

            modelBuilder.Entity<Warehouse>()
                .HasMany(e => e.Placements)
                .WithRequired(e => e.Warehouse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderComposition>()
                .Property(e => e.Article2)
                .IsFixedLength();

            modelBuilder.Entity<Placement>()
                .Property(e => e.Article1)
                .IsFixedLength();
        }
    }
}
