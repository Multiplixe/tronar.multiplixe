using System;
using coredto = multiplixe.comum.dto;
using coreinterfaces = multiplixe.comum.interfaces.triador;

namespace multiplixe.comum.triador
{
    public class TriadorService<T> where T : coredto.EventoBase
    {
        private coreinterfaces.IValidadorDeEvento<T> validadorDeEvento { get; }
        private coreinterfaces.ITriador<T> triador { get; }
        private coreinterfaces.IIdentificadorUsuario<T> identificadorUsuario { get; }

        public TriadorService(
            coreinterfaces.IValidadorDeEvento<T> validadorDeEvento,
            coreinterfaces.IIdentificadorUsuario<T> identificadorUsuario,
            coreinterfaces.ITriador<T> triador)
        {
            this.validadorDeEvento = validadorDeEvento;
            this.triador = triador;
            this.identificadorUsuario = identificadorUsuario;
        }

        public virtual void ProcessarEnvelope(coredto.EnvelopeEvento<T> envelope)
        {
            this.validadorDeEvento.Validar(envelope);

            var responseUsuario = identificadorUsuario.Identificar(envelope);

            if (responseUsuario.Success)
            {
                envelope.Id = Guid.NewGuid();

                envelope.UsuarioId = responseUsuario.Item.UsuarioId;

                var eventoTriado = triador.Triar(envelope);

                var podeProcessar = eventoTriado.Avaliar();

                if (podeProcessar)
                {
                    eventoTriado.RegistrarEvento();

                    eventoTriado.EnfileirarEvento();
                }
            }
        }
    }
}
