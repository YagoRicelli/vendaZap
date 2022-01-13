using Microsoft.EntityFrameworkCore;
using VendaZap.Comum.Dominio;
using VendaZap.Comum.Dominio.Messages;
using VendaZap.Usuarios.Dados.Mapeamento;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dados
{
    public class UsuariosContext : DbContext
    {
        private string connectionString = "server=localhost;user id=usuaplic;password=qnaldp$ndu9;persistsecurityinfo=True;port=3306;database=vendazap_usuarios";

        #region Construtores
        public UsuariosContext()
        {
        }

        public UsuariosContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UsuariosContext(DbContextOptions<UsuariosContext> options)
            : base(options)
        {
        }

        #endregion

        internal DbSet<Telefone> Telefones { get; set; }
        internal DbSet<Chapa> Chapas { get; set; }
        internal DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<DomainEntity>();
            builder.Ignore<DomainBaseItem>();
            builder.Ignore<Notification>();

            ChapaMapping.Map(builder);
            TelefoneMapping.Map(builder);
            UsuarioMapping.Map(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(this.connectionString, ServerVersion.AutoDetect(this.connectionString));
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
