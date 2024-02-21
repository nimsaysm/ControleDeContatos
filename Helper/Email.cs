using System.Net;
using System.Net.Mail;

namespace ControleDeContatos.Helper
{
    public class Email : IEmail
    {
        // buscando informações do web settings
        private readonly IConfiguration _configuration;
        
        public Email (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                string host = _configuration.GetValue<string> ("SMTP:Host"); //traz a informação de Host
                string nome = _configuration.GetValue<string> ("SMTP:Nome");
                string username = _configuration.GetValue<string> ("SMTP:UserName");
                string senha = _configuration.GetValue<string> ("SMTP:Senha");
                int porta = _configuration.GetValue<int> ("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(email); //email do usuario passado como parametro
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // envio do email
                using (SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.Credentials = new NetworkCredential(username, senha);
                    smtp.EnableSsl = true; // envio de email seguro

                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}