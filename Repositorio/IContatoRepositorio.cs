using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {
        //com o parâmetro do id, vai retornar o ContatoModel correspondente;
        ContatoModel ListarPorId(int id);

        //contrato para listar dados do BD
        List<ContatoModel> BuscarTodos(int usuarioId);

        //Adicionar recebe contato como parâmetro e retorna ContatoModel 
        ContatoModel Adicionar(ContatoModel contato);

        //para atualizar informações de contato já existente
        ContatoModel Atualizar(ContatoModel contato);
        
        bool ApagarContato(int id);
    }
}