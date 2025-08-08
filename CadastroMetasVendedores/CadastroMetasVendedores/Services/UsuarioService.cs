using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroMetasVendedores.Services
{
    internal class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Login(string usuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Usuário e senha são obrigatórios");

            return _usuarioRepository.GetByUsuarioESenha(usuario, senha);
        }
    }
}
