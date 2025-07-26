using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Services.Interfaces
{
    public interface IProdutoService
    {
        // Operações básicas
        IEnumerable<Produto> ObterTodosProdutos();
        IEnumerable<Produto> ObterProdutosAtivos();
        Produto ObterProdutoPorId(int id);

        // Busca e filtros
        IEnumerable<Produto> ObterProdutosPorTipo(TipoProduto tipo);
        IEnumerable<Produto> ObterProdutosQueAceitamMetaLitros();

        // Validações específicas para metas
        bool ProdutoAceitaMetaLitros(int produtoId);
        IEnumerable<Produto> ObterProdutosValidosParaTipoMeta(TipoMeta tipoMeta);

        // Formatação e exibição
        string FormatarNomeProduto(Produto produto);
        string ObterDescricaoTipoProduto(TipoProduto tipo);
        Dictionary<int, string> ObterProdutosParaComboBox();
        Dictionary<int, string> ObterProdutosPorTipoParaComboBox(TipoMeta tipoMeta);
    }
}