using CadastroMetasVendedores.Repositories;
using CadastroMetasVendedores.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroMetasVendedores.Forms
{
    public partial class Login : Form
    {
        private string usuario;
        private string senha;

        public Login()
        {
            InitializeComponent();
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            usuario = textBoxUsuario.Text.Trim();
            senha = textBoxSenha.Text.Trim();

            try
            {
                // Inicializar o repositório e service de usuário
                var usuarioRepository = new UsuarioRepository();
                var usuarioService = new UsuarioService(usuarioRepository);

                // Tenta autenticar
                var usuarioLogado = usuarioService.Login(usuario, senha);

                if (usuarioLogado != null)
                {
                    // Inicializar os outros repositórios
                    var vendedorRepository = new VendedorRepository();
                    var produtoRepository = new ProdutoRepository();
                    var metaRepository = new MetaRepository();

                    // Inicializar os serviços
                    var metaService = new MetaService(metaRepository, vendedorRepository, produtoRepository);
                    var vendedorService = new VendedorService(vendedorRepository);
                    var produtoService = new ProdutoService(produtoRepository);

                    // Abrir tela principal
                    var form = new VisualizacaoMetasForm(metaService, vendedorService, produtoService);
                    this.Hide();
                    form.ShowDialog();
                    this.Show(); // opcional, se quiser voltar ao login quando fechar
                }
                else
                {
                    labelLoginIncorreto.Text = "Usuário ou senha incorretos. Tente novamente.";
                    textBoxUsuario.ResetText();
                    textBoxSenha.ResetText();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar a aplicação: {ex.Message}",
                    "Erro de Inicialização",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogin_MouseEnter_1(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.FromArgb(23, 250, 28);
        }



        private void buttonLogin_MouseLeave_1(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.FromArgb(23, 181, 28);
        }        
        private void buttonSair_MouseLeave(object sender, EventArgs e)
        {
            buttonSair.BackColor = Color.FromArgb(255, 72, 72);
        }

        private void buttonSair_MouseEnter(object sender, EventArgs e)
        {
            buttonSair.BackColor = Color.FromArgb(255, 40, 40);
        }
    }
}
