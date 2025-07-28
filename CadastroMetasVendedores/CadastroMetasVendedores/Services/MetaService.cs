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
                throw new InvalidOperationException("Erro: Meta não encontrada.\nDetalhe: A meta que você está tentando atualizar não existe no sistema.\nDica: Verifique se a meta não foi removida por outro usuário e tente novamente.");

            if (!ValidarMeta(meta, out string mensagemErro))
                throw new InvalidOperationException(mensagemErro);

            return _metaRepository.Update(meta);
        }

        public bool ExcluirMeta(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Erro: ID inválido.\nDetalhe: O identificador da meta deve ser um número maior que zero.\nDica: Verifique o ID da meta selecionada.", nameof(id));

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

            // Valida nome da metaDuplicada
            if (string.IsNullOrWhiteSpace(meta.Nome))
            {
                mensagemErro = "Erro: Nome da meta obrigatório.\nDetalhe: O campo nome da meta deve ser preenchido.\nDica: Digite um nome descritivo para identificar a meta.";
                return false;
            }

            // Valida aspas simples
            if (ContemAspasSimples(meta.Nome))
            {
                mensagemErro = "Erro: Caractere inválido no nome.\nDetalhe: O nome da meta não pode conter aspas simples (').\nDica: Remova as aspas simples do nome da meta.";
                return false;
            }

            // Verifica se já existe uma metaDuplicada com o mesmo nome
            if (_metaRepository.ExisteMetaPorNome(meta.Nome, meta.Id))
            {
                mensagemErro = "Erro: Nome da meta já existe.\nDetalhe: Já existe uma meta cadastrada com este nome.\nDica: Escolha um nome diferente para a meta ou verifique se você não está duplicando uma meta existente.";
                return false;
            }

            // Valida se o vendedor existe e está ativo
            var vendedor = _vendedorRepository.GetById(meta.VendedorId);
            if (vendedor == null)
            {
                mensagemErro = "Erro: Vendedor não encontrado.\nDetalhe: O vendedor selecionado não existe no sistema.\nDica: Selecione um vendedor válido da lista ou verifique se o vendedor não foi removido.";
                return false;
            }

            // Valida tipo de metaDuplicada x produto
            if (!ValidarTipoMetaProduto(meta.TipoMeta, meta.ProdutoId, out mensagemErro))
                return false;

            // Valida valor da metaDuplicada
            if (meta.Valor <= 0)
            {
                mensagemErro = "Erro: Valor inválido.\nDetalhe: O valor da meta deve ser maior que zero.\nDica: Digite um valor positivo para a meta.";
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
                mensagemErro = "Erro: Produto não encontrado.\nDetalhe: O produto selecionado não existe no sistema.\nDica: Selecione um produto válido da lista.";
                return false;
            }

            // Metas em litros só podem ser aplicadas a produtos líquidos (Barris e Garrafas/Latas)
            if (tipoMeta == TipoMeta.Litros && !produto.AceitaMetaLitros())
            {
                mensagemErro = "Erro: Tipo de meta incompatível.\nDetalhe: Metas em litros só podem ser aplicadas a produtos líquidos " +
                              "(Barris, Garrafas e Latas).\nDica: Selecione o tipo 'Monetário' ou 'Unidades' para este produto.";
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
            _ = _metaRepository.GetById(metaId) ?? throw new InvalidOperationException("Erro: Meta não encontrada.\nDetalhe: A meta que você está tentando duplicar não existe.\nDica: Verifique se a meta não foi removida e tente novamente.");
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

        public bool ValidarCamposObrigatorios(string nome, int vendedorId, int produtoId, TipoMeta tipoMeta,
            decimal valor, PeriodicidadeMeta periodicidade, out List<string> camposInvalidos)
        {
            camposInvalidos = new List<string>();

            if (string.IsNullOrWhiteSpace(nome))
                camposInvalidos.Add("Nome da Meta");

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

        // Método para validar aspas simples
        private bool ContemAspasSimples(string texto)
        {
            return !string.IsNullOrEmpty(texto) && texto.Contains("'");
        }
    }
}