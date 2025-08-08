using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using Dapper;

namespace CadastroMetasVendedores.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _connectionString =
            "Data Source=DESKTOP-I82247C\\SQLEXPRESS;Initial Catalog=MinhaBaseDeDados;Integrated Security=True;Encrypt=False";

        public int Insert(Produto entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Produto (Nome, TipoProduto, PrecoUnitario, UnidadeMedida, DataCadastro, Ativo)
                    VALUES (@Nome, @TipoProduto, @PrecoUnitario, @UnidadeMedida, @DataCadastro, @Ativo);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                entity.DataCadastro = DateTime.Now;
                return connection.QuerySingle<int>(sql, entity);
            }
        }

        public bool Update(Produto entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE Produto
                    SET Nome = @Nome,
                        TipoProduto = @TipoProduto,
                        PrecoUnitario = @PrecoUnitario,
                        UnidadeMedida = @UnidadeMedida,
                        Ativo = @Ativo
                    WHERE Id = @Id";

                return connection.Execute(sql, entity) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "DELETE FROM Produto WHERE Id = @Id";
                return connection.Execute(sql, new { Id = id }) > 0;
            }
        }

        public Produto GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Produto>(
                    "SELECT * FROM Produto WHERE Id = @Id", new { Id = id });
            }
        }

        public IEnumerable<Produto> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>("SELECT * FROM Produto ORDER BY Nome");
            }
        }

        public IEnumerable<Produto> GetActive()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(
                    "SELECT * FROM Produto WHERE Ativo = 1 ORDER BY Nome");
            }
        }

        public bool Exists(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Produto WHERE Id = @Id", new { Id = id }) > 0;
            }
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(
                    "UPDATE Produto SET Ativo = @Ativo WHERE Id = @Id",
                    new { Id = id, Ativo = activate }) > 0;
            }
        }

        public bool ExistsByNome(string nome)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Produto WHERE Nome = @Nome", new { Nome = nome }) > 0;
            }
        }

        public bool ExistsByNome(string nome, int excludeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Produto WHERE Nome = @Nome AND Id <> @ExcludeId",
                    new { Nome = nome, ExcludeId = excludeId }) > 0;
            }
        }

        public IEnumerable<Produto> GetByTipo(TipoProduto tipo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(
                    "SELECT * FROM Produto WHERE TipoProduto = @Tipo AND Ativo = 1 ORDER BY Nome",
                    new { Tipo = (int)tipo });
            }
        }

        public IEnumerable<Produto> GetProdutosQueAceitamMetaLitros()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto>(
                    @"SELECT * FROM Produto 
                      WHERE (TipoProduto = @Barris OR TipoProduto = @GarrafasLatas)
                      AND Ativo = 1
                      ORDER BY Nome",
                    new { Barris = (int)TipoProduto.Barris, GarrafasLatas = (int)TipoProduto.GarrafasLatas });
            }
        }

        public IEnumerable<Produto> SearchByFilter(string filtro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (string.IsNullOrWhiteSpace(filtro))
                    return GetAll();

                return connection.Query<Produto>(
                    "SELECT * FROM Produto WHERE UPPER(Nome) LIKE @Filtro ORDER BY Nome",
                    new { Filtro = $"%{filtro.ToUpper()}%" });
            }
        }
    }
}
