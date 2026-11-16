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

        public async Task AdicionarAsync(Usuario usuario, CancellationToken cancellationToken = default) {
            
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarAsync(Usuario usuario, CancellationToken cancellationToken = default) {
            
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ExcluirAsync(Guid idUsuario, CancellationToken cancellationToken = default) {

            var usuario = await ObterPorIdAsync(idUsuario, cancellationToken);

            if (usuario == null) return;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Email == email, cancellationToken);
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid idUsuario, CancellationToken cancellationToken = default) {

            return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.IdUsuario == idUsuario, cancellationToken);
        }
    }
}
