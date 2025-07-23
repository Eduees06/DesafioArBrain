using System;
using System.Collections.Generic;

namespace CadastroMetasVendedores.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        // Lista de metas associadas ao vendedor
        public List<Meta> Metas { get; set; }

        public Vendedor()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
            Metas = new List<Meta>();
        }
    }
}