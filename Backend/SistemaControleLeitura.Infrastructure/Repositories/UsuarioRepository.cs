using Microsoft.EntityFrameworkCore;
using SistemaControleLeitura.Application.Interfaces;
using SistemaControleLeitura.Domain.Entities;
using SistemaControleLeitura.Infrastructure.Data;

namespace SistemaControleLeitura.Infrastructure.Repositories {
    public class UsuarioRepository : IUsuarioRepository {

        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context) {
            _context = context;
        }

        public async Task CadastrarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken = default) {

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario, CancellationToken cancellationToken = default) {

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirUsuarioAsync(Guid idUsuario, CancellationToken cancellationToken = default) {

            var usuario = await ObterUsuarioPorIdAsync(idUsuario, cancellationToken);

            if (usuario == null) return;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid idUsuario, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.IdUsuario == idUsuario, cancellationToken);
        }

        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email, cancellationToken);
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosAsync(CancellationToken cancellationToken = default) {

            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
