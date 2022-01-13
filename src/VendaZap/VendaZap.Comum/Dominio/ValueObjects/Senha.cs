using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Messages;

namespace VendaZap.Comum.Dominio.ValueObjects
{
    public class Senha : ValueObject
    {
        private const int saltSize = 16; // 128 bit 
        private const int keySize = 32; // 256 bit
        private const int iterations = 10000;
        private readonly string senha;

        public Senha(string senha)
        {
            this.senha = senha;
            this.Validar();
            this.senha = this.Criptografar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(this.senha))
            {
                throw new BusinessRuleException(Mensagem.SenhaInvalida);
            }
        }

        public override string ToString()
        {
            return this.senha;
        }

        private string Criptografar()
        {
            var parts = this.senha.Split('.', 3);
            if (parts.Length == 3 && parts[0] == iterations.ToString())
            {
                return this.senha;
            }

            using (var algorithm = new Rfc2898DeriveBytes(
                              this.senha,
                              saltSize,
                              iterations,
                              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{iterations}.{salt}.{key}";
            }
        }

        public bool ValidarSenha(string senhaValidar)
        {
            var parts = this.senha.Split('.', 3);
            if (parts.Length != 3)
            {
                throw new BusinessRuleException(Mensagem.SenhaInvalida);
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            //var needsUpgrade = iterations != iterations;
            using (var algorithm = new Rfc2898DeriveBytes(
              senhaValidar,
              salt,
              iterations,
              HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(keySize);

                var verified = keyToCheck.SequenceEqual(key);
                //return (verified, needsUpgrade);
                return verified;
            }
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.senha;
        }
    }
}
