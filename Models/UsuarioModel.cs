using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

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
            return Senha == senha.GerarHash(); //se for igual retornará True
        }

        //criptografar a senha
        public void SetSenhaHash()
        {
            //senha que o usuário digitou = ela mesma criptografada
            Senha = Senha.GerarHash();
        }

        //gerar nova senha ao resetar na aplicação
        public string GerarNovaSenha()
        {
            //novaSenha terá os 8 primeiros caracteres gerados
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = novaSenha.GerarHash(); //senha com hash ficará no BD
            return novaSenha; //senha sem hash que será enviada no e-mail
        }
    }
}