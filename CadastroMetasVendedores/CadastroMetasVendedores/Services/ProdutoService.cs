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

        public bool ValidarProduto(Produto produto, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            // Valida nome
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                mensagemErro = "Erro: Nome do produto obrigatório.\nDetalhe: O campo nome deve ser preenchido.\nDica: Digite um nome descritivo para o produto.";
                return false;
            }

            // Valida aspas simples no nome
            if (ContemAspasSimples(produto.Nome))
            {
                mensagemErro = "Erro: Caractere inválido no nome.\nDetalhe: O nome do produto não pode conter aspas simples (').\nDica: Remova as aspas simples do nome.";
                return false;
            }

            // Valida aspas simples na unidade de medida
            if (!string.IsNullOrEmpty(produto.UnidadeMedida) && ContemAspasSimples(produto.UnidadeMedida))
            {
                mensagemErro = "Erro: Caractere inválido na unidade.\nDetalhe: A unidade de medida não pode conter aspas simples (').\nDica: Remova as aspas simples da unidade de medida.";
                return false;
            }

            // Verifica se já existe produto com o mesmo nome
            if (_produtoRepository.ExistsByNome(produto.Nome, produto.Id))
            {
                mensagemErro = "Erro: Nome já cadastrado.\nDetalhe: Já existe um produto com este nome.\nDica: Verifique se o produto já não está cadastrado ou use um nome diferente.";
                return false;
            }

            // Valida preço unitário
            if (produto.PrecoUnitario < 0)
            {
                mensagemErro = "Erro: Preço inválido.\nDetalhe: O preço unitário não pode ser negativo.\nDica: Digite um valor igual ou maior que zero.";
                return false;
            }

            return true;
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