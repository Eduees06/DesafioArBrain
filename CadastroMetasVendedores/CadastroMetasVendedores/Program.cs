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
                var form = new Login();

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