using System.ComponentModel;

namespace VendaZap.Usuarios.Dominio.Enumeradores
{
    public enum ETipoUsuario
    {
        [Description("Vendedor")]
        Vendedor = 1,
        [Description("Lider")]
        Lider = 2,
        [Description("Administrativo")]
        Administrativo = 3
    }
}
