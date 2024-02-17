using System.Text.Json;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace ControleDeContatos.Filters
{

    // ActionFilterAttribute -> antes de executar qualquer coisa da Controller, irá fazer as validações
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        //ActionFilterAttribute possui o método OnActionExecuting, que soferá override (será sobrescrito)
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //verificar se o usuário está logado
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario)) //não há sessão de usuário logado, irá redicionar para a controller Login e action Index
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            { 
                UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario); 
                
                //outra verificação -> se não conseguir deserializar (é nulo) irá redirecionar
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }

            //se não for nulo (passou pelas verificações), irá manter o codigo base do ActionFilter
            base.OnActionExecuting(context);
        }

    }
}