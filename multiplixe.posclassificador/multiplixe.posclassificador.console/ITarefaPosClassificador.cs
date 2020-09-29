using coredto = multiplixe.comum.dto;

namespace multiplixe.posclassificador.console
{
    interface ITarefaPosClassificador
    {
        void Executar(coredto.UsuarioParaProcessar usuarioParaProcessar);
    }
}
