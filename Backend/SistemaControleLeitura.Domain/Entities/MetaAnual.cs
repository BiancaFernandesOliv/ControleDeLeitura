namespace SistemaControleLeitura.Domain.Entities {
    public class MetaAnual {

        public Guid IdMeta { get; private set; } = Guid.NewGuid();
        public Guid IdUsuario { get; private set; }
        public Usuario Usuario { get; private set; }
        public int Ano {  get; private set; }
        public int MetaQuantidade { get; private set; }

        public MetaAnual(Usuario usuario, int ano, int metaQuantidade) {

            ValidarUsuario(usuario);
            ValidarAno(ano);
            ValidarMetaQuantidade(metaQuantidade);

            IdUsuario = usuario.IdUsuario;
            Usuario = usuario;
            Ano = ano;
            MetaQuantidade = metaQuantidade;
        }

        public void AlterarMetaQuantidade(int novaMetaQuantidade) {
            ValidarMetaQuantidade(novaMetaQuantidade);

            MetaQuantidade = novaMetaQuantidade;
        }

        private static void ValidarUsuario(Usuario usuario) {
            if (usuario is null) throw new ArgumentNullException(nameof(usuario), "Usuário informado é inválido.");

            if (usuario.IdUsuario == Guid.Empty) throw new ArgumentException("O identificador do usuário é inválido.");
        }

        private static void ValidarAno(int ano) {
            if (ano < DateTime.Now.Year) throw new ArgumentException("Não é possível definir uma meta para um ano passado.");
        }

        private static void ValidarMetaQuantidade(int metaQuantidade) {
            if (metaQuantidade <= 0) throw new ArgumentOutOfRangeException(nameof(metaQuantidade), "Meta deve ser maior que zero.");
        }
    }
}
