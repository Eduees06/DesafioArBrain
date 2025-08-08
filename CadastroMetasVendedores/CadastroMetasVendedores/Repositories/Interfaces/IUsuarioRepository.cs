using CadastroMetasVendedores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMetasVendedores.Repositories.Interfaces
{
    internal interface IUsuarioRepository
    {
        Usuario GetByUsuarioESenha(string usuario, string senha);
    }
}
