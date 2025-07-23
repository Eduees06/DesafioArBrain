using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        // Métodos específicos para Produto
        bool ExistsByNome(string nome);
        bool ExistsByNome(string nome, int excludeId);
        Produto GetByCodigo(string codigo);
        IEnumerable<Produto> GetByTipo(TipoProduto tipo);
        IEnumerable<Produto> GetProdutosQueAceitamMetaLitros();
        IEnumerable<Produto> SearchByFilter(string filtro);
        bool ExistsByCodigo(string codigo);
        bool ExistsByCodigo(string codigo, int excludeId);
    }
}