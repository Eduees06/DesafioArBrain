using System;
using System.Collections.Generic;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using CadastroMetasVendedores.Services.Interfaces;

namespace CadastroMetasVendedores.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;

        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository ?? throw new ArgumentNullException(nameof(vendedorRepository));
        }

        public IEnumerable<Vendedor> ObterTodosVendedores()
        {
            return _vendedorRepository.GetAll();
        }

        public IEnumerable<Vendedor> ObterVendedoresAtivos()
        {
            return _vendedorRepository.GetActive();
        }

        public Vendedor ObterVendedorPorId(int id)
        {
            if (id <= 0)
                return null;

            return _vendedorRepository.GetById(id);
        }

        public IEnumerable<Vendedor> ObterVendedoresPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return new List<Vendedor>();

            return _vendedorRepository.GetByNome(nome);
        }

        public bool ValidarVendedor(Vendedor vendedor, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            // Valida nome
            if (string.IsNullOrWhiteSpace(vendedor.Nome))
            {
                mensagemErro = "Erro: Nome do vendedor obrigatório.\nDetalhe: O campo nome deve ser preenchido.\nDica: Digite o nome completo do vendedor.";
                return false;
            }

            // Valida aspas simples no nome
            if (ContemAspasSimples(vendedor.Nome))
            {
                mensagemErro = "Erro: Caractere inválido no nome.\nDetalhe: O nome do vendedor não pode conter aspas simples (').\nDica: Remova as aspas simples do nome.";
                return false;
            }

            // Valida aspas simples no email
            if (!string.IsNullOrEmpty(vendedor.Email) && ContemAspasSimples(vendedor.Email))
            {
                mensagemErro = "Erro: Caractere inválido no email.\nDetalhe: O email não pode conter aspas simples (').\nDica: Remova as aspas simples do email.";
                return false;
            }

            // Valida aspas simples no telefone
            if (!string.IsNullOrEmpty(vendedor.Telefone) && ContemAspasSimples(vendedor.Telefone))
            {
                mensagemErro = "Erro: Caractere inválido no telefone.\nDetalhe: O telefone não pode conter aspas simples (').\nDica: Remova as aspas simples do telefone.";
                return false;
            }

            // Verifica se já existe vendedor com o mesmo nome
            if (_vendedorRepository.ExistsByNome(vendedor.Nome, vendedor.Id))
            {
                mensagemErro = "Erro: Nome já cadastrado.\nDetalhe: Já existe um vendedor com este nome.\nDica: Verifique se o vendedor já não está cadastrado ou use um nome diferente.";
                return false;
            }

            return true;
        }

        public string FormatarNomeVendedor(Vendedor vendedor)
        {
            if (vendedor == null)
                return string.Empty;

            return vendedor.Nome;
        }

        public Dictionary<int, string> ObterVendedoresParaComboBox()
        {
            var vendedores = ObterVendedoresAtivos();
            var dicionario = new Dictionary<int, string>();

            foreach (var vendedor in vendedores)
            {
                dicionario.Add(vendedor.Id, FormatarNomeVendedor(vendedor));
            }

            return dicionario;
        }

        // Método para validar aspas simples
        private bool ContemAspasSimples(string texto)
        {
            return !string.IsNullOrEmpty(texto) && texto.Contains("'");
        }
    }
}