using Framework.Common;
using Microsoft.EntityFrameworkCore;
using KO.Entidades;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KO.Datos.EFScafolding
{
    public class KOContext : DbContext
    {

        public KOContext(DbContextOptions<KOContext> options) : base(options)
        {


        }
    
        //public DbSet<Usuario> Usuarios { get; set; }
        //public DbSet<Colada> Coladas { get; set; }
        //public DbSet<Via> Vias { get; set; }
        //public DbSet<TipoEquipo> TiposEquipo { get; set; }
        //public DbSet<Carga> Cargas { get; set; }
        //public DbSet<CargaDetalle> CargasDetalles { get; set; }
        //public DbSet<RecetaUtilizada> RecetaUtilizada { get; set; }
        //public DbSet<Funcionalidad> Funcionalidades { get; set; }
        //public DbSet<FuncionalidadRol> FuncionalidadesRol { get; set; }
        //public DbSet<Receta> Receta { get; set; }
        //public DbSet<CapasReceta> CapasReceta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.SetKeys(modelBuilder);
            this.RemoveCascadeDeleteForAll(modelBuilder);            
        }

        private void SetKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuncionalidadRol>(entity =>
            {
                entity.HasKey(e => new { e.IdFuncionalidad, e.IdRol });
            });

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
