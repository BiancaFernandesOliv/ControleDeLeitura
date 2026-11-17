using Microsoft.AspNetCore.Mvc;
using SistemaControleLeitura.Application.DTOs;
using SistemaControleLeitura.Application.Interfaces;

namespace SistemaControleLeitura.API.Controllers {

    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuarioAsync([FromBody] UsuarioCadastroDto usuarioDto) {

            try {

                var usuarioCriado = await _usuarioService.CadastrarUsuarioAsync(usuarioDto);

                var resposta = new UsuarioRespostaDto {
                    Id = usuarioCriado.IdUsuario,
                    Nome = usuarioCriado.Nome,
                    Email = usuarioCriado.Email
                };

                var respostaPadrao = new RespostaPadraoDto {
                    Sucesso = true,
                    Mensagem = "Usuário cadastrado com sucesso.",
                    Dados = resposta
                };

                return StatusCode(201, respostaPadrao);
            }
            catch (ArgumentException ex) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = ex.Message
                };

                if (ex.Message == "E-mail já cadastrado.")
                    return Conflict(resposta);

                return BadRequest(resposta);
            }
        }


        [HttpPatch("{idUsuario:guid}")]
        public async Task<IActionResult> AtualizarUsuarioAsync(Guid idUsuario, [FromBody] UsuarioAtualizacaoDto usuarioDto) {

            try {

                var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(idUsuario, usuarioDto);

                var resposta = new UsuarioRespostaDto {
                    Id = usuarioAtualizado.IdUsuario,
                    Nome = usuarioAtualizado.Nome,
                    Email = usuarioAtualizado.Email
                };

                var respostaPadrao = new RespostaPadraoDto {
                    Sucesso = true,
                    Mensagem = "Usuário atualizado com sucesso.",
                    Dados = resposta
                };

                return Ok(respostaPadrao);
            }
            catch (ArgumentException ex) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = ex.Message
                };

                if (ex.Message == "Usuário não encontrado.")
                    return NotFound(resposta);

                if (ex.Message == "E-mail já cadastrado.")
                    return Conflict(resposta);

                return BadRequest(resposta);
            }
        }

        [HttpDelete("{idUsuario:guid}")]
        public async Task<IActionResult> ExcluirUsuarioAsync(Guid idUsuario) {

            try {

                await _usuarioService.ExcluirUsuarioAsync(idUsuario);

                var respostaPadrao = new RespostaPadraoDto {
                    Sucesso = true,
                    Mensagem = "Usuário excluído com sucesso.",
                    Dados = null
                };

                return Ok(respostaPadrao);

            }
            catch (ArgumentException ex) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = ex.Message
                };

                if (ex.Message == "Usuário não encontrado.")
                    return NotFound(resposta);

                return BadRequest(resposta);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ObterUsuarioPorEmailAsync(string email) {

            var usuario = await _usuarioService.ObterUsuarioPorEmailAsync(email);

            if (usuario == null) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = "Usuário não encontrado."
                };

                return NotFound(resposta);
            }

            var respostaUsuario = new UsuarioRespostaDto {
                Id = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = email
            };

            var respostaPadrao = new RespostaPadraoDto {
                Sucesso = true,
                Mensagem = "Usuário encontrado.",
                Dados = respostaUsuario
            };

            return Ok(respostaPadrao);
        }

        [HttpGet("{idUsuario:guid}")]
        public async Task<IActionResult> ObterUsuarioPorIdAsync(Guid idUsuario) {

            var usuario = await _usuarioService.ObterUsuarioPorIdAsync(idUsuario);

            if (usuario == null) {

                var resposta = new RespostaPadraoDto {
                    Sucesso = false,
                    Mensagem = "Usuário não encontrado."
                };

                return NotFound(resposta);
            }

            var respostaUsuario = new UsuarioRespostaDto {
                Id = idUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            var respostaPadrao = new RespostaPadraoDto {
                Sucesso = true,
                Mensagem = "Usuário encontrado.",
                Dados = respostaUsuario
            };

            return Ok(respostaPadrao);

        }

    }
}


