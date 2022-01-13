using System;
using System.Collections.Generic;
using VendaZap.Comum.Dominio;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.Entidades
{
    public class Usuario : DomainEntity
    {
        public string Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Sobrenome { get; protected set; }
        public Senha Senha { get; protected set; }
        public bool Ativo { get; protected set; }
        public DateTime DataIncusao { get; protected set; }
        public Email Email { get; protected set; }
        public string IdUsuarioLider { get; protected set; }
        public ETipoUsuario Tipo { get; protected set; }
        public string IdChapa { get; protected set; }
        public Chapa Chapa { get; protected set; }
        public List<Telefone> Telefones { get; protected set; }

        public Usuario()
        {

        }

        public Usuario(string nome, string sobrenome, string email, string idUsuarioLider, Senha senha, string idChapa, ETipoUsuario tipo)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Ativo = true;
            this.DataIncusao = DateTime.Now;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Email = new Email(email);
            this.Senha = senha;
            this.IdUsuarioLider = idUsuarioLider;
            this.Tipo = tipo;
            this.IdChapa = idChapa;

            this.Validar();
        }

        public void Inativar()
        {
            this.Ativo = false;
        }

        public void RemoverLider()
        {
            this.IdUsuarioLider = string.Empty;
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(this.Nome.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.NomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Sobrenome.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.SobrenomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Email.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.EmailObrigatorio);
            }

            if (string.IsNullOrEmpty(this.IdChapa.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Chapa.IdObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Senha.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.SenhaObrigatoria);
            }
        }
    }
}
