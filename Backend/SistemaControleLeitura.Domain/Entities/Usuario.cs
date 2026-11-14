using System.ComponentModel.DataAnnotations;

namespace SistemaControleLeitura.Domain.Entities {
    public class Usuario {

        public Guid IdUsuario { get; private set; } = Guid.NewGuid();
        public string Nome { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string SenhaHash { get; private set; } = string.Empty;

        public Usuario(string nome, string email, string senhaHash) {

            ValidarNome(nome);
            ValidarEmail(email);
            ValidarSenhaHash(senhaHash);

            Nome = nome.Trim();
            Email = email.Trim();
            SenhaHash = senhaHash;
        }

        public void AlterarNome(string novoNome) {
            novoNome = novoNome.Trim();

            ValidarNome(novoNome);
            Nome = novoNome;
        }

        public void AlterarEmail(string novoEmail) {
            novoEmail = novoEmail.Trim();

            ValidarEmail(novoEmail);
            Email = novoEmail;
        }

        public void AlterarSenhaHash(string novaSenhaHash) {
            ValidarSenhaHash(novaSenhaHash);
            SenhaHash = novaSenhaHash;
        }

        private static void ValidarNome(string nome) {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome informado é inválido.");

            if (!nome.Any(char.IsLetter)) throw new ArgumentException("O nome deve conter pelo menos uma letra.");

            if (nome.Any(c => !char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')) throw new ArgumentException("O nome contém caracteres inválidos.");
        }

        private static void ValidarEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-mail informado é inválido.");

            if (!new EmailAddressAttribute().IsValid(email)) throw new ArgumentException("E-mail informado é inválido.");
        }

        private static void ValidarSenhaHash(string senhaHash) {
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("Hash da senha informado é inválido.");
        }
    }
}
