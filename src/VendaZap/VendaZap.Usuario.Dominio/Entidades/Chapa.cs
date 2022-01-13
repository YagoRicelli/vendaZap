using System;
using System.Collections.Generic;
using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.Entidades
{
    public class Chapa: DomainEntity
    {
        public string Id { get; protected set; }
        public string Nome { get; protected set; }
        public bool Ativo { get; protected set; }
        public DateTime DataIncusao { get; protected set; }
        public List<Usuario> Usuarios { get; protected set; }

        public Chapa()
        {

        }
        public Chapa(string nome)
        {
            this.Id = Guid.NewGuid().ToString();
            Nome = nome;
            Ativo = true;
            DataIncusao = DateTime.Now;
        }
    }
}
