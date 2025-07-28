using System;
using System.Collections.Generic;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using CadastroMetasVendedores.Services.Interfaces;

namespace CadastroMetasVendedores.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            return _produtoRepository.GetAll();
        }

        public IEnumerable<Produto> ObterProdutosAtivos()
        {
            return _produtoRepository.GetActive();
        }

        public Produto ObterProdutoPorId(int id)
        {
            if (id <= 0)
                return null;

            return _produtoRepository.GetById(id);
        }

        public IEnumerable<Produto> ObterProdutosPorTipo(TipoProduto tipo)
        {
            return _produtoRepository.GetByTipo(tipo);
        }

        public IEnumerable<Produto> ObterProdutosQueAceitamMetaLitros()
        {
            return _produtoRepository.GetProdutosQueAceitamMetaLitros();
        }

        public bool ProdutoAceitaMetaLitros(int produtoId)
        {
            var produto = ObterProdutoPorId(produtoId);
            return produto != null && produto.AceitaMetaLitros();
        }

        public string FormatarNomeProduto(Produto produto)
        {
            if (produto == null)
                return string.Empty;

            return produto.Nome;
        }

        public string ObterDescricaoTipoProduto(TipoProduto tipo)
        {
            switch (tipo)
            {
                case TipoProduto.Barris:
                    return "Barris";
                case TipoProduto.GarrafasLatas:
                    return "Garrafas e Latas";
                case TipoProduto.AcessoriosProdutos:
                    return "Acessórios e Produtos";
                default:
                    return "Desconhecido";
            }
        }

        public Dictionary<int, string> ObterProdutosParaComboBox()
        {
            var produtos = ObterProdutosAtivos();
            var dicionario = new Dictionary<int, string>();

            foreach (var produto in produtos)
            {
                dicionario.Add(produto.Id, FormatarNomeProduto(produto));
            }

            return dicionario;
        }

        public Dictionary<int, string> ObterProdutosPorTipoParaComboBox(TipoMeta tipoMeta)
        {
            var produtos = ObterProdutosValidosParaTipoMeta(tipoMeta);
            var dicionario = new Dictionary<int, string>();

            foreach (var produto in produtos)
            {
                dicionario.Add(produto.Id, FormatarNomeProduto(produto));
            }

            return dicionario;
        }

        public IEnumerable<Produto> ObterProdutosValidosParaTipoMeta(TipoMeta tipoMeta)
        {
            var produtos = ObterProdutosAtivos();

            // Se for meta em litros, retorna apenas produtos que aceitam meta em litros
            if (tipoMeta == TipoMeta.Litros)
            {
                return produtos.Where(p => p.AceitaMetaLitros());
            }

            // Para outros tipos de meta (Monetário e Unidades), todos os produtos são válidos
            return produtos;
        }

        // Método para validar aspas simples
        private bool ContemAspasSimples(string texto)
        {
            return !string.IsNullOrEmpty(texto) && texto.Contains("'");
        }
    }
}