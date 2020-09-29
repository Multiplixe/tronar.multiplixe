using multiplixe.usuarios.grpc.protos;
using dto = multiplixe.comum.dto;

namespace multiplixe.usuarios.grpc.parsers
{
    public class UsuarioBase
    {
        public UsuarioMessage Response(dto.Usuario dto)
        {
            return new UsuarioMessage
            {
                Id = dto.Id.ToString(),
                Nome = dto.Nome,
                Apelido = dto.Apelido,
                Email = dto.Email,
                DataCadastro = dto.DataCadastro.Ticks,
                EmpresaId = dto.EmpresaId.ToString()
            };
        }
    }
}
