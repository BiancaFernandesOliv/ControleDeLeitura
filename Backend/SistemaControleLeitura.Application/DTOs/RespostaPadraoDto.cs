namespace SistemaControleLeitura.Application.DTOs {
    public class RespostaPadraoDto {

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public object? Dados { get; set; }
    }
}
