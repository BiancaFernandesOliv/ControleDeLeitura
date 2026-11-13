namespace SistemaControleLeitura.Domain.Models {
    public class Livro {

        public Guid IdLivro { get; private set; } = Guid.NewGuid();
        public Usuario Usuario { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Autor { get; private set; } = string.Empty;
        public int NumeroPaginas { get; private set; }


        public Livro(Usuario usuario, string titulo, string autor, int numeroPaginas) {

            ValidarUsuario(usuario);
            ValidarTitulo(titulo);
            ValidarAutor(autor);
            ValidarNumeroPaginas(numeroPaginas);

            Usuario = usuario;
            Titulo = titulo.Trim();
            Autor = autor.Trim();
            NumeroPaginas = numeroPaginas;
        }

        public void AlterarTitulo(string novoTitulo) {
            novoTitulo = novoTitulo.Trim();

            ValidarTitulo(novoTitulo);
            Titulo = novoTitulo;
        }

        public void AlterarAutor(string novoAutor) {
            novoAutor = novoAutor.Trim();

            ValidarAutor(novoAutor);
            Autor = novoAutor;
        }

        public void AlterarNumeroPaginas(int novoNumeroPaginas) {

            ValidarNumeroPaginas(novoNumeroPaginas);
            NumeroPaginas = novoNumeroPaginas;
        }

        private static void ValidarUsuario(Usuario usuario) {
            if (usuario is null) throw new ArgumentNullException(nameof(usuario), "Usuário informado é inválido.");

            if (usuario.IdUsuario == Guid.Empty) throw new ArgumentException("O identificador do usuário é inválido.");
        }

        private static void ValidarTitulo(string titulo) {
            if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Nome informado é inválido.");
        }

        private static void ValidarAutor(string autor) {
            if (string.IsNullOrWhiteSpace(autor)) throw new ArgumentException("Nome informado é inválido.");

            if (!autor.Any(char.IsLetter)) throw new ArgumentException("O nome deve conter pelo menos uma letra.");

            if (autor.Any(c => !char.IsLetter(c) && c != ' ' && c != '-' && c != '\'')) throw new ArgumentException("O nome contém caracteres inválidos.");
        }

        private static void ValidarNumeroPaginas(int numeroPaginas) {
            if (numeroPaginas <= 0) throw new ArgumentException("O número de páginas deve ser maior que zero.");
        }
    }
}
