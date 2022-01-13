namespace VendaZap.Usuarios.Dominio
{
    internal class Mensagem
    {
        internal class Usuario
        {
            internal static readonly string NomeUsuarioObrigatorio = "Nome do usuário é obrigatório.";
            internal static readonly string SobrenomeUsuarioObrigatorio = "Sobrenome do usuário é obrigatório.";
            internal static readonly string EmailObrigatorio = "O e-mail do usuário é obrigatório.";
            internal static readonly string IdUsuarioLiderObrigatorio = "Identificador usuário lider é obrigatório.";
            internal static readonly string SenhaObrigatoria = "Senha é obrigatória.";
            internal static readonly string IdObrigatorio = "Identificador é obrigatório.";
            internal static readonly string NaoEnconrtrado = "Usuário não encontrado.";
            internal static readonly string EmailJaCadastradoPorOutroUsuario = "O e-mail informado já está sendo utilizado por outro usuário.";
        }

        internal class Chapa
        {
            internal static readonly string IdObrigatorio = "Identificador é obrigatório.";
            internal static readonly string NaoEnconrtrada = "Chapa não encontrada.";
        }

        internal class Telefone
        {
            internal static readonly string IdObrigatorio = "Identificador é obrigatório.";
            internal static readonly string DDDObrigatorio = "DDD é obrigatório.";
            internal static readonly string NumerObrigatorio = "Número é obrigatório.";
            internal static readonly string IdUsuarioObrigatorio = "Identificador usuário é obrigatório.";
            internal static readonly string UsuarioNãoPossuiNenhumTelefoneCadastrado = "Usuário não possui nenhum telefone cadastrado.";
            internal static readonly string NaoEnconrtrado = "Telefone não encontrado.";
        }

        internal class RealizarLogin
        {
            internal static readonly string EmailObrigatorio = "O e-mail é obrigatório.";
            internal static readonly string SenhaObrigatoria = "A senha é obrigatória.";
            internal static readonly string UsuarioOuSenhaInvalidos = "Usuário ou senha inválidos.";
        }

        internal class CadastrarUsuario
        {
            internal static readonly string UsuarioLiderNaoEncontrado = "Nenhum usuário lider encontrado.";
        }
    }
}
