using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Repositories.Interfaces
{
    public interface IMetaRepository : IRepository<Meta>
    {
        // Métodos específicos para Meta
        IEnumerable<Meta> GetByVendedor(int vendedorId);
        IEnumerable<Meta> GetByProduto(int produtoId);
        IEnumerable<Meta> GetByTipoMeta(TipoMeta tipoMeta);
        IEnumerable<Meta> GetByPeriodicidade(PeriodicidadeMeta periodicidade);
        IEnumerable<Meta> GetMetasAtivas();
        IEnumerable<Meta> SearchByFilter(string filtroVendedor = null, int? produtoId = null,
            TipoMeta? tipoMeta = null, PeriodicidadeMeta? periodicidade = null);
        bool ExisteMetaDuplicada(int vendedorId, int produtoId, TipoMeta tipoMeta,
            PeriodicidadeMeta periodicidade, int excludeId = 0);
        Meta DuplicarMeta(int metaId);
    }
}