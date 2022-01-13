using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar
{
    public class ConsultaTelefoneResponse : Response
    {
        public ConsultaTelefoneResponse(List<TelefoneDTO> telefones)
        {
            if (telefones != null && telefones.Any())
            {
                Telefones = telefones.Select(x => new Telefone
                {
                    Id = x.Id,
                    DDD = x.DDD,
                    Numero = x.Numero
                }).ToList();
            }
        }

        public List<Telefone> Telefones { get; set; }

        public class Telefone
        {
            public string Id { get; set; }
            public int DDD { get; set; }
            public int Numero { get; set; }
        }
    }
}
