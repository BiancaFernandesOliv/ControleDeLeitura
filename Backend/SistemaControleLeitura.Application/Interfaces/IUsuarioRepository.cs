using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Application.Interfaces;

public interface IUsuarioRepository {

    Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterPorIdAsync(Guid idUsuario, CancellationToken cancellationToken = default);

    Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default);

    Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken = default);

    Task ExcluirAsync(Guid idUsuario, CancellationToken cancellationToken = default);

}
