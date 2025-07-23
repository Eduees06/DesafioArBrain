using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Services.Interfaces
{
    public interface IVendedorService
    {
        // Operações básicas
        IEnumerable<Vendedor> ObterTodosVendedores();
        IEnumerable<Vendedor> ObterVendedoresAtivos();
        Vendedor ObterVendedorPorId(int id);
        Vendedor ObterVendedorPorCodigo(string codigo);

        // Busca e filtros
        IEnumerable<Vendedor> ObterVendedoresPorNome(string nome);

        // Formatação e exibição
        string FormatarNomeVendedor(Vendedor vendedor);
        Dictionary<int, string> ObterVendedoresParaComboBox();
    }
}