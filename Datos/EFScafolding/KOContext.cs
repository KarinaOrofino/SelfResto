using KO.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace KO.Data.EFScafolding
{
    public class KOContext : DbContext
    {

        public KOContext(DbContextOptions<KOContext> options) : base(options)
        {

        }

        public DbSet<AccessType> AccessTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailStatus> OrderDetailStatus { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.RemoveCascadeDeleteForAll(modelBuilder);
        }

        private void RemoveCascadeDeleteForAll(ModelBuilder builder)
        {
            var cascadeFKs = builder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetForeignKeys())
                            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
