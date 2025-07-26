using System;

namespace CadastroMetasVendedores.Models
{
    public class Meta
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int VendedorId { get; set; }

        public int ProdutoId { get; set; }

        public TipoMeta TipoMeta { get; set; }

        public decimal Valor { get; set; }

        public PeriodicidadeMeta Periodicidade { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Ativo { get; set; }

        // Propriedades de navegação
        public Vendedor Vendedor { get; set; }

        public Produto Produto { get; set; }

        public Meta()
        {
            DataCriacao = DateTime.Now;
            Ativo = true;
        }
    }

    public enum TipoMeta
    {
        Monetario = 1,  // R$
        Litros = 2,     // L
        Unidades = 3    // UN
    }

    public enum PeriodicidadeMeta
    {
        Diaria = 1,
        Semanal = 2,
        Mensal = 3
    }
}