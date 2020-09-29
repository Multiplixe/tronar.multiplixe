using multiplixe.comum.dto.interfaces;
using System;

namespace multiplixe.comum.dto
{
    public class EnvelopeEvento<T> : IEmpresaID where T : EventoBase 
    {
        public Guid Id { get; set; }
        public DateTime DataEvento { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
        public T Evento { get; set; }

        public EnvelopeEvento(T evento)
        {
            Evento = evento;
        }

        public EnvelopeEvento()
        {

        }

        public EnvelopeEvento(Guid usuarioId, DateTime dataEvento, T evento)
        {
            UsuarioId = usuarioId;
            Evento = evento;
            DataEvento = dataEvento;
        }

        public EnvelopeEvento<Tt> Transformar<Tt>(Tt evento) where Tt : EventoBase
        {
            return new EnvelopeEvento<Tt>(evento) { 
                DataEvento = this.DataEvento,
                EmpresaId = this.EmpresaId,
                UsuarioId = this.UsuarioId,
                Id = this.Id
            };
        }


    }
}
