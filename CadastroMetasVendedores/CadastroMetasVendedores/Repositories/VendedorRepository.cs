using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using Dapper;

namespace CadastroMetasVendedores.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly string _connectionString =
            "Data Source=DESKTOP-I82247C\\SQLEXPRESS;Initial Catalog=MinhaBaseDeDados;Integrated Security=True;Encrypt=False";

        public int Insert(Vendedor entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO Vendedor (Nome, Email, Telefone, DataCadastro, Ativo)
                    VALUES (@Nome, @Email, @Telefone, @DataCadastro, @Ativo);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                entity.DataCadastro = DateTime.Now;
                return connection.QuerySingle<int>(sql, entity);
            }
        }

        public bool Update(Vendedor entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    UPDATE Vendedor
                    SET Nome = @Nome,
                        Email = @Email,
                        Telefone = @Telefone,
                        Ativo = @Ativo
                    WHERE Id = @Id";

                return connection.Execute(sql, entity) > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("DELETE FROM Vendedor WHERE Id = @Id", new { Id = id }) > 0;
            }
        }

        public Vendedor GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Vendedor>(
                    "SELECT * FROM Vendedor WHERE Id = @Id", new { Id = id });
            }
        }

        public IEnumerable<Vendedor> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Vendedor>("SELECT * FROM Vendedor ORDER BY Nome");
            }
        }

        public IEnumerable<Vendedor> GetActive()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Vendedor>(
                    "SELECT * FROM Vendedor WHERE Ativo = 1 ORDER BY Nome");
            }
        }

        public bool Exists(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Vendedor WHERE Id = @Id", new { Id = id }) > 0;
            }
        }

        public bool ActivateDeactivate(int id, bool activate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Execute(
                    "UPDATE Vendedor SET Ativo = @Ativo WHERE Id = @Id",
                    new { Id = id, Ativo = activate }) > 0;
            }
        }

        public bool ExistsByNome(string nome)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Vendedor WHERE Nome = @Nome", new { Nome = nome }) > 0;
            }
        }

        public bool ExistsByNome(string nome, int excludeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Vendedor WHERE Nome = @Nome AND Id <> @ExcludeId",
                    new { Nome = nome, ExcludeId = excludeId }) > 0;
            }
        }

        public IEnumerable<Vendedor> GetByNome(string nome)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Vendedor>(
                    "SELECT * FROM Vendedor WHERE UPPER(Nome) LIKE @Filtro ORDER BY Nome",
                    new { Filtro = $"%{nome.ToUpper()}%" });
            }
        }

        public IEnumerable<Vendedor> SearchByFilter(string filtro)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (string.IsNullOrWhiteSpace(filtro))
                    return GetAll();

                return connection.Query<Vendedor>(
                    @"SELECT * FROM Vendedor 
                      WHERE UPPER(Nome) LIKE @Filtro 
                         OR UPPER(Email) LIKE @Filtro
                      ORDER BY Nome",
                    new { Filtro = $"%{filtro.ToUpper()}%" });
            }
        }
    }
}
