using System.Security.Cryptography;
using System.Text;

namespace ControleDeContatos.Helper
{
    // classe estática (não precisa ser instânciada para ser acessada)
    public static class Criptografia
    {
        //this -> método de extensão da string ao digitar
        public static string GerarHash(this string valor)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(valor); //pega todos os bytes do valor e transformar em array
            
            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}