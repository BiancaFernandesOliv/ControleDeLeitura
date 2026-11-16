using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Application.Interfaces;

public interface IUsuarioRepository {

    Task CadastrarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterUsuarioPorIdAsync(Guid idUsuario, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterUsuarioPorEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default);

    Task AtualizarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken = default);

    Task ExcluirUsuarioAsync(Guid idUsuario, CancellationToken cancellationToken = default);

}
