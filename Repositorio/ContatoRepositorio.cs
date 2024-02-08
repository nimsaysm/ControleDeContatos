using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    //implementando interface
    public class ContatoRepositorio : IContatoRepositorio
    {
        //variavel do tipo privada pra passar o parâmetro
        private readonly BancoContext _bancoContext;

        //injetar BancoContext no Repositorio
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public ContatoModel ListarPorId(int id)
        {
            //FirstOrDefault para buscar o primeiro ou único registro
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos()
        {
            //irá retornar registros da tabela Contatos em lista
            return _bancoContext.Contatos.ToList();
        }
        
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato); //inserindo no BD
            _bancoContext.SaveChanges();//confirmar a inserção
            
            return contato;
        }

        //vai buscar registros do Id no BD e atualizar conforme o preenchimento do usuário
        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            //só deixa alterar se o Id for existente
            if(contatoDB == null) throw new System.Exception("Houve um erro na atualização do contato!");

            //se não for null
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool ApagarContato(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if(contatoDB == null) throw new System.Exception("Houve um erro na exclusão do contato!");


            _bancoContext.Contatos.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true; //retorna true se ocorreu tudo certo
        }
    }
}