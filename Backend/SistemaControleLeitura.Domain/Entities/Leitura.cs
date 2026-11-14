using SistemaControleLeitura.Domain.Enums;

namespace SistemaControleLeitura.Domain.Entities {
    public class Leitura {

        public Guid IdLeitura { get; private set; } = Guid.NewGuid();
        public Guid IdUsuario { get; private set; }
        public Usuario Usuario { get; private set; }
        public Guid IdLivro { get; private set; }
        public Livro Livro { get; private set; }
        public StatusLeitura StatusLeitura { get; private set; } = StatusLeitura.QueroLer;
        public DateTime? DataInicio { get; private set; }
        public DateTime? DataTermino { get; private set; }

        public Leitura(Usuario usuario, Livro livro) {

            ValidarUsuario(usuario);
            ValidarLivro(livro);

            IdUsuario = usuario.IdUsuario;
            IdLivro = livro.IdLivro;
            Usuario = usuario;
            Livro = livro;
        }

        public void RegistrarComoLendo(DateTime dataInicio) {
            ValidarDataInicio(dataInicio);

            StatusLeitura = StatusLeitura.Lendo;
            DataInicio = dataInicio;
            DataTermino = null;
        }

        public void RegistrarComoLido(DateTime dataInicio, DateTime dataTermino) {
            ValidarDataInicio(dataInicio);
            ValidarDataTermino(dataInicio, dataTermino);

            StatusLeitura = StatusLeitura.Lido;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
        }

        private static void ValidarUsuario(Usuario usuario) {
            if (usuario is null) throw new ArgumentNullException(nameof(usuario), "Usuário informado é inválido.");

            if (usuario.IdUsuario == Guid.Empty) throw new ArgumentException("O identificador do usuário é inválido.");
        }
        private static void ValidarLivro(Livro livro) {
            if (livro is null) throw new ArgumentNullException(nameof(livro), "Livro informado é inválido.");

            if (livro.IdLivro == Guid.Empty) throw new ArgumentException("O identificador do livro é inválido.");
        }

        private static void ValidarDataInicio(DateTime dataInicio) {
            if (dataInicio > DateTime.Now) throw new ArgumentException("A data de início não pode estar no futuro.");
        }

        private static void ValidarDataTermino(DateTime dataInicio, DateTime dataTermino) {
            if (dataTermino > DateTime.Now) throw new ArgumentException("A data de término não pode estar no futuro.");

            if (dataTermino < dataInicio) throw new ArgumentException("A data de término não pode ser anterior à data de início.");
        }
    }
}
