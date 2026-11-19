using SistemaControleLeitura.Domain.Entities;

namespace SistemaControleLeitura.Tests.Domain {
    public class UsuarioTests {

        [Fact]
        public void Construtor_QuandoDadosValidos_DeveCriarUsuario() {

            var nome = "Maria";
            var email = "maria@gmail.com";
            var senhaHash = "hashSenha123";

            var usuario = new Usuario(nome, email, senhaHash);

            Assert.NotEqual(Guid.Empty, usuario.IdUsuario);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(senhaHash, usuario.SenhaHash);
        }

        [Fact]
        public void Construtor_QuandoNomeInvalido_DeveLancarExcecao() {

            var nome = "";
            var email = "maria@gmail.com";
            var senhaHash = "hashSenha123";

            var excecao = Assert.Throws<ArgumentException>(
                () => new Usuario(nome, email, senhaHash)
            );

            Assert.Equal("Nome informado é inválido.", excecao.Message);
        }

        [Fact]
        public void Construtor_QuandoEmailInvalido_DeveLancarExcecao() {

            var nome = "Maria";
            var email = "";
            var senhaHash = "hashSenha123";

            var excecao = Assert.Throws<ArgumentException>(
                () => new Usuario(nome, email, senhaHash)
            );

            Assert.Equal("Email informado é inválido.", excecao.Message);
        }

        [Fact]
        public void Construtor_QuandoSenhaHashInvalida_DeveLancarExcecao() {

            var nome = "Maria";
            var email = "maria@gmail.com";
            var senhaHash = "";

            var excecao = Assert.Throws<ArgumentException>(
                () => new Usuario(nome, email, senhaHash)
            );

            Assert.Equal("Hash da senha informado é inválido.", excecao.Message);
        }

        [Fact]
        public void AlterarNome_QuandoNomeValido_DeveAtualizarNome() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novoNome = "Maria Silva";

            usuario.AlterarNome(novoNome);

            Assert.Equal(novoNome, usuario.Nome);
        }

        [Fact]
        public void AlterarNome_QuandoNomeInvalido_DeveLancarExcecao() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novoNome = "";

            var excecao = Assert.Throws<ArgumentException>(
                () => usuario.AlterarNome(novoNome));

            Assert.Equal("Nome informado é inválido.", excecao.Message);
        }

        [Fact]
        public void AlterarEmail_QuandoEmailValido_DeveAtualizarEmail() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novoEmail = "maria.novo@gmail.com";

            usuario.AlterarEmail(novoEmail);

            Assert.Equal(novoEmail, usuario.Email);
        }

        [Fact]
        public void AlterarEmail_QuandoEmailInvalido_DeveLancarExcecao() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novoEmail = "";

            var excecao = Assert.Throws<ArgumentException>(
                () => usuario.AlterarEmail(novoEmail));

            Assert.Equal("Email informado é inválido.", excecao.Message);
        }

        [Fact]
        public void AlterarSenhaHash_QuandoHashValido_DeveAtualizarSenhaHash() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novaSenhaHash = "NovaSenha123";

            usuario.AlterarSenhaHash(novaSenhaHash);

            Assert.Equal(novaSenhaHash, usuario.SenhaHash);
        }

        [Fact]
        public void AlterarSenhaHash_QuandoHashInvalido_DeveLancarExcecao() {

            var usuario = new Usuario("Maria", "maria@gmail.com", "hashSenha123");
            var novaSenhaHash = "";


            var excecao = Assert.Throws<ArgumentException>(
                () => usuario.AlterarSenhaHash(novaSenhaHash));

            Assert.Equal("Hash da senha informado é inválido.", excecao.Message);
        }
    }
}
