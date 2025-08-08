using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using Dapper;

namespace CadastroMetasVendedores.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        private readonly string _connectionString =
            "Data Source=DESKTOP-I82247C\\SQLEXPRESS;Initial Catalog=MinhaBaseDeDados;Integrated Security=True;Encrypt=False";

        private const string BaseSelect = @"
            SELECT 
                m.Id, m.Nome, m.VendedorId, m.ProdutoId, m.TipoMeta, m.Valor, m.Periodicidade, m.DataCriacao, m.Ativo,
                v.Id, v.Nome, v.Email, v.Telefone, v.DataCadastro, v.Ativo,
                p.Id, p.Nome, p.TipoProduto, p.PrecoUnitario, p.UnidadeMedida, p.DataCadastro, p.Ativo
            FROM Meta m
            INNER JOIN Vendedor v ON m.VendedorId = v.Id
            INNER JOIN Produto p ON m.ProdutoId = p.Id
        ";

        public int Insert(Meta entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Meta 
                        (Nome, VendedorId, ProdutoId, TipoMeta, Valor, Periodicidade, DataCriacao, Ativo)
                    VALUES 
                        (@Nome, @VendedorId, @ProdutoId, @TipoMeta, @Valor, @Periodicidade, @DataCriacao, @Ativo);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                entity.DataCriacao = DateTime.Now;
                return connection.QuerySingle<int>(sql, entity);
            }
        }

        public bool Update(Meta entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE Meta
                    SET Nome = @Nome,
                        VendedorId = @VendedorId,
                        ProdutoId = @ProdutoId,
                        TipoMeta = @TipoMeta,
                        Valor = @Valor,
                        Periodicidade = @Periodicidade,
                        Ativo = @Ativo
                    WHERE Id = @Id";

                return connection.Execute(sql, entity) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("DELETE FROM Meta WHERE Id = @Id", new { Id = id }) > 0;
            }
        }

        public Meta GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.Id = @Id";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    new { Id = id },
                    splitOn: "Id,Id"
                ).FirstOrDefault();
            }
        }

        public IEnumerable<Meta> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    splitOn: "Id,Id"
                );
            }
        }

        public IEnumerable<Meta> GetActive()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.Ativo = 1 ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    splitOn: "Id,Id"
                );
            }
        }

        public bool Exists(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Meta WHERE Id = @Id", new { Id = id }) > 0;
            }
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(
                    "UPDATE Meta SET Ativo = @Ativo WHERE Id = @Id",
                    new { Id = id, Ativo = activate }) > 0;
            }
        }

        public IEnumerable<Meta> GetByVendedor(int vendedorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.VendedorId = @VendedorId ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    new { VendedorId = vendedorId },
                    splitOn: "Id,Id"
                );
            }
        }

        public IEnumerable<Meta> GetByProduto(int produtoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.ProdutoId = @ProdutoId ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    new { ProdutoId = produtoId },
                    splitOn: "Id,Id"
                );
            }
        }

        public IEnumerable<Meta> GetByTipoMeta(TipoMeta tipoMeta)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.TipoMeta = @TipoMeta ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    new { TipoMeta = (int)tipoMeta },
                    splitOn: "Id,Id"
                );
            }
        }

        public IEnumerable<Meta> GetByPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE m.Periodicidade = @Periodicidade ORDER BY m.DataCriacao DESC";
                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    new { Periodicidade = (int)periodicidade },
                    splitOn: "Id,Id"
                );
            }
        }

        public IEnumerable<Meta> SearchByFilter(string filtroVendedor = null, int? produtoId = null,
            TipoMeta? tipoMeta = null, PeriodicidadeMeta? periodicidade = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = BaseSelect + " WHERE 1=1 ";
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(filtroVendedor))
                {
                    sql += " AND v.Nome LIKE @FiltroVendedor";
                    parameters.Add("@FiltroVendedor", $"%{filtroVendedor}%");
                }

                if (produtoId.HasValue)
                {
                    sql += " AND m.ProdutoId = @ProdutoId";
                    parameters.Add("@ProdutoId", produtoId.Value);
                }

                if (tipoMeta.HasValue)
                {
                    sql += " AND m.TipoMeta = @TipoMeta";
                    parameters.Add("@TipoMeta", (int)tipoMeta.Value);
                }

                if (periodicidade.HasValue)
                {
                    sql += " AND m.Periodicidade = @Periodicidade";
                    parameters.Add("@Periodicidade", (int)periodicidade.Value);
                }

                sql += " ORDER BY m.DataCriacao DESC";

                return connection.Query<Meta, Vendedor, Produto, Meta>(
                    sql,
                    (meta, vendedor, produto) =>
                    {
                        meta.Vendedor = vendedor;
                        meta.Produto = produto;
                        return meta;
                    },
                    parameters,
                    splitOn: "Id,Id"
                );
            }
        }

        public bool ExisteMetaDuplicada(int vendedorId, int produtoId, TipoMeta tipoMeta,
            PeriodicidadeMeta periodicidade, int excludeId = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT COUNT(1) 
                    FROM Meta
                    WHERE VendedorId = @VendedorId
                      AND ProdutoId = @ProdutoId
                      AND TipoMeta = @TipoMeta
                      AND Periodicidade = @Periodicidade
                      AND Id <> @ExcludeId
                      AND Ativo = 1";

                return connection.ExecuteScalar<int>(query, new
                {
                    VendedorId = vendedorId,
                    ProdutoId = produtoId,
                    TipoMeta = (int)tipoMeta,
                    Periodicidade = (int)periodicidade,
                    ExcludeId = excludeId
                }) > 0;
            }
        }

        public bool ExisteMetaPorNome(string nome, int excludeId = 0)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT COUNT(1) 
                    FROM Meta
                    WHERE Nome = @Nome
                      AND Id <> @ExcludeId";

                return connection.ExecuteScalar<int>(query, new
                {
                    Nome = nome,
                    ExcludeId = excludeId
                }) > 0;
            }
        }

        public Meta DuplicarMeta(int metaId)
        {
            var metaOriginal = GetById(metaId);
            if (metaOriginal == null) return null;

            var novaMeta = new Meta
            {
                Nome = $"{metaOriginal.Nome} - Cópia",
                VendedorId = metaOriginal.VendedorId,
                ProdutoId = metaOriginal.ProdutoId,
                TipoMeta = metaOriginal.TipoMeta,
                Valor = metaOriginal.Valor,
                Periodicidade = metaOriginal.Periodicidade,
                Ativo = metaOriginal.Ativo
            };

            var novoId = Insert(novaMeta);
            return GetById(novoId);
        }

        public IEnumerable<Meta> GetMetasAtivas()
        {
            return GetActive();
        }
    }
}
