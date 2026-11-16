using SistemaControleLeitura.Application.DTOs;
using SistemaControleLeitura.Application.Interfaces;
using SistemaControleLeitura.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace SistemaControleLeitura.Application.Services {
    public class UsuarioService : IUsuarioService {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(IUsuarioRepository usuarioRepository) {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroDto usuarioDto) {

            var email = usuarioDto.Email.Trim();

            if (await _usuarioRepository.ExisteEmailAsync(email))
                throw new ArgumentException("E-mail já cadastrado.");

            var senhaHash = _passwordHasher.HashPassword(null!, usuarioDto.Senha);

            var usuario = new Usuario(
                usuarioDto.Nome,
                email,
                senhaHash
            );

            await _usuarioRepository.CadastrarUsuarioAsync(usuario);

            return usuario;
        }

        public async Task<Usuario> AtualizarUsuarioAsync(Guid idUsuario, UsuarioAtualizacaoDto usuarioDto) {

            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(idUsuario);

            if (usuario == null) throw new ArgumentException("Usuário não encontrado.");

            usuario.AlterarNome(usuarioDto.Nome);

            var email = usuarioDto.Email.Trim();

            var usuarioComEmail = await _usuarioRepository.ObterUsuarioPorEmailAsync(email);

            if (usuarioComEmail != null && usuarioComEmail.IdUsuario != idUsuario) throw new ArgumentException("E-mail já cadastrado.");

            usuario.AlterarEmail(email);

            var senhaHash = _passwordHasher.HashPassword(null!, usuarioDto.Senha);

            usuario.AlterarSenhaHash(senhaHash);

            await _usuarioRepository.AtualizarUsuarioAsync(usuario);

            return usuario;
        }

        public async Task ExcluirUsuarioAsync(Guid idUsuario) {

            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(idUsuario);

            if (usuario == null) throw new ArgumentException("Usuário não encontrado.");

            await _usuarioRepository.ExcluirUsuarioAsync(idUsuario);
        }

        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email) {

            return await _usuarioRepository.ObterUsuarioPorEmailAsync(email);
        }

        public async Task<Usuario?> ObterUsuarioPorIdAsync(Guid idUsuario) {

            return await _usuarioRepository.ObterUsuarioPorIdAsync(idUsuario);
        }
    }
}
