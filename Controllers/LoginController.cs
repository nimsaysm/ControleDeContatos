using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio; //injetar UsuarioRepositorio na variavel privada _usuarioRepositorio
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio,
                                ISessao sessao, 
                                IEmail email) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao; //injeção de dependência do construtor
            _email = email;
        }

        public IActionResult Index()
        {
            //se o usuário já estiver logado, será direcionado para a Home, sem ser necessário entrar novamente
            if(_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha() 
        {
            return View();
        }

        public IActionResult SairDaSessaoUsuario()
        {
            _sessao.RemoverSessaoDoUsuario();
            
            return RedirectToAction("Index", "Login");
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
                        //validação de senha e criação de sessão do usuário validado
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
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


        [HttpPost] 
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if(usuario != null) //achou usuário por meio do email e login
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        // e-mail para redefinir senha com email do usuario, assunto e mensagem
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);
                        
                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos uma nova senha para seu e-mail cadastrado.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar o e-mail para redefinição de senha. Por favor, tente novamente.";
                        }
                
                        return RedirectToAction("Index", "Login");
                    }
                    
                    TempData["MensagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
                }

                return View("Index"); //se o login der errado, irá voltar para Index
            }
            catch (Exception erro)
            {
                
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente! Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}