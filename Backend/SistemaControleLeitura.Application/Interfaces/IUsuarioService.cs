using SistemaControleLeitura.Application.DTOs;
using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Application.Interfaces {
    public interface IUsuarioService {

        Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto);

        Task<Usuario?> ObterUsuarioPorIdAsync(Guid idUsuario);

        Task<Usuario?> ObterUsuarioPorEmailAsync(string email);

        Task<Usuario> AtualizarUsuarioAsync(Guid idUsuario, UsuarioAtualizacaoDto usuarioDto);

        Task ExcluirUsuarioAsync(Guid idUsuario);
    }
}
