using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        // private readonly ILogger<Contato> _logger;

        // public Contato(ILogger<Contato> logger)
        // {
        //     _logger = logger;
        // }

        private readonly IContatoRepositorio _contatoRepositorio;

        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            //mostrará na interface Index registros do BD
            List <ContatoModel> contatos = _contatoRepositorio.BuscarTodos();
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
                    _contatoRepositorio.Adicionar(contato);
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
                    _contatoRepositorio.Atualizar(contato);
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