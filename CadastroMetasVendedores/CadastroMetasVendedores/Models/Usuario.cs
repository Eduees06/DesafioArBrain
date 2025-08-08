using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMetasVendedores.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string User { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Adm { get; set; }
    }
}
