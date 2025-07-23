using System;
using System.Collections.Generic;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;

namespace CadastroMetasVendedores.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        // Lista em memória para simulação
        private static List<Vendedor> _vendedores = new List<Vendedor>();
        private static int _nextId = 1;

        static VendedorRepository()
        {
            // Dados iniciais fake para testes
            CarregarDadosIniciais();
        }

        private static void CarregarDadosIniciais()
        {
            _vendedores = new List<Vendedor>
            {
                new Vendedor { Id = _nextId++, Nome = "João Silva", Codigo = "V001", Email = "joao@email.com", Telefone = "(11) 99999-0001" },
                new Vendedor { Id = _nextId++, Nome = "Maria Santos", Codigo = "V002", Email = "maria@email.com", Telefone = "(11) 99999-0002" },
                new Vendedor { Id = _nextId++, Nome = "Pedro Costa", Codigo = "V003", Email = "pedro@email.com", Telefone = "(11) 99999-0003" },
                new Vendedor { Id = _nextId++, Nome = "Ana Oliveira", Codigo = "V004", Email = "ana@email.com", Telefone = "(11) 99999-0004" }
            };
        }

        public int Insert(Vendedor entity)
        {
            entity.Id = _nextId++;
            entity.DataCadastro = DateTime.Now;
            _vendedores.Add(entity);
            return entity.Id;
        }

        public bool Update(Vendedor entity)
        {
            var vendedor = GetById(entity.Id);
            if (vendedor == null) return false;

            vendedor.Nome = entity.Nome;
            vendedor.Codigo = entity.Codigo;
            vendedor.Email = entity.Email;
            vendedor.Telefone = entity.Telefone;
            vendedor.Ativo = entity.Ativo;

            return true;
        }

        public bool Delete(int id)
        {
            var vendedor = GetById(id);
            if (vendedor == null) return false;

            return _vendedores.Remove(vendedor);
        }

        public Vendedor GetById(int id)
        {
            return _vendedores.FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<Vendedor> GetAll()
        {
            return _vendedores.OrderBy(v => v.Nome);
        }

        public IEnumerable<Vendedor> GetActive()
        {
            return _vendedores.Where(v => v.Ativo).OrderBy(v => v.Nome);
        }

        public bool Exists(int id)
        {
            return _vendedores.Any(v => v.Id == id);
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            var vendedor = GetById(id);
            if (vendedor == null) return false;

            vendedor.Ativo = activate;
            return true;
        }

        public bool ExistsByNome(string nome)
        {
            return _vendedores.Any(v => v.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsByNome(string nome, int excludeId)
        {
            return _vendedores.Any(v => v.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && v.Id != excludeId);
        }

        public Vendedor GetByCodigo(string codigo)
        {
            return _vendedores.FirstOrDefault(v => v.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Vendedor> GetByNome(string nome)
        {
            return _vendedores.Where(v => v.Nome.ToUpper().Contains(nome.ToUpper()));
        }

        public IEnumerable<Vendedor> SearchByFilter(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return GetAll();

            var filtroUpper = filtro.ToUpper();
            return _vendedores.Where(v =>
                v.Nome.ToUpper().Contains(filtroUpper) ||
                v.Codigo.ToUpper().Contains(filtroUpper) ||
                v.Email.ToUpper().Contains(filtroUpper))
                .OrderBy(v => v.Nome);
        }

        public bool ExistsByCodigo(string codigo)
        {
            return _vendedores.Any(v => v.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsByCodigo(string codigo, int excludeId)
        {
            return _vendedores.Any(v => v.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && v.Id != excludeId);
        }
    }
}