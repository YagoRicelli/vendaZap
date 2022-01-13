using Microsoft.EntityFrameworkCore;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dados.Mapeamento
{
    internal class ChapaMapping
    {
        internal static void Map(ModelBuilder builder)
        {
            builder.Entity<Chapa>(tbl =>
            {
                // Table
                tbl.ToTable("Chapa");

                // Keys
                tbl.HasKey(x => x.Id);

                // Properties
                tbl.Property(x => x.Id).HasMaxLength(36);
                tbl.Property(x => x.Nome).IsRequired().HasMaxLength(100);
                tbl.Property(x => x.Ativo).IsRequired();
                tbl.Property(x => x.DataIncusao).IsRequired();

                // Relationship
                tbl.HasMany(x => x.Usuarios).WithOne(x => x.Chapa).HasForeignKey(x => x.IdChapa);
            });
        }
    }
}
