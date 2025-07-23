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

        public Vendedor ObterVendedorPorCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return null;

            return _vendedorRepository.GetByCodigo(codigo);
        }

        public IEnumerable<Vendedor> ObterVendedoresPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return new List<Vendedor>();

            return _vendedorRepository.GetByNome(nome);
        }

        public string FormatarNomeVendedor(Vendedor vendedor)
        {
            if (vendedor == null)
                return string.Empty;

            return $"{vendedor.Nome} ({vendedor.Codigo})";
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
    }
}