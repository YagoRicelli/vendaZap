using Microsoft.EntityFrameworkCore;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dados.Mapeamento
{
    internal class TelefoneMapping
    {
        internal static void Map(ModelBuilder builder)
        {
            builder.Entity<Telefone>(tbl =>
            {
                // Table
                tbl.ToTable("Telefone");

                // Keys
                tbl.HasKey(x => x.Id);

                // Properties
                tbl.Property(x => x.Id).HasMaxLength(36);
                tbl.Property(x => x.DDD).IsRequired();
                tbl.Property(x => x.Numero).IsRequired();
                tbl.Property(x => x.IdUsuario).IsRequired();

                // Relationship
                tbl.HasOne(x => x.Usuario).WithMany(x => x.Telefones).HasForeignKey(x => x.IdUsuario);
            });
        }
    }
}
