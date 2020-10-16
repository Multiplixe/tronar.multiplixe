using adduohelper = adduo.helper.envelopes;

namespace multiplixe.api.integracao_grpc
{
    public interface IIntegracaoGRPC<T>  
    {
        adduohelper.ResponseEnvelope<T> Enviar();
    }

    public interface IIntegracaoGRPC
    {
        adduohelper.ResponseEnvelope Enviar();
    }
}
