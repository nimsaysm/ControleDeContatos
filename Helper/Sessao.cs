using ControleDeContatos.Models;
using System.Text.Json;


namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {

        //http context para criar sessões do usuário
        private readonly IHttpContextAccessor _httpContext;

        //criar construtor
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioModel BuscarSessaoDoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario)) return null; //se a sessão for nula ou vazia, irá retornar nulo

            return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario); //deserializar para transformar string em UsuarioModel
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            string valor = JsonSerializer.Serialize(usuario); //transforma objeto de informações do usuario em string
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor); //sessaoUsuarioLogado irá conter os dados de sessão do usuario (estão como objetos e devem ser convertidos para string)
        }

        public void RemoverSessaoDoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado"); //remove a chave sessaoUsuarioLogado
        }
    }
}