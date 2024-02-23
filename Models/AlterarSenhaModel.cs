using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; } //id do usuário atual
        
        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public string SenhaAtual { get; set; }  

        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")] //compara se o atributo ConfirmarNovaSenha é igual ao NovaSenha
        public string ConfirmarNovaSenha { get; set; }
    }
}