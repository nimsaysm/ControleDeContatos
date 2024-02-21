using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        //irá retornar a busca do login do tipo UsuarioModel
        UsuarioModel BuscarPorLogin(string login);

        UsuarioModel BuscarPorEmailELogin(string email, string login); //para redefinição de senha
        
        UsuarioModel ListarPorId(int id);

        List<UsuarioModel> BuscarTodos();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar(UsuarioModel usuario);
        
        bool ApagarUsuario(int id);
    }
}