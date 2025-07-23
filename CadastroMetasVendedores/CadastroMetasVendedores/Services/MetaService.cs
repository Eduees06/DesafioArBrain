using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using CadastroMetasVendedores.Services.Interfaces;

namespace CadastroMetasVendedores.Services
{
    public class MetaService : IMetaService
    {
        private readonly IMetaRepository _metaRepository;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IProdutoRepository _produtoRepository;

        public MetaService(IMetaRepository metaRepository, IVendedorRepository vendedorRepository,
            IProdutoRepository produtoRepository)
        {
            _metaRepository = metaRepository ?? throw new ArgumentNullException(nameof(metaRepository));
            _vendedorRepository = vendedorRepository ?? throw new ArgumentNullException(nameof(vendedorRepository));
            _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
        }

        public int CriarMeta(Meta meta)
        {
            if (meta == null)
                throw new ArgumentNullException(nameof(meta));

            if (!ValidarMeta(meta, out string mensagemErro))
                throw new InvalidOperationException(mensagemErro);

            return _metaRepository.Insert(meta);
        }

        public bool AtualizarMeta(Meta meta)
        {
            if (meta == null)
                throw new ArgumentNullException(nameof(meta));

            if (!_metaRepository.Exists(meta.Id))
                throw new InvalidOperationException("Meta não encontrada para atualização.");

            if (!ValidarMeta(meta, out string mensagemErro))
                throw new InvalidOperationException(mensagemErro);

            return _metaRepository.Update(meta);
        }

        public bool ExcluirMeta(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.", nameof(id));

            if (!_metaRepository.Exists(id))
                return false;

            return _metaRepository.Delete(id);
        }

        public Meta ObterMetaPorId(int id)
        {
            if (id <= 0)
                return null;

            return _metaRepository.GetById(id);
        }

        public IEnumerable<Meta> ObterTodasMetas()
        {
            return _metaRepository.GetAll();
        }

        public IEnumerable<Meta> ObterMetasAtivas()
        {
            return _metaRepository.GetActive();
        }

        public bool ValidarMeta(Meta meta, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            // Valida se o vendedor existe e está ativo
            var vendedor = _vendedorRepository.GetById(meta.VendedorId);
            if (vendedor == null)
            {
                mensagemErro = "Vendedor não encontrado.";
                return false;
            }

            if (!vendedor.Ativo)
            {
                mensagemErro = "Não é possível criar meta para vendedor inativo.";
                return false;
            }

            // Valida se o produto existe e está ativo
            var produto = _produtoRepository.GetById(meta.ProdutoId);
            if (produto == null)
            {
                mensagemErro = "Produto não encontrado.";
                return false;
            }

            if (!produto.Ativo)
            {
                mensagemErro = "Não é possível criar meta para produto inativo.";
                return false;
            }

            // Valida tipo de meta x produto
            if (!ValidarTipoMetaProduto(meta.TipoMeta, meta.ProdutoId, out mensagemErro))
                return false;

            // Valida valor da meta
            if (meta.Valor <= 0)
            {
                mensagemErro = "O valor da meta deve ser maior que zero.";
                return false;
            }

            // Verifica se já existe meta duplicada
            if (VerificarMetaDuplicada(meta.VendedorId, meta.ProdutoId, meta.TipoMeta,
                meta.Periodicidade, meta.Id))
            {
                mensagemErro = $"Já existe uma meta {ObterDescricaoTipoMeta(meta.TipoMeta)} " +
                              $"{ObterDescricaoPeriodicidade(meta.Periodicidade).ToLower()} " +
                              $"para o vendedor {vendedor.Nome} e produto {produto.Nome}.";
                return false;
            }

            return true;
        }

        public bool ValidarTipoMetaProduto(TipoMeta tipoMeta, int produtoId, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            var produto = _produtoRepository.GetById(produtoId);
            if (produto == null)
            {
                mensagemErro = "Produto não encontrado.";
                return false;
            }

            // Metas em litros só podem ser aplicadas a produtos líquidos (Barris e Garrafas/Latas)
            if (tipoMeta == TipoMeta.Litros && !produto.AceitaMetaLitros())
            {
                mensagemErro = "Metas em litros só podem ser aplicadas a produtos líquidos " +
                              "(Barris, Garrafas e Latas).";
                return false;
            }

            return true;
        }

        public bool VerificarMetaDuplicada(int vendedorId, int produtoId, TipoMeta tipoMeta,
            PeriodicidadeMeta periodicidade, int excludeId = 0)
        {
            return _metaRepository.ExisteMetaDuplicada(vendedorId, produtoId, tipoMeta,
                periodicidade, excludeId);
        }

        public Meta DuplicarMeta(int metaId)
        {
            var metaOriginal = _metaRepository.GetById(metaId);
            if (metaOriginal == null)
                throw new InvalidOperationException("Meta não encontrada para duplicação.");

            return _metaRepository.DuplicarMeta(metaId);
        }

        public bool AtivarInativarMeta(int id, bool ativar)
        {
            if (!_metaRepository.Exists(id))
                return false;

            return _metaRepository.ActivateDeactivate(id, ativar);
        }

        public IEnumerable<Meta> BuscarMetas(string filtroVendedor = null, int? produtoId = null,
            TipoMeta? tipoMeta = null, PeriodicidadeMeta? periodicidade = null, bool? ativo = null)
        {
            var metas = _metaRepository.SearchByFilter(filtroVendedor, produtoId, tipoMeta, periodicidade);

            if (ativo.HasValue)
            {
                metas = metas.Where(m => m.Ativo == ativo.Value);
            }

            return metas;
        }

        public IEnumerable<Meta> ObterMetasPorVendedor(int vendedorId)
        {
            return _metaRepository.GetByVendedor(vendedorId);
        }

        public IEnumerable<Meta> ObterMetasPorProduto(int produtoId)
        {
            return _metaRepository.GetByProduto(produtoId);
        }

        public IEnumerable<Meta> ObterMetasPorTipo(TipoMeta tipoMeta)
        {
            return _metaRepository.GetByTipoMeta(tipoMeta);
        }

        public IEnumerable<Meta> ObterMetasPorPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            return _metaRepository.GetByPeriodicidade(periodicidade);
        }

        public bool ValidarCamposObrigatorios(int vendedorId, int produtoId, TipoMeta tipoMeta,
            decimal valor, PeriodicidadeMeta periodicidade, out List<string> camposInvalidos)
        {
            camposInvalidos = new List<string>();

            if (vendedorId <= 0)
                camposInvalidos.Add("Vendedor");

            if (produtoId <= 0)
                camposInvalidos.Add("Produto");

            if (!Enum.IsDefined(typeof(TipoMeta), tipoMeta))
                camposInvalidos.Add("Tipo de Meta");

            if (valor <= 0)
                camposInvalidos.Add("Valor");

            if (!Enum.IsDefined(typeof(PeriodicidadeMeta), periodicidade))
                camposInvalidos.Add("Periodicidade");

            return camposInvalidos.Count == 0;
        }

        public string FormatarValorMeta(decimal valor, TipoMeta tipoMeta)
        {
            switch (tipoMeta)
            {
                case TipoMeta.Monetario:
                    return valor.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));

                case TipoMeta.Litros:
                    return $"{valor:N2} L";

                case TipoMeta.Unidades:
                    return $"{valor:N0} UN";

                default:
                    return valor.ToString("N2");
            }
        }

        // Métodos auxiliares para obter descrições
        private string ObterDescricaoTipoMeta(TipoMeta tipoMeta)
        {
            switch (tipoMeta)
            {
                case TipoMeta.Monetario:
                    return "monetária";
                case TipoMeta.Litros:
                    return "em litros";
                case TipoMeta.Unidades:
                    return "em unidades";
                default:
                    return "desconhecida";
            }
        }

        private string ObterDescricaoPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            switch (periodicidade)
            {
                case PeriodicidadeMeta.Diaria:
                    return "Diária";
                case PeriodicidadeMeta.Semanal:
                    return "Semanal";
                case PeriodicidadeMeta.Mensal:
                    return "Mensal";
                default:
                    return "Desconhecida";
            }
        }
    }
}