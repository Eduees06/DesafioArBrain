using System;
using System.Collections.Generic;

namespace CadastroMetasVendedores.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public TipoProduto TipoProduto { get; set; }

        public decimal PrecoUnitario { get; set; }

        public string UnidadeMedida { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        // Lista de metas associadas ao produto
        public List<Meta> Metas { get; set; }

        public Produto()
        {
            DataCadastro = DateTime.Now;
            Ativo = true;
            Metas = new List<Meta>();
        }

        // Método para verificar se o produto aceita meta em litros
        public bool AceitaMetaLitros()
        {
            return TipoProduto == TipoProduto.Barris ||
                   TipoProduto == TipoProduto.GarrafasLatas;
        }
    }

    public enum TipoProduto
    {
        Barris = 1,
        GarrafasLatas = 2,
        AcessoriosProdutos = 3
    }
}