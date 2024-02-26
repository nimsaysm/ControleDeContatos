using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    //importando o filter de acesso dos usuários logados
    [PaginaParaUsuarioLogado]

    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepositorio contatoRepositorio,
                                ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            //mostrará na interface Index registros do BD
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List <ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult CriarContato()
        {
            return View();
        }

        public IActionResult EditarContato(int id)
        {
            //id vem do FrontEnd
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato); //o contato que buscou é enviado para a View de Edição 
        }
        
        public IActionResult ApagarConfirmacao(int id) //aviso da exclusão
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id); //busca o contato pelo Id
            return View(contato);
        }

        public IActionResult ApagarContato(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.ApagarContato(id);
                if(apagado) //se apagado é true
                {
                    TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar seu contato!";
                }
                
                return RedirectToAction("Index");  

            }
            catch (System.Exception erro)
            {
                
                TempData["MensagemErro"] = $"Não conseguimos apagar seu contato! Mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");  

            }
        }
        

        //por padrão, métodos sem assinatura são métodos do tipo get
        //assinando método como post
        [HttpPost]
        public IActionResult CriarContato(ContatoModel contato)
        {
            try
            {
                //se a informação do Model (campos do forms) é válida
                if(ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario(); //assim que criar, irá passar o usuário logado
                    contato.UsuarioId = usuarioLogado.Id;
                    
                    contato = _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso"; //mensagem temporária de sucesso ao criar o contato
                    return RedirectToAction("Index"); //ação de voltar para a página Index
                }
            
                return View(contato); //se não for válido retorna para a View mostrando o ContatoModel
            }
            catch (System.Exception erro) //o Exception retorna o que deu errado e isso será armazenado em "erro"
            {
                TempData["MensagemErro"] = $"Não conseguimos cadastrar seu contato, tente novamente! Detalhe do erro: {erro.Message}"; //irá mostrar a mensagem do erro depois da string
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditarContato(ContatoModel contato)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso"; 
                    return RedirectToAction("Index"); //ação de voltar para a página Index
                }
                
                return View(contato);
            }
            catch (System.Exception erro)
            {
                
                TempData["MensagemErro"] = $"Não conseguimos atualizar seu contato, tente novamente! Detalhe do erro: {erro.Message}"; //irá mostrar a mensagem do erro depois da string
                return RedirectToAction("Index");
            }
        }
    }
}