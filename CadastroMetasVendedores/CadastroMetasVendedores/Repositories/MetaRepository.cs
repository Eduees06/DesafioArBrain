using System;
using System.Collections.Generic;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;

namespace CadastroMetasVendedores.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        // Lista em memória para simulação (substitua por conexão com banco)
        private static List<Meta> _metas = new List<Meta>();
        private static int _nextId = 1;

        private readonly IVendedorRepository _vendedorRepository;
        private readonly IProdutoRepository _produtoRepository;

        public MetaRepository(IVendedorRepository vendedorRepository, IProdutoRepository produtoRepository)
        {
            _vendedorRepository = vendedorRepository;
            _produtoRepository = produtoRepository;
        }

        public int Insert(Meta entity)
        {
            entity.Id = _nextId++;
            entity.DataCriacao = DateTime.Now;

            // Carrega as entidades relacionadas
            entity.Vendedor = _vendedorRepository.GetById(entity.VendedorId);
            entity.Produto = _produtoRepository.GetById(entity.ProdutoId);

            _metas.Add(entity);
            return entity.Id;
        }

        public bool Update(Meta entity)
        {
            var meta = GetById(entity.Id);
            if (meta == null) return false;

            meta.VendedorId = entity.VendedorId;
            meta.ProdutoId = entity.ProdutoId;
            meta.TipoMeta = entity.TipoMeta;
            meta.Valor = entity.Valor;
            meta.Periodicidade = entity.Periodicidade;
            meta.Ativo = entity.Ativo;

            // Atualiza as entidades relacionadas
            meta.Vendedor = _vendedorRepository.GetById(entity.VendedorId);
            meta.Produto = _produtoRepository.GetById(entity.ProdutoId);

            return true;
        }

        public bool Delete(int id)
        {
            var meta = GetById(id);
            if (meta == null) return false;

            return _metas.Remove(meta);
        }

        public Meta GetById(int id)
        {
            var meta = _metas.FirstOrDefault(m => m.Id == id);
            if (meta != null)
            {
                // Carrega as entidades relacionadas
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }
            return meta;
        }

        public IEnumerable<Meta> GetAll()
        {
            var metas = _metas.OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public IEnumerable<Meta> GetActive()
        {
            var metas = _metas.Where(m => m.Ativo).OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public bool Exists(int id)
        {
            return _metas.Any(m => m.Id == id);
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            var meta = GetById(id);
            if (meta == null) return false;

            meta.Ativo = activate;
            return true;
        }

        public IEnumerable<Meta> GetByVendedor(int vendedorId)
        {
            var metas = _metas.Where(m => m.VendedorId == vendedorId).OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public IEnumerable<Meta> GetByProduto(int produtoId)
        {
            var metas = _metas.Where(m => m.ProdutoId == produtoId).OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public IEnumerable<Meta> GetByTipoMeta(TipoMeta tipoMeta)
        {
            var metas = _metas.Where(m => m.TipoMeta == tipoMeta).OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public IEnumerable<Meta> GetByPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            var metas = _metas.Where(m => m.Periodicidade == periodicidade).OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public IEnumerable<Meta> GetMetasAtivas()
        {
            return GetActive();
        }

        public IEnumerable<Meta> SearchByFilter(string filtroVendedor = null, int? produtoId = null,
            TipoMeta? tipoMeta = null, PeriodicidadeMeta? periodicidade = null)
        {
            var query = _metas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtroVendedor))
            {
                var vendedoresIds = _vendedorRepository.SearchByFilter(filtroVendedor)
                    .Select(v => v.Id).ToList();
                query = query.Where(m => vendedoresIds.Contains(m.VendedorId));
            }

            if (produtoId.HasValue)
                query = query.Where(m => m.ProdutoId == produtoId.Value);

            if (tipoMeta.HasValue)
                query = query.Where(m => m.TipoMeta == tipoMeta.Value);

            if (periodicidade.HasValue)
                query = query.Where(m => m.Periodicidade == periodicidade.Value);

            var metas = query.OrderByDescending(m => m.DataCriacao).ToList();

            // Carrega as entidades relacionadas
            foreach (var meta in metas)
            {
                meta.Vendedor = _vendedorRepository.GetById(meta.VendedorId);
                meta.Produto = _produtoRepository.GetById(meta.ProdutoId);
            }

            return metas;
        }

        public bool ExisteMetaDuplicada(int vendedorId, int produtoId, TipoMeta tipoMeta,
            PeriodicidadeMeta periodicidade, int excludeId = 0)
        {
            return _metas.Any(m =>
                m.VendedorId == vendedorId &&
                m.ProdutoId == produtoId &&
                m.TipoMeta == tipoMeta &&
                m.Periodicidade == periodicidade &&
                m.Id != excludeId &&
                m.Ativo);
        }

        public Meta DuplicarMeta(int metaId)
        {
            var metaOriginal = GetById(metaId);
            if (metaOriginal == null) return null;

            var novaMeta = new Meta
            {
                VendedorId = metaOriginal.VendedorId,
                ProdutoId = metaOriginal.ProdutoId,
                TipoMeta = metaOriginal.TipoMeta,
                Valor = metaOriginal.Valor,
                Periodicidade = metaOriginal.Periodicidade
            };

            Insert(novaMeta);
            return novaMeta;
        }
    }
}