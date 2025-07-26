using System;
using System.Collections.Generic;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;

namespace CadastroMetasVendedores.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        // Lista em memória para simulação (substitua por conexão com banco)
        private static List<Produto> _produtos = new List<Produto>();
        private static int _nextId = 1;

        static ProdutoRepository()
        {
            // Dados iniciais fake para testes
            CarregarDadosIniciais();
        }

        private static void CarregarDadosIniciais()
        {
            _produtos = new List<Produto>
            {
                // Barris
                new Produto { Id = _nextId++, Nome = "Barril 50L Pilsen", TipoProduto = TipoProduto.Barris,
                    PrecoUnitario = 250.00m, UnidadeMedida = "L" },
                new Produto { Id = _nextId++, Nome = "Barril 30L IPA", TipoProduto = TipoProduto.Barris,
                    PrecoUnitario = 180.00m, UnidadeMedida = "L" },
                
                // Garrafas e Latas
                new Produto { Id = _nextId++, Nome = "Cerveja Long Neck 355ml", TipoProduto = TipoProduto.GarrafasLatas,
                    PrecoUnitario = 4.50m, UnidadeMedida = "UN" },
                new Produto { Id = _nextId++, Nome = "Lata 350ml Pilsen", TipoProduto = TipoProduto.GarrafasLatas,
                    PrecoUnitario = 3.20m, UnidadeMedida = "UN" },
                new Produto { Id = _nextId++, Nome = "Garrafa 600ml Weiss", TipoProduto = TipoProduto.GarrafasLatas,
                    PrecoUnitario = 6.80m, UnidadeMedida = "UN" },
                
                // Acessórios e Produtos
                new Produto { Id = _nextId++, Nome = "Copo Personalizado 300ml", TipoProduto = TipoProduto.AcessoriosProdutos,
                    PrecoUnitario = 12.00m, UnidadeMedida = "UN" },
                new Produto { Id = _nextId++, Nome = "Abridor de Garrafa", TipoProduto = TipoProduto.AcessoriosProdutos,
                    PrecoUnitario = 15.00m, UnidadeMedida = "UN" },
                new Produto { Id = _nextId++, Nome = "Bolacha de Chopp", TipoProduto = TipoProduto.AcessoriosProdutos,
                    PrecoUnitario = 2.50m, UnidadeMedida = "UN" }
            };
        }

        public int Insert(Produto entity)
        {
            entity.Id = _nextId++;
            entity.DataCadastro = DateTime.Now;
            _produtos.Add(entity);
            return entity.Id;
        }

        public bool Update(Produto entity)
        {
            var produto = GetById(entity.Id);
            if (produto == null) return false;

            produto.Nome = entity.Nome;
            produto.TipoProduto = entity.TipoProduto;
            produto.PrecoUnitario = entity.PrecoUnitario;
            produto.UnidadeMedida = entity.UnidadeMedida;
            produto.Ativo = entity.Ativo;

            return true;
        }

        public bool Delete(int id)
        {
            var produto = GetById(id);
            if (produto == null) return false;

            return _produtos.Remove(produto);
        }

        public Produto GetById(int id)
        {
            return _produtos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Produto> GetAll()
        {
            return _produtos.OrderBy(p => p.Nome);
        }

        public IEnumerable<Produto> GetActive()
        {
            return _produtos.Where(p => p.Ativo).OrderBy(p => p.Nome);
        }

        public bool Exists(int id)
        {
            return _produtos.Any(p => p.Id == id);
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            var produto = GetById(id);
            if (produto == null) return false;

            produto.Ativo = activate;
            return true;
        }

        public bool ExistsByNome(string nome)
        {
            return _produtos.Any(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExistsByNome(string nome, int excludeId)
        {
            return _produtos.Any(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) && p.Id != excludeId);
        }

        public IEnumerable<Produto> GetByTipo(TipoProduto tipo)
        {
            return _produtos.Where(p => p.TipoProduto == tipo && p.Ativo).OrderBy(p => p.Nome);
        }

        public IEnumerable<Produto> GetProdutosQueAceitamMetaLitros()
        {
            return _produtos.Where(p => p.AceitaMetaLitros() && p.Ativo).OrderBy(p => p.Nome);
        }

        public IEnumerable<Produto> SearchByFilter(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return GetAll();

            var filtroUpper = filtro.ToUpper();
            return _produtos.Where(p =>
                p.Nome.ToUpper().Contains(filtroUpper))
                .OrderBy(p => p.Nome);
        }
    }
}