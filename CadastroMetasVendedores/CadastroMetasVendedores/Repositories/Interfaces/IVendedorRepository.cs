using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Repositories.Interfaces
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        // Métodos específicos para Vendedor
        bool ExistsByNome(string nome);
        bool ExistsByNome(string nome, int excludeId);
        IEnumerable<Vendedor> GetByNome(string nome);
        IEnumerable<Vendedor> SearchByFilter(string filtro);
    }
}