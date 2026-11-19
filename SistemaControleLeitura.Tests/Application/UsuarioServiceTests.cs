using Microsoft.AspNetCore.Identity;
using Moq;
using SistemaControleLeitura.Application.DTOs;
using SistemaControleLeitura.Application.Interfaces;
using SistemaControleLeitura.Application.Services;
using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Tests.Application {
    public class UsuarioServiceTests {

        private readonly Mock<IUsuarioRepository> _repositoryMock;
        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests() {
            _repositoryMock = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarUsuarioAsync_QuandoDadosValidos_DeveCadastrarUsuario() {

            var dto = new UsuarioCadastroDto {
                Nome = "Maria",
                Email = "maria@gmail.com",
                Senha = "Senha123!"
            };

            _repositoryMock
                .Setup(r => r.ExisteEmailAsync(dto.Email))
                .ReturnsAsync(false);

            var resultado = await _usuarioService.CadastrarUsuarioAsync(dto);

            var passwordHasher = new PasswordHasher<Usuario>();

            var verificacaoSenha = passwordHasher.VerifyHashedPassword(
                resultado,
                resultado.SenhaHash,
                dto.Senha
                );

            Assert.Equal(PasswordVerificationResult.Success, verificacaoSenha);

            Assert.NotEqual(Guid.Empty, resultado.IdUsuario);
            Assert.Equal(dto.Nome, resultado.Nome);
            Assert.Equal(dto.Email, resultado.Email);
            Assert.NotEqual(dto.Senha, resultado.SenhaHash);

            _repositoryMock.Verify(r => r.CadastrarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ExisteEmailAsync(
                dto.Email, It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task CadastrarUsuarioAsync_QuandoEmailJaCadastrado_DeveLancarExcecao() {

            var dto = new UsuarioCadastroDto {
                Nome = "Maria",
                Email = "maria@gmail.com",
                Senha = "Senha123!"
            };

            _repositoryMock
                .Setup(r => r.ExisteEmailAsync(dto.Email))
                .ReturnsAsync(true);

            var excecao = await Assert.ThrowsAsync<ArgumentException>(
                () => _usuarioService.CadastrarUsuarioAsync(dto)
            );

            Assert.Equal("E-mail já cadastrado.", excecao.Message);

            _repositoryMock.Verify(r => r.CadastrarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Never
                );

            _repositoryMock.Verify(r => r.ExisteEmailAsync(
                dto.Email, It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_QuandoDadosValidos_DeveAtualizarUsuario() {

            Usuario usuarioExistente = new Usuario(
                "Maria Antigo",
                "maria.antigo@gmail.com",
                "hashSenhaAntiga"
            );

            var idUsuario = usuarioExistente.IdUsuario;

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync(usuarioExistente);

            var dto = new UsuarioAtualizacaoDto {
                Nome = "Maria Novo",
                Email = "maria.novo@gmail.com",
                Senha = "NovaSenha123"
            };

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorEmailAsync(dto.Email))
                .ReturnsAsync((Usuario?)null);

            var resultado = await _usuarioService.AtualizarUsuarioAsync(idUsuario, dto);

            var passwordHasher = new PasswordHasher<Usuario>();

            var verificacaoSenha = passwordHasher.VerifyHashedPassword(
                resultado,
                resultado.SenhaHash,
                dto.Senha
                );

            Assert.Equal(PasswordVerificationResult.Success, verificacaoSenha);

            Assert.Equal(idUsuario, resultado.IdUsuario);
            Assert.Equal(dto.Nome, resultado.Nome);
            Assert.Equal(dto.Email, resultado.Email);
            Assert.NotEqual(dto.Senha, resultado.SenhaHash);

            _repositoryMock.Verify(r => r.AtualizarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorEmailAsync(
                dto.Email,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_QuandoDadosNaoInformados_DeveLancarExcecao() {

            var idUsuario = Guid.NewGuid();

            var dto = new UsuarioAtualizacaoDto {
                Nome = "",
                Email = "",
                Senha = ""
            };

            var excecao = await Assert.ThrowsAsync<ArgumentException>(
                () => _usuarioService.AtualizarUsuarioAsync(idUsuario, dto)
                );

            Assert.Equal("Informe pelo menos um campo para atualizar.", excecao.Message);

            _repositoryMock.Verify(r => r.AtualizarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Never
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Never
                );
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_QuandoUsuarioNaoExiste_DeveLancarExcecao() {

            var idUsuario = Guid.NewGuid();

            var dto = new UsuarioAtualizacaoDto {
                Nome = "Maria",
                Email = "maria@gmail.com",
                Senha = "Senha123!"
            };

            _repositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync((Usuario?)null);

            var excecao = await Assert.ThrowsAsync<ArgumentException>(
                () => _usuarioService.AtualizarUsuarioAsync(idUsuario, dto)
                );

            Assert.Equal("Usuário não encontrado.", excecao.Message);

            _repositoryMock.Verify(r => r.AtualizarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Never
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorEmailAsync(dto.Email,
                It.IsAny<CancellationToken>()),
                Times.Never
                );
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_QuandoEmailJaCadastrado_DeveLancarExcecao() {

            Usuario usuarioExistente1 = new Usuario(
                "Maria Clara",
                "maria@gmail.com",
                "hashSenhaAntiga"
            );

            Usuario usuarioExistente2 = new Usuario(
                "Maria Paula",
                "maria.paula@gmail.com",
                "hashSenhaAntiga"
            );

            var dto = new UsuarioAtualizacaoDto {
                Nome = "Maria Paula",
                Email = "maria@gmail.com",
                Senha = "NovaSenha123"
            };

            var idUsuarioAtualização = usuarioExistente2.IdUsuario;

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorIdAsync(idUsuarioAtualização))
                .ReturnsAsync(usuarioExistente2);

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorEmailAsync(dto.Email))
                .ReturnsAsync(usuarioExistente1);

            var excecao = await Assert.ThrowsAsync<ArgumentException>(
                () => _usuarioService.AtualizarUsuarioAsync(idUsuarioAtualização, dto)
            );

            Assert.Equal("E-mail já cadastrado.", excecao.Message);

            _repositoryMock.Verify(r => r.AtualizarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Never
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(idUsuarioAtualização,
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorEmailAsync(
                dto.Email,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task AtualizarUsuarioAsync_QuandoMantemSeuProprioEmail_DeveAtualizarUsuario() {

            Usuario usuarioExistente = new Usuario(
                "Maria Antigo",
                "maria.antigo@gmail.com",
                "hashSenhaAntiga"
            );

            var idUsuario = usuarioExistente.IdUsuario;

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync(usuarioExistente);

            var dto = new UsuarioAtualizacaoDto {
                Nome = "Maria Novo",
                Email = "maria.antigo@gmail.com",
                Senha = "NovaSenha123"
            };

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorEmailAsync(dto.Email))
                .ReturnsAsync((usuarioExistente));

            var resultado = await _usuarioService.AtualizarUsuarioAsync(idUsuario, dto);

            var passwordHasher = new PasswordHasher<Usuario>();

            var verificacaoSenha = passwordHasher.VerifyHashedPassword(
                resultado,
                resultado.SenhaHash,
                dto.Senha
                );

            Assert.Equal(PasswordVerificationResult.Success, verificacaoSenha);

            Assert.Equal(idUsuario, resultado.IdUsuario);
            Assert.Equal(dto.Nome, resultado.Nome);
            Assert.Equal(dto.Email, resultado.Email);
            Assert.NotEqual(dto.Senha, resultado.SenhaHash);

            _repositoryMock.Verify(r => r.AtualizarUsuarioAsync(
                It.IsAny<Usuario>(),
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorEmailAsync(
                dto.Email,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task ExcluirUsuarioAsync_QuandoUsuarioExiste_DeveExcluirUsuario() {

            Usuario usuario = new Usuario(
                "Maria",
                "maria@gmail.com",
                "hashSenhaAntiga"
                );

            var idUsuario = usuario.IdUsuario;

            _repositoryMock
                .Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync(usuario);

            await _usuarioService.ExcluirUsuarioAsync(idUsuario);

            _repositoryMock.Verify(r => r.ExcluirUsuarioAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task ExcluirUsuarioAsync_QuandoUsuarioNaoExiste_DeveLancarExcecao() {

            var idUsuario = Guid.NewGuid();

            _repositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync((Usuario?)null);

            var excecao = await Assert.ThrowsAsync<ArgumentException>(
                () => _usuarioService.ExcluirUsuarioAsync(idUsuario)
                );

            Assert.Equal("Usuário não encontrado.", excecao.Message);

            _repositoryMock.Verify(r => r.ExcluirUsuarioAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Never
                );

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task ObterUsuarioPorIdAsync_QuandoUsuarioExiste_DeveRetornarUsuario() {

            Usuario usuario = new Usuario(
                "Maria",
                "maria@gmail.com",
                "hashSenhaAntiga"
                );

            var idUsuario = usuario.IdUsuario;

            _repositoryMock.Setup(r => r.ObterUsuarioPorIdAsync(idUsuario))
                .ReturnsAsync(usuario);

            var resultado = await _usuarioService.ObterUsuarioPorIdAsync(idUsuario);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.IdUsuario, resultado.IdUsuario);
            Assert.Equal(usuario.Email, resultado.Email);

            _repositoryMock.Verify(r => r.ObterUsuarioPorIdAsync(
                idUsuario,
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }

        [Fact]
        public async Task ObterUsuariosAsync_QuandoExistemUsuarios_DeveRetornarLista() {

            Usuario usuarioExistente1 = new Usuario(
               "Maria",
               "maria@gmail.com",
               "hashSenhaAntiga"
           );

            Usuario usuarioExistente2 = new Usuario(
                "João",
                "joao@gmail.com",
                "hashSenhaAntiga"
            );

            _repositoryMock.Setup(r => r.ObterUsuariosAsync())
                .ReturnsAsync(new List<Usuario> {
                    usuarioExistente1,
                    usuarioExistente2
                });

            var resultado = await _usuarioService.ObterUsuariosAsync();

            Assert.NotEmpty(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(usuarioExistente1, resultado);
            Assert.Contains(usuarioExistente2, resultado);

            _repositoryMock.Verify(r => r.ObterUsuariosAsync(
                It.IsAny<CancellationToken>()),
                Times.Once
                );
        }
    }
}

