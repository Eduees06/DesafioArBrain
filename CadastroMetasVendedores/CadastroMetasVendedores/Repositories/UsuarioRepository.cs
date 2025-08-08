using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace CadastroMetasVendedores.Repositories
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString =
        "Data Source=DESKTOP-I82247C\\SQLEXPRESS;Initial Catalog=MinhaBaseDeDados;Integrated Security=True;Encrypt=False";

        public Usuario GetByUsuarioESenha(string Usuario, string Senha)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Usuario WHERE Usuario = @Usuario AND Senha = @Senha";
                var parameters = new { Usuario, Senha };
                return connection.QueryFirstOrDefault<Usuario>(query, parameters);
            }
        }
    }
}
