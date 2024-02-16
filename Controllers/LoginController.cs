using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio; //injetar UsuarioRepositorio na variavel privada _usuarioRepositorio

        public LoginController(IUsuarioRepositorio usuarioRepositorio) 
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login); //buscar usuario no BD

                    if(usuario != null)
                    {
                        //validação de senha
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home"); //ação Index da controller Home
                        }
                        
                        TempData["MensagemErro"] = "Senha do usuário é inválida. Por favor, tente novamente!";
                    }
                    
                    TempData["MensagemErro"] = "Usuário e/ou senha inválido(s). Por favor, tente novamente!";
                }

                return View("Index"); //se o login der errado, irá voltar para Index
            }
            catch (Exception erro)
            {
                
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente! Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}