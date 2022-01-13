using Microsoft.EntityFrameworkCore;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dados.Mapeamento
{
    internal class UsuarioMapping
    {
        internal static void Map(ModelBuilder builder)
        {
            builder.Entity<Usuario>(tbl =>
            {
                // Table
                tbl.ToTable("Usuario");

                // Keys
                tbl.HasKey(x => x.Id);

                // Properties
                tbl.Property(x => x.Id).HasMaxLength(36);
                tbl.Property(x => x.Nome).IsRequired().HasMaxLength(100);
                tbl.Property(x => x.Sobrenome).IsRequired().HasMaxLength(100);
                tbl.Property(x => x.Ativo).IsRequired();
                tbl.Property(x => x.DataIncusao).IsRequired();
                tbl.Property(x => x.Email).HasConversion(x => x.ToString(), x => new Email(x)).IsRequired().HasMaxLength(100);
                tbl.Property(x => x.IdUsuarioLider).HasMaxLength(36);
                tbl.Property(x => x.IdChapa).HasMaxLength(36).IsRequired();
                tbl.Property(x => x.Senha).HasConversion(x => x.ToString(), x => new Senha(x)).IsRequired().HasMaxLength(200);
                tbl.Property(x => x.Tipo).IsRequired().HasConversion<int>(); ;

                // Relationship
                tbl.HasMany(x => x.Telefones).WithOne(x => x.Usuario).HasForeignKey(x => x.IdUsuario);
                tbl.HasOne(x => x.Chapa).WithMany(x => x.Usuarios).HasForeignKey(x => x.IdChapa);
            });
        }
    }
}
