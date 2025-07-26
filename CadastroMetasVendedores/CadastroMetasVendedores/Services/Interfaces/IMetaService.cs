using System.Collections.Generic;
using CadastroMetasVendedores.Models;

namespace CadastroMetasVendedores.Services.Interfaces
{
    public interface IMetaService
    {
        // Operações básicas CRUD
        int CriarMeta(Meta meta);
        bool AtualizarMeta(Meta meta);
        bool ExcluirMeta(int id);
        Meta ObterMetaPorId(int id);
        IEnumerable<Meta> ObterTodasMetas();
        IEnumerable<Meta> ObterMetasAtivas();

        // Operações específicas de negócio
        bool ValidarMeta(Meta meta, out string mensagemErro);
        bool ValidarTipoMetaProduto(TipoMeta tipoMeta, int produtoId, out string mensagemErro);
        bool VerificarMetaDuplicada(int vendedorId, int produtoId, TipoMeta tipoMeta,
            PeriodicidadeMeta periodicidade, int excludeId = 0);

        // Duplicação de meta
        Meta DuplicarMeta(int metaId);

        // Ativação/Inativação
        bool AtivarInativarMeta(int id, bool ativar);

        // Consultas filtradas
        IEnumerable<Meta> BuscarMetas(string filtroVendedor = null, int? produtoId = null,
            TipoMeta? tipoMeta = null, PeriodicidadeMeta? periodicidade = null, bool? ativo = null);

        IEnumerable<Meta> ObterMetasPorVendedor(int vendedorId);
        IEnumerable<Meta> ObterMetasPorProduto(int produtoId);
        IEnumerable<Meta> ObterMetasPorTipo(TipoMeta tipoMeta);
        IEnumerable<Meta> ObterMetasPorPeriodicidade(PeriodicidadeMeta periodicidade);

        // Validações de entrada - assinatura corrigida
        bool ValidarCamposObrigatorios(string nome, int vendedorId, int produtoId, TipoMeta tipoMeta,
            decimal valor, PeriodicidadeMeta periodicidade, out List<string> camposInvalidos);

        // Formatação de valor para exibição
        string FormatarValorMeta(decimal valor, TipoMeta tipoMeta);
    }
}