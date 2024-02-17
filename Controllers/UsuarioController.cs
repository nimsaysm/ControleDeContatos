using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio; //variável recebe o que está sendo injetado no construtor
        }

        public IActionResult Index()
        {
            List <UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult CriarUsuario()
        {
            return View();
        }

        public IActionResult EditarUsuario(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id) 
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id); 
            return View(usuario);
        }

        public IActionResult ApagarUsuario(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.ApagarUsuario(id);
                if(apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = "Não conseguimos apagar seu usuário!";
                }
                
                return RedirectToAction("Index");  

            }
            catch (Exception erro)
            {
                
                TempData["MensagemErro"] = $"Não conseguimos apagar seu usuário! Mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");  

            }
        }

        [HttpPost]
        public IActionResult CriarUsuario(UsuarioModel usuario)
        {
            try
            {
                //se a informação do Model (campos do forms) é válida
                if(ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso"; 
                    return RedirectToAction("Index"); 
                }
            
                return View(usuario); 
            }
            catch (System.Exception erro) 
            {
                TempData["MensagemErro"] = $"Não conseguimos cadastrar seu usuário, tente novamente! Detalhe do erro: {erro.Message}"; 
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;

                if(ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso"; 
                    return RedirectToAction("Index"); 
                }
                
                return View(usuario);
            }
            catch (Exception erro)
            {
                
                TempData["MensagemErro"] = $"Não conseguimos atualizar seu usuário, tente novamente! Detalhe do erro: {erro.Message}"; 
                return RedirectToAction("Index");
            }
        }
    }
}