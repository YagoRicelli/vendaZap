using System.Text.RegularExpressions;

namespace VendaZap.Comum.Dominio.Utils
{
    public class EmailUtil
    {
        public static bool IsValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public static bool IsInvalid(string email)
        {
            return string.IsNullOrEmpty(email) ? false : !IsValid(email);
        }
    }
}
