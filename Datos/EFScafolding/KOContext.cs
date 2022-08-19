using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace KO.Data.EFScafolding
{
    public class KOContext : DbContext
    {

        public KOContext(DbContextOptions<KOContext> options) : base(options)
        {

        }
    
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
