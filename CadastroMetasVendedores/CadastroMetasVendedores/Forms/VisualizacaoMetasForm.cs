using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;

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
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // Panel de fundo para a tabela
            pnlBackground = new Panel();
            pnlBackground.Location = new Point(8, 60);
            pnlBackground.Size = new Size(1184, 580);
            pnlBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlBackground.BorderStyle = BorderStyle.FixedSingle;
            pnlBackground.BackColor = Color.White;

            // DataGridView
            dgvMetas = new DataGridView();
            dgvMetas.Location = new Point(5, 5);
            dgvMetas.Size = new Size(1174, 570);
            dgvMetas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMetas.BorderStyle = BorderStyle.FixedSingle;
            dgvMetas.AllowUserToAddRows = false;
            dgvMetas.AllowUserToDeleteRows = false;
            dgvMetas.ReadOnly = true;
            dgvMetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMetas.MultiSelect = false;
            dgvMetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMetas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dgvMetas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMetas.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            dgvMetas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 244, 244);
            dgvMetas.RowHeadersVisible = false;
            dgvMetas.Font = new Font("Montserrat", 9F);
            dgvMetas.CellDoubleClick += DgvMetas_CellDoubleClick;

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

            // Botões (da esquerda para direita: Excluir, Buscar, Editar, Adicionar, Voltar)
            int buttonY = 15;
            int buttonWidth = 100;
            int buttonHeight = 35;
            int buttonSpacing = 120;
            int startX = 20;

            btnExcluir = new Button();
            btnExcluir.Text = "Excluir";
            btnExcluir.Location = new Point(startX, buttonY);
            btnExcluir.Size = new Size(buttonWidth, buttonHeight);
            btnExcluir.UseVisualStyleBackColor = true;
            btnExcluir.Font = new Font("Montserrat", 9F);
            btnExcluir.Click += BtnExcluir_Click;

            btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(startX + buttonSpacing, buttonY);
            btnBuscar.Size = new Size(buttonWidth, buttonHeight);
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Font = new Font("Montserrat", 9F);
            btnBuscar.Click += BtnBuscar_Click;

            btnEditar = new Button();
            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(startX + (buttonSpacing * 2), buttonY);
            btnEditar.Size = new Size(buttonWidth, buttonHeight);
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Font = new Font("Montserrat", 9F);
            btnEditar.Click += BtnEditar_Click;

            btnAdicionar = new Button();
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.Location = new Point(startX + (buttonSpacing * 3), buttonY);
            btnAdicionar.Size = new Size(buttonWidth, buttonHeight);
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Font = new Font("Montserrat", 9F);
            btnAdicionar.Click += BtnAdicionar_Click;

            btnVoltar = new Button();
            btnVoltar.Text = "Voltar";
            btnVoltar.Location = new Point(startX + (buttonSpacing * 4), buttonY);
            btnVoltar.Size = new Size(buttonWidth, buttonHeight);
            btnVoltar.UseVisualStyleBackColor = true;
            btnVoltar.Font = new Font("Montserrat", 9F);
            btnVoltar.Click += BtnVoltar_Click;

            // Checkbox para mostrar inativos
            chkMostrarInativos = new CheckBox();
            chkMostrarInativos.Text = "Mostrar inativos";
            chkMostrarInativos.Location = new Point(700, 25);
            chkMostrarInativos.Size = new Size(150, 20);
            chkMostrarInativos.Font = new Font("Montserrat", 9F);
            chkMostrarInativos.ForeColor = Color.Gray;
            chkMostrarInativos.CheckedChanged += ChkMostrarInativos_CheckedChanged;

            // Campos de busca
            Label lblBusca = new Label();
            lblBusca.Text = "Buscar por:";
            lblBusca.Location = new Point(870, 8);
            lblBusca.Size = new Size(80, 15);
            lblBusca.Font = new Font("Montserrat", 9F);
            lblBusca.ForeColor = Color.Gray;

            cmbTipoBusca = new ComboBox();
            cmbTipoBusca.Location = new Point(870, 25);
            cmbTipoBusca.Size = new Size(120, 21);
            cmbTipoBusca.Font = new Font("Montserrat", 9F);
            cmbTipoBusca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoBusca.Items.AddRange(new object[] { "Vendedor", "Produto", "Tipo Meta" });
            cmbTipoBusca.SelectedIndex = 0;

            txtBusca = new TextBox();
            txtBusca.Location = new Point(1000, 25);
            txtBusca.Size = new Size(180, 21);
            txtBusca.Font = new Font("Montserrat", 9F);
            txtBusca.KeyPress += TxtBusca_KeyPress;

            // Label para total de registros
            lblTotalRegistros = new Label();
            lblTotalRegistros.Location = new Point(8, 650);
            lblTotalRegistros.Size = new Size(300, 20);
            lblTotalRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotalRegistros.Font = new Font("Montserrat", 9F);
            lblTotalRegistros.ForeColor = Color.Gray;

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
                lblTotalRegistros
            });

            this.ResumeLayout(false);
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