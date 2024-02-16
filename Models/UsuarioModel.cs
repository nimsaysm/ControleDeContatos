using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Enums;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; } //informado pelo usuário
        
        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        public string Email { get; set; }
    

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum Perfil { get; set; } //determina se é usuário padrão ou administrador

        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string Senha { get; set; }

        public DateTime DataCadastroUsuario { get; set; }

        //sinal ? determina que a propriedade não é obrigatória (pode ser nulo)
        public DateTime? DataAtualizacaoUsuario { get; set; }


        public bool SenhaValida(string senha)
        {
            //compara Senha informada pelo usuário com a senha preenchida no login
            return Senha == senha; //se for igual retornará True
        }
    }
}