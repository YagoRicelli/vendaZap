using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VendaZap.Usuarios.Dados;

namespace VendaZap.Usuarios.Infra
{
    public class Banco
    {
        public static void Registrar(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UsuariosContext>(option => option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
