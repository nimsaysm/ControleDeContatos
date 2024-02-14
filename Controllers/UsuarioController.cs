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
    }
}