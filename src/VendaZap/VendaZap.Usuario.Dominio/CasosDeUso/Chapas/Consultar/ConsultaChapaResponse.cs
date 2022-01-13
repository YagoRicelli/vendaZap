using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar
{
    public class ConsultaChapaResponse : Response
    {
        public ConsultaChapaResponse(List<ChapaDTO> chapas)
        {
            if (chapas == null || !chapas.Any())
            {
                return;
            }

            this.Chapas = chapas.Select(x => new ChapaRetorno
            {
                Id = x.Id,
                Nome = x.Nome
            }).ToList();
        }

        public List<ChapaRetorno> Chapas { get; set; }

        public class ChapaRetorno
        {
            public string Id { get; set; }
            public string Nome { get; set; }
        }
    }
}
