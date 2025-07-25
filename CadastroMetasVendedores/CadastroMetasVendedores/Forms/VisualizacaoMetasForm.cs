using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;
using System.IO;

namespace CadastroMetasVendedores.Forms
{
    public partial class VisualizacaoMetasForm : Form
    {
        private readonly MetaService _metaService;
        private readonly VendedorService _vendedorService;
        private readonly ProdutoService _produtoService;

        private DataGridView dgvMetas;
        private Panel pnlBackground;
        private Button btnExcluir;
        private Button btnBuscar;
        private Button btnEditar;
        private Button btnAdicionar;
        private Button btnVoltar;
        private CheckBox chkMostrarInativos;
        private Label lblTotalRegistros;
        private Label lblMensagemVazia;
        private TextBox txtBusca;
        private ComboBox cmbTipoBusca;
        private PictureBox picLogo;

        public VisualizacaoMetasForm(MetaService metaService, VendedorService vendedorService, ProdutoService produtoService)
        {
            _metaService = metaService;
            _vendedorService = vendedorService;
            _produtoService = produtoService;

            InitializeComponent(); // Este chama o método do Designer
            InitializeCustomComponents(); // Este chama seu método customizado
            CarregarDados();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();

            // Configurações do Form
            this.Text = "Visualização de Metas";
            this.Size = new Size(1200, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 720);
            this.MaximumSize = new Size(1200, 720); // Fixa o tamanho
            this.Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BackColor = Color.FromArgb(51, 67, 85); // #334355

            // Panel de fundo para a tabela
            pnlBackground = new Panel();
            pnlBackground.Location = new Point(8, 80);
            pnlBackground.Size = new Size(1184, 480);
            pnlBackground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlBackground.BorderStyle = BorderStyle.FixedSingle;
            pnlBackground.BackColor = Color.FromArgb(244, 244, 244); // #F4F4F4
            pnlBackground.Paint += PnlBackground_Paint; // Para desenhar borda personalizada

            // DataGridView
            dgvMetas = new DataGridView();
            dgvMetas.Location = new Point(5, 0);
            dgvMetas.Size = new Size(1174, 480);
            dgvMetas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvMetas.BorderStyle = BorderStyle.FixedSingle;
            dgvMetas.AllowUserToAddRows = false;
            dgvMetas.AllowUserToDeleteRows = false;
            dgvMetas.ReadOnly = true;
            dgvMetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMetas.MultiSelect = false;
            dgvMetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMetas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 67, 85); // #334355
            dgvMetas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMetas.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            dgvMetas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 244, 244);
            dgvMetas.DefaultCellStyle.BackColor = Color.White;
            dgvMetas.RowHeadersVisible = false;
            dgvMetas.Font = new Font("Montserrat", 9F);
            dgvMetas.CellDoubleClick += DgvMetas_CellDoubleClick;
            dgvMetas.GridColor = Color.FromArgb(255, 197, 36); // #ffc524
            dgvMetas.ColumnHeadersHeight = 35;

            pnlBackground.Controls.Add(dgvMetas);

            // Label para mensagem quando lista estiver vazia
            lblMensagemVazia = new Label();
            lblMensagemVazia.Text = "Nenhuma meta encontrada.";
            lblMensagemVazia.Font = new Font("Montserrat", 12F, FontStyle.Regular);
            lblMensagemVazia.ForeColor = Color.Gray;
            lblMensagemVazia.TextAlign = ContentAlignment.MiddleCenter;
            lblMensagemVazia.Dock = DockStyle.Fill;
            lblMensagemVazia.Visible = false;
            pnlBackground.Controls.Add(lblMensagemVazia);

            // Verificar se os ícones existem (debug)
            VerificarIcones();

            // Botões com estilos personalizados - abaixo do painel
            int buttonY = 580; // Posicionado abaixo do painel
            int buttonWidth = 100;
            int buttonHeight = 40;
            int buttonSpacing = 20; // Espaçamento entre botões
            int totalButtonsWidth = (buttonWidth * 5) + (buttonSpacing * 4); // 5 botões + 4 espaços
            int startX = (1200 - totalButtonsWidth) / 2; // Centraliza na tela de 1200px

            // Botão Excluir
            btnExcluir = CreateStyledButton("Excluir", Color.FromArgb(255, 72, 72), Color.White, Color.Black);
            btnExcluir.Location = new Point(startX, buttonY);
            btnExcluir.Size = new Size(buttonWidth, buttonHeight);
            btnExcluir.Click += BtnExcluir_Click;
            SetButtonIcon(btnExcluir, "icone_excluir.png", ContentAlignment.MiddleRight);

            // Botão Buscar
            btnBuscar = CreateStyledButton("Buscar", Color.DarkCyan, Color.White, Color.Black);
            btnBuscar.Location = new Point(startX + buttonWidth + buttonSpacing, buttonY);
            btnBuscar.Size = new Size(buttonWidth, buttonHeight);
            btnBuscar.Click += BtnBuscar_Click;
            SetButtonIcon(btnBuscar, "icone_buscar.png", ContentAlignment.MiddleRight);

            // Botão Editar
            btnEditar = CreateStyledButton("Editar", Color.FromArgb(51, 67, 85), Color.White, Color.White);
            btnEditar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 2, buttonY);
            btnEditar.Size = new Size(buttonWidth, buttonHeight);
            btnEditar.Click += BtnEditar_Click;
            SetButtonIcon(btnEditar, "icone_editar.png", ContentAlignment.MiddleRight);

            // Botão Adicionar
            btnAdicionar = CreateStyledButton("Adicionar", Color.FromArgb(23, 181, 28), Color.White, Color.Black);
            btnAdicionar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 3, buttonY);
            btnAdicionar.Size = new Size(buttonWidth, buttonHeight);
            btnAdicionar.Click += BtnAdicionar_Click;
            SetButtonIcon(btnAdicionar, "icone_adicionar.png", ContentAlignment.MiddleRight);

            // Botão Voltar
            btnVoltar = CreateStyledButton("Voltar", Color.FromArgb(51, 67, 85), Color.White, Color.White);
            btnVoltar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 4, buttonY);
            btnVoltar.Size = new Size(buttonWidth, buttonHeight);
            btnVoltar.Click += BtnVoltar_Click;
            SetButtonIcon(btnVoltar, "icone_voltar.png", ContentAlignment.MiddleLeft);

            // Checkbox para mostrar inativos
            chkMostrarInativos = new CheckBox();
            chkMostrarInativos.Text = "Mostrar inativos";
            chkMostrarInativos.Location = new Point(750, 35);
            chkMostrarInativos.Size = new Size(150, 20);
            chkMostrarInativos.Font = new Font("Montserrat", 9F);
            chkMostrarInativos.ForeColor = Color.Gray;
            chkMostrarInativos.CheckedChanged += ChkMostrarInativos_CheckedChanged;

            // Campos de busca
            Label lblBusca = new Label();
            lblBusca.Text = "Buscar por:";
            lblBusca.Location = new Point(920, 15);
            lblBusca.Size = new Size(80, 15);
            lblBusca.Font = new Font("Montserrat", 9F);
            lblBusca.ForeColor = Color.Gray;

            cmbTipoBusca = new ComboBox();
            cmbTipoBusca.Location = new Point(920, 35);
            cmbTipoBusca.Size = new Size(120, 21);
            cmbTipoBusca.Font = new Font("Montserrat", 9F);
            cmbTipoBusca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoBusca.Items.AddRange(new object[] { "Vendedor", "Produto", "Tipo Meta" });
            cmbTipoBusca.SelectedIndex = 0;
            cmbTipoBusca.BackColor = Color.White;

            txtBusca = new TextBox();
            txtBusca.Location = new Point(1050, 35);
            txtBusca.Size = new Size(130, 21);
            txtBusca.Font = new Font("Montserrat", 9F);
            txtBusca.KeyPress += TxtBusca_KeyPress;
            txtBusca.BackColor = Color.White;

            // Label para total de registros
            lblTotalRegistros = new Label();
            lblTotalRegistros.Location = new Point(8, 640);
            lblTotalRegistros.Size = new Size(300, 20);
            lblTotalRegistros.Font = new Font("Montserrat", 9F);
            lblTotalRegistros.ForeColor = Color.Gray;

            // Logo da empresa
            picLogo = new PictureBox();
            picLogo.Location = new Point(950, 630);
            picLogo.Size = new Size(240, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            LoadLogo();

            // Adicionar controles ao form
            this.Controls.AddRange(new Control[] {
                pnlBackground,
                btnExcluir,
                btnBuscar,
                btnEditar,
                btnAdicionar,
                btnVoltar,
                chkMostrarInativos,
                lblBusca,
                cmbTipoBusca,
                txtBusca,
                lblTotalRegistros,
                picLogo
            });

            this.ResumeLayout(false);
        }

        private void VerificarIcones()
        {
            try
            {
                string[] possibleBasePaths = {
                    Application.StartupPath,
                    Directory.GetCurrentDirectory(),
                    AppDomain.CurrentDomain.BaseDirectory,
                    Environment.CurrentDirectory
                };

                foreach (string basePath in possibleBasePaths)
                {
                    string iconsPath = Path.Combine(basePath, "Assets", "Icons");
                    System.Diagnostics.Debug.WriteLine($"Verificando: {iconsPath}");

                    if (Directory.Exists(iconsPath))
                    {
                        System.Diagnostics.Debug.WriteLine("Diretório Assets/Icons encontrado!");
                        string[] files = Directory.GetFiles(iconsPath, "*.png");
                        System.Diagnostics.Debug.WriteLine($"Arquivos PNG encontrados: {files.Length}");
                        foreach (string file in files)
                        {
                            System.Diagnostics.Debug.WriteLine($"  - {Path.GetFileName(file)}");
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao verificar ícones: {ex.Message}");
            }
        }

        private Button CreateStyledButton(string text, Color backgroundColor, Color textColor, Color borderColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = backgroundColor;
            btn.ForeColor = textColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = borderColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            btn.UseVisualStyleBackColor = false;
            btn.Cursor = Cursors.Hand;

            // Efeito hover
            btn.MouseEnter += (s, e) => {
                btn.BackColor = Color.FromArgb(Math.Min(255, backgroundColor.R + 20),
                                             Math.Min(255, backgroundColor.G + 20),
                                             Math.Min(255, backgroundColor.B + 20));
            };
            btn.MouseLeave += (s, e) => {
                btn.BackColor = backgroundColor;
            };

            return btn;
        }

        private void SetButtonIcon(Button button, string iconResourceName, ContentAlignment alignment)
        {
            try
            {
                // Tenta carregar do Resources primeiro
                Image icon = null;

                switch (iconResourceName)
                {
                    case "icone_buscar.png":
                        try { icon = Properties.Resources.icone_buscar; } catch { }
                        break;
                    case "icone_adicionar.png":
                        try { icon = Properties.Resources.icone_adicionar; } catch { }
                        break;
                    case "icone_voltar.png":
                        try { icon = Properties.Resources.icone_voltar; } catch { }
                        break;
                    case "icone_excluir.png":
                        try { icon = Properties.Resources.icone_excluir; } catch { }
                        break;
                    case "icone_editar.png":
                        try { icon = Properties.Resources.icone_editar; } catch { }
                        break;
                }

                // Se não encontrou no Resources, tenta nos arquivos
                if (icon == null)
                {
                    string[] possiblePaths = {
                        Path.Combine(Application.StartupPath, "Assets", "Icons", iconResourceName),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Icons", iconResourceName),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", iconResourceName),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "Icons", iconResourceName)
                    };

                    foreach (string path in possiblePaths)
                    {
                        if (File.Exists(path))
                        {
                            icon = Image.FromFile(path);
                            break;
                        }
                    }
                }

                if (icon != null)
                {
                    // Redimensiona o ícone
                    Image resizedIcon = new Bitmap(icon, new Size(16, 16));
                    button.Image = resizedIcon;
                    button.ImageAlign = alignment;

                    // Ajusta o espaçamento entre texto e ícone
                    if (alignment == ContentAlignment.MiddleLeft)
                    {
                        button.TextImageRelation = TextImageRelation.ImageBeforeText;
                        button.Padding = new Padding(5, 0, 0, 0);
                    }
                    else
                    {
                        button.TextImageRelation = TextImageRelation.TextBeforeImage;
                        button.Padding = new Padding(0, 0, 5, 0);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Ícone não encontrado: {iconResourceName}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar ícone {iconResourceName}: {ex.Message}");
            }
        }

        private void LoadLogo()
        {
            try
            {
                // Tenta carregar do Resources primeiro
                Image logo = null;
                try
                {
                    logo = Properties.Resources.VectorArBrain;
                }
                catch { }

                // Se não encontrou no Resources, tenta nos arquivos
                if (logo == null)
                {
                    string[] possiblePaths = {
                        Path.Combine(Application.StartupPath, "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "Icons", "VectorArBrain.png")
                    };

                    foreach (string path in possiblePaths)
                    {
                        if (File.Exists(path))
                        {
                            logo = Image.FromFile(path);
                            break;
                        }
                    }
                }

                if (logo != null)
                {
                    picLogo.Image = logo;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Logo não encontrada: VectorArBrain.png");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar logo: {ex.Message}");
            }
        }

        private void PnlBackground_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(255, 197, 36), 2)) // #ffc524
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void CarregarDados()
        {
            try
            {
                var metas = chkMostrarInativos.Checked ?
                    _metaService.ObterTodasMetas() :
                    _metaService.ObterMetasAtivas();

                CarregarGrid(metas);
                AtualizarTotalRegistros(metas.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarGrid(System.Collections.Generic.IEnumerable<Meta> metas)
        {
            dgvMetas.DataSource = null;

            if (!metas.Any())
            {
                lblMensagemVazia.Visible = true;
                dgvMetas.Visible = false;
                return;
            }

            lblMensagemVazia.Visible = false;
            dgvMetas.Visible = true;

            var dadosGrid = metas.Select(m => new
            {
                Id = m.Id,
                Vendedor = m.Vendedor?.Nome ?? "N/A",
                Produto = m.Produto?.Nome ?? "N/A",
                TipoMeta = ObterDescricaoTipoMeta(m.TipoMeta),
                Valor = _metaService.FormatarValorMeta(m.Valor, m.TipoMeta),
                Periodicidade = ObterDescricaoPeriodicidade(m.Periodicidade),
                DataCriacao = m.DataCriacao.ToString("dd/MM/yyyy"),
                Status = m.Ativo ? "Ativo" : "Inativo"
            }).ToList();

            dgvMetas.DataSource = dadosGrid;

            // Configurar colunas
            if (dgvMetas.Columns["Id"] != null)
                dgvMetas.Columns["Id"].Visible = false;

            if (dgvMetas.Columns["Vendedor"] != null)
            {
                dgvMetas.Columns["Vendedor"].HeaderText = "Vendedor";
                dgvMetas.Columns["Vendedor"].Width = 150;
            }

            if (dgvMetas.Columns["Produto"] != null)
            {
                dgvMetas.Columns["Produto"].HeaderText = "Produto";
                dgvMetas.Columns["Produto"].Width = 150;
            }

            if (dgvMetas.Columns["TipoMeta"] != null)
            {
                dgvMetas.Columns["TipoMeta"].HeaderText = "Tipo";
                dgvMetas.Columns["TipoMeta"].Width = 100;
            }

            if (dgvMetas.Columns["Valor"] != null)
            {
                dgvMetas.Columns["Valor"].HeaderText = "Valor";
                dgvMetas.Columns["Valor"].Width = 120;
            }

            if (dgvMetas.Columns["Periodicidade"] != null)
            {
                dgvMetas.Columns["Periodicidade"].HeaderText = "Periodicidade";
                dgvMetas.Columns["Periodicidade"].Width = 100;
            }

            if (dgvMetas.Columns["DataCriacao"] != null)
            {
                dgvMetas.Columns["DataCriacao"].HeaderText = "Data Criação";
                dgvMetas.Columns["DataCriacao"].Width = 100;
            }

            if (dgvMetas.Columns["Status"] != null)
            {
                dgvMetas.Columns["Status"].HeaderText = "Status";
                dgvMetas.Columns["Status"].Width = 80;
            }

            // Colorir linhas inativas
            foreach (DataGridViewRow row in dgvMetas.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Inativo")
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void AtualizarTotalRegistros(int total)
        {
            string texto = chkMostrarInativos.Checked ?
                $"Total de registros: {total}" :
                $"Total de registros ativos: {total}";

            lblTotalRegistros.Text = texto;
        }

        private string ObterDescricaoTipoMeta(TipoMeta tipoMeta)
        {
            switch (tipoMeta)
            {
                case TipoMeta.Monetario: return "Monetário";
                case TipoMeta.Litros: return "Litros";
                case TipoMeta.Unidades: return "Unidades";
                default: return "Desconhecido";
            }
        }

        private string ObterDescricaoPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            switch (periodicidade)
            {
                case PeriodicidadeMeta.Diaria: return "Diária";
                case PeriodicidadeMeta.Semanal: return "Semanal";
                case PeriodicidadeMeta.Mensal: return "Mensal";
                default: return "Desconhecida";
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new CadastroMetaForm(_metaService, _vendedorService, _produtoService))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir formulário de cadastro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int metaId = Convert.ToInt32(dgvMetas.SelectedRows[0].Cells["Id"].Value);
                using (var form = new CadastroMetaForm(_metaService, _vendedorService, _produtoService, metaId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir formulário de edição: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para excluir.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int metaId = Convert.ToInt32(dgvMetas.SelectedRows[0].Cells["Id"].Value);
                string vendedor = dgvMetas.SelectedRows[0].Cells["Vendedor"].Value?.ToString();
                string produto = dgvMetas.SelectedRows[0].Cells["Produto"].Value?.ToString();

                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir a meta do vendedor '{vendedor}' para o produto '{produto}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool sucesso = _metaService.ExcluirMeta(metaId);

                    if (sucesso)
                    {
                        MessageBox.Show("Meta excluída com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarDados();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir meta.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir meta: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusca();
        }

        private void RealizarBusca()
        {
            try
            {
                string filtro = txtBusca.Text.Trim();
                string tipoBusca = cmbTipoBusca.SelectedItem?.ToString();

                System.Collections.Generic.IEnumerable<Meta> metas;

                if (string.IsNullOrEmpty(filtro))
                {
                    metas = chkMostrarInativos.Checked ?
                        _metaService.ObterTodasMetas() :
                        _metaService.ObterMetasAtivas();
                }
                else
                {
                    switch (tipoBusca)
                    {
                        case "Vendedor":
                            metas = _metaService.BuscarMetas(filtro, null, null, null, !chkMostrarInativos.Checked ? true : (bool?)null);
                            break;
                        case "Produto":
                            // Para busca por produto, precisaria de uma implementação específica
                            metas = _metaService.ObterTodasMetas().Where(m =>
                                m.Produto != null &&
                                m.Produto.Nome.ToUpper().Contains(filtro.ToUpper()));
                            break;
                        case "Tipo Meta":
                            TipoMeta? tipoMeta = null;
                            if (filtro.ToUpper().Contains("MONETÁRIO") || filtro.ToUpper().Contains("MONETARIO"))
                                tipoMeta = TipoMeta.Monetario;
                            else if (filtro.ToUpper().Contains("LITROS"))
                                tipoMeta = TipoMeta.Litros;
                            else if (filtro.ToUpper().Contains("UNIDADES"))
                                tipoMeta = TipoMeta.Unidades;

                            metas = _metaService.BuscarMetas(null, null, tipoMeta, null, !chkMostrarInativos.Checked ? true : (bool?)null);
                            break;
                        default:
                            metas = chkMostrarInativos.Checked ?
                                _metaService.ObterTodasMetas() :
                                _metaService.ObterMetasAtivas();
                            break;
                    }

                    if (!chkMostrarInativos.Checked)
                    {
                        metas = metas.Where(m => m.Ativo);
                    }
                }

                CarregarGrid(metas);
                AtualizarTotalRegistros(metas.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar busca: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChkMostrarInativos_CheckedChanged(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void TxtBusca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RealizarBusca();
            }
        }

        private void DgvMetas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditar_Click(sender, e);
            }
        }
    }
}