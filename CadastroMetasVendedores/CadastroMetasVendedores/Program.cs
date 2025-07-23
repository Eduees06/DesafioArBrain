using System;
using System.Windows.Forms;
using CadastroMetasVendedores.Forms;
using CadastroMetasVendedores.Services;
using CadastroMetasVendedores.Repositories;

namespace CadastroMetasVendedores
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Inicializar os repositórios base primeiro (sem dependências circulares)
                var vendedorRepository = new VendedorRepository();
                var produtoRepository = new ProdutoRepository();

                // Inicializar repositório que depende dos outros
                var metaRepository = new MetaRepository(vendedorRepository, produtoRepository);

                // Inicializar os serviços simplificados
                var metaService = new MetaService(metaRepository, vendedorRepository, produtoRepository);
                var vendedorService = new VendedorService(vendedorRepository);
                var produtoService = new ProdutoService(produtoRepository);

                // Criar o formulário de visualização como tela inicial
                var form = new VisualizacaoMetasForm(metaService, vendedorService, produtoService);

                Application.Run(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar a aplicação: {ex.Message}",
                    "Erro de Inicialização",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}