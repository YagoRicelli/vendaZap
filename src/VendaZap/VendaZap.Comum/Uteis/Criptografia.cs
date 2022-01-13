using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace VendaZap.Comum.Uteis
{
    public static class Criptografia
    {
        private const string chave = "123456";
        public static string Criptografar(string texto, bool encode = true)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string textoCriptografado = string.Empty;

            try
            {
                tripleDES.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(chave));
                tripleDES.Mode = CipherMode.ECB;
                ICryptoTransform encriptador = tripleDES.CreateEncryptor();
                byte[] bytesTexto = Encoding.ASCII.GetBytes(texto);
                textoCriptografado = HttpUtility.UrlEncode(Convert.ToBase64String(encriptador.TransformFinalBlock(bytesTexto, 0, bytesTexto.Length)), Encoding.UTF8);
            }
            finally
            {
                tripleDES = null;
                md5 = null;
            }

            return textoCriptografado;
        }

        public static string Decriptografar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return string.Empty;
            }

            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string textoDescriptografado = string.Empty;

            try
            {
                tripleDES.Key = md5.ComputeHash(Encoding.ASCII.GetBytes(chave));
                tripleDES.Mode = CipherMode.ECB;
                ICryptoTransform desencriptador = tripleDES.CreateDecryptor();
                byte[] bytesTexto = Convert.FromBase64String(HttpUtility.UrlDecode(texto, Encoding.UTF8));
                textoDescriptografado = HttpUtility.UrlDecode(Encoding.ASCII.GetString(desencriptador.TransformFinalBlock(bytesTexto, 0, bytesTexto.Length)), Encoding.UTF8);
            }
            finally
            {
                tripleDES = null;
                md5 = null;
            }

            return textoDescriptografado;
        }
    }
}
