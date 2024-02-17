using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControleDeContatos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

            return View(usuario);
        }
    }
}