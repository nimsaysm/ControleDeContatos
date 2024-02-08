using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data
{
    //usando DbContext do Entity Framework
    public class BancoContext : DbContext
    {
        //criando construtor injetando DbContextOptions<BancoContext> options
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) //passando infos do options para DbContext atraves do base()
        {
        }

        //criando a tabela, importando ConatatoModel e definindo o nome da tabela como Contatos
        public DbSet<ContatoModel> Contatos { get; set; }
    }
}