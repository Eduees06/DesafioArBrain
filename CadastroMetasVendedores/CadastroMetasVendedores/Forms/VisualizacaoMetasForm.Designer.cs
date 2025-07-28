using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadastroMetasVendedores.Forms
{
    partial class VisualizacaoMetasForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvMetas;
        private Panel pnlBackground;
        private Button btnExcluir;
        private Button btnBuscar;
        private Button btnEditar;
        private Button btnAdicionar;
        private Button btnVoltar;
        private Button btnDuplicar;
        private Button btnLimparFiltros;
        private Button btnHistorico;
        private Panel pnlToggleSwitch;
        private Label lblToggleText;
        private Panel pnlToggleSlider;
        private Label lblTotalRegistros;
        private Label lblMensagemVazia;
        private TextBox txtBusca;
        private PictureBox picLogo;
        private Label lblBusca;
        private Panel pnlFiltros;
        private FlowLayoutPanel flpFiltrosAtivos;
        private Label lblLegendaFiltros;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvMetas = new System.Windows.Forms.DataGridView();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnDuplicar = new System.Windows.Forms.Button();
            this.btnLimparFiltros = new System.Windows.Forms.Button();
            this.btnHistorico = new System.Windows.Forms.Button();
            this.pnlToggleSwitch = new System.Windows.Forms.Panel();
            this.lblToggleText = new System.Windows.Forms.Label();
            this.pnlToggleSlider = new System.Windows.Forms.Panel();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.lblMensagemVazia = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblBusca = new System.Windows.Forms.Label();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.flpFiltrosAtivos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblLegendaFiltros = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMetas)).BeginInit();
            this.pnlBackground.SuspendLayout();
            this.pnlToggleSwitch.SuspendLayout();
            this.pnlFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // Form
            this.Text = "Visualização de Metas";
            this.Size = new Size(1200, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 720);
            this.Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BackColor = Color.FromArgb(51, 67, 85);
            this.KeyPreview = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;

            // picLogo
            this.picLogo.Location = new Point(15, 15);
            this.picLogo.Size = new Size(80, 80);
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.picLogo.BackColor = Color.Transparent;
            this.picLogo.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // lblBusca
            this.lblBusca.Text = "Buscar:";
            this.lblBusca.Location = new Point(500, 15);
            this.lblBusca.Size = new Size(60, 15);
            this.lblBusca.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            this.lblBusca.ForeColor = Color.White;
            this.lblBusca.BackColor = Color.Transparent;
            this.lblBusca.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // txtBusca
            this.txtBusca.Location = new Point(550, 35);
            this.txtBusca.Size = new Size(250, 21);
            this.txtBusca.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            this.txtBusca.BackColor = Color.White;
            this.txtBusca.BorderStyle = BorderStyle.FixedSingle;
            this.txtBusca.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // lblLegendaFiltros
            this.lblLegendaFiltros.Text = "Filtrável por: Nome, Vendedor, Produto, Tipo, Valor, Periodicidade, Data, Status";
            this.lblLegendaFiltros.Location = new Point(550, 60);
            this.lblLegendaFiltros.Size = new Size(650, 15);
            this.lblLegendaFiltros.Font = new Font("Montserrat", 8F, FontStyle.Italic);
            this.lblLegendaFiltros.ForeColor = Color.LightGray;
            this.lblLegendaFiltros.BackColor = Color.Transparent;
            this.lblLegendaFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // btnLimparFiltros
            this.btnLimparFiltros.Text = "Limpar Filtros";
            this.btnLimparFiltros.Location = new Point(760, 33);
            this.btnLimparFiltros.Size = new Size(100, 25);
            this.btnLimparFiltros.BackColor = Color.FromArgb(51, 67, 85);
            this.btnLimparFiltros.ForeColor = Color.White;
            this.btnLimparFiltros.FlatStyle = FlatStyle.Flat;
            this.btnLimparFiltros.FlatAppearance.BorderColor = Color.White;
            this.btnLimparFiltros.FlatAppearance.BorderSize = 1;
            this.btnLimparFiltros.Font = new Font("Montserrat", 8F, FontStyle.Bold);
            this.btnLimparFiltros.UseVisualStyleBackColor = false;
            this.btnLimparFiltros.Cursor = Cursors.Hand;
            this.btnLimparFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnLimparFiltros.TextAlign = ContentAlignment.MiddleCenter;

            // lblToggleText
            this.lblToggleText.Text = "Exibir Inativos";
            this.lblToggleText.Location = new Point(0, 2);
            this.lblToggleText.Size = new Size(90, 18);
            this.lblToggleText.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            this.lblToggleText.ForeColor = Color.White;
            this.lblToggleText.TextAlign = ContentAlignment.BottomRight;
            this.lblToggleText.BackColor = Color.Transparent;

            // pnlToggleSwitch - Posição fixa em relação ao logo
            this.pnlToggleSwitch.Location = new Point(600, 35);
            this.pnlToggleSwitch.Size = new Size(160, 50);
            this.pnlToggleSwitch.BackColor = Color.Transparent;
            this.pnlToggleSwitch.Controls.Add(this.lblToggleText);
            this.pnlToggleSwitch.Controls.Add(this.pnlToggleSlider);
            this.pnlToggleSwitch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // pnlToggleSlider
            this.pnlToggleSlider.Location = new Point(95, 3);
            this.pnlToggleSlider.Size = new Size(60, 25);
            this.pnlToggleSlider.BackColor = Color.Transparent;
            this.pnlToggleSlider.BorderStyle = BorderStyle.None;
            this.pnlToggleSlider.Cursor = Cursors.Hand;
            this.pnlToggleSlider.Paint += PnlToggleSlider_Paint;
            this.pnlToggleSlider.Click += PnlToggleSlider_Click;

            // pnlFiltros
            this.pnlFiltros.Location = new Point(500, 85);
            this.pnlFiltros.Size = new Size(680, 25);
            this.pnlFiltros.BackColor = Color.Transparent;
            this.pnlFiltros.Controls.Add(this.flpFiltrosAtivos);
            this.pnlFiltros.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // flpFiltrosAtivos
            this.flpFiltrosAtivos.Location = new Point(0, 0);
            this.flpFiltrosAtivos.Size = new Size(680, 25);
            this.flpFiltrosAtivos.BackColor = Color.Transparent;
            this.flpFiltrosAtivos.FlowDirection = FlowDirection.LeftToRight;
            this.flpFiltrosAtivos.WrapContents = false;
            this.flpFiltrosAtivos.AutoScroll = true;

            // pnlBackground
            this.pnlBackground.Location = new Point(8, 115);
            this.pnlBackground.Size = new Size(1184, 450);
            this.pnlBackground.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.pnlBackground.BorderStyle = BorderStyle.FixedSingle;
            this.pnlBackground.BackColor = Color.FromArgb(244, 244, 244);
            this.pnlBackground.Controls.Add(this.dgvMetas);
            this.pnlBackground.Controls.Add(this.lblMensagemVazia);

            // dgvMetas
            this.dgvMetas.Location = new Point(5, 0);
            this.dgvMetas.Size = new Size(1174, 448);
            this.dgvMetas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.dgvMetas.BorderStyle = BorderStyle.FixedSingle;
            this.dgvMetas.AllowUserToAddRows = false;
            this.dgvMetas.AllowUserToDeleteRows = false;
            this.dgvMetas.ReadOnly = true;
            this.dgvMetas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMetas.MultiSelect = false;
            this.dgvMetas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMetas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 67, 85);
            this.dgvMetas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvMetas.ColumnHeadersDefaultCellStyle.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.dgvMetas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 244, 244);
            this.dgvMetas.DefaultCellStyle.BackColor = Color.White;
            this.dgvMetas.DefaultCellStyle.Font = new Font("Montserrat", 9F);
            this.dgvMetas.RowHeadersVisible = false;
            this.dgvMetas.GridColor = Color.FromArgb(255, 197, 36);
            this.dgvMetas.ColumnHeadersHeight = 35;
            this.dgvMetas.AllowUserToOrderColumns = true;
            this.dgvMetas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(71, 87, 105);
            this.dgvMetas.EnableHeadersVisualStyles = false;

            // lblMensagemVazia
            this.lblMensagemVazia.Text = "Nenhuma meta encontrada.";
            this.lblMensagemVazia.Font = new Font("Montserrat", 12F, FontStyle.Regular);
            this.lblMensagemVazia.ForeColor = Color.Gray;
            this.lblMensagemVazia.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMensagemVazia.Dock = DockStyle.Fill;
            this.lblMensagemVazia.Visible = false;
            this.lblMensagemVazia.BackColor = Color.Transparent;

            // lblTotalRegistros
            this.lblTotalRegistros.Location = new Point(8, 580);
            this.lblTotalRegistros.Size = new Size(300, 20);
            this.lblTotalRegistros.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            this.lblTotalRegistros.ForeColor = Color.White;
            this.lblTotalRegistros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblTotalRegistros.BackColor = Color.Transparent;

            // Configuração dos botões - Nova ordem: Excluir, Histórico, Editar, Buscar, Duplicar, Voltar, Adicionar
            int buttonWidth = 150;
            int buttonHeight = 40;
            int buttonSpacing = 30;
            int buttonY = 630;

            // btnExcluir (índice 0)
            this.btnExcluir.Text = "Excluir (Del)";
            this.btnExcluir.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2, buttonY);
            this.btnExcluir.Size = new Size(buttonWidth, buttonHeight);
            this.btnExcluir.BackColor = Color.FromArgb(255, 72, 72);
            this.btnExcluir.ForeColor = Color.White;
            this.btnExcluir.FlatStyle = FlatStyle.Flat;
            this.btnExcluir.FlatAppearance.BorderColor = Color.Black;
            this.btnExcluir.FlatAppearance.BorderSize = 2;
            this.btnExcluir.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Cursor = Cursors.Hand;
            this.btnExcluir.Anchor = AnchorStyles.Bottom;
            this.btnExcluir.TextAlign = ContentAlignment.MiddleCenter;
            this.btnExcluir.ImageAlign = ContentAlignment.MiddleRight;
            this.btnExcluir.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.btnExcluir.Padding = new Padding(0, 0, 5, 0);

            // btnHistorico (índice 1)
            this.btnHistorico.Text = "Histórico";
            this.btnHistorico.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + buttonWidth + buttonSpacing, buttonY);
            this.btnHistorico.Size = new Size(buttonWidth, buttonHeight);
            this.btnHistorico.BackColor = Color.FromArgb(51, 67, 85);
            this.btnHistorico.ForeColor = Color.White;
            this.btnHistorico.FlatStyle = FlatStyle.Flat;
            this.btnHistorico.FlatAppearance.BorderColor = Color.White;
            this.btnHistorico.FlatAppearance.BorderSize = 2;
            this.btnHistorico.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnHistorico.UseVisualStyleBackColor = false;
            this.btnHistorico.Cursor = Cursors.Hand;
            this.btnHistorico.Anchor = AnchorStyles.Bottom;
            this.btnHistorico.TextAlign = ContentAlignment.MiddleCenter;
            this.btnHistorico.ImageAlign = ContentAlignment.MiddleRight;
            this.btnHistorico.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.btnHistorico.Padding = new Padding(0, 0, 5, 0);

            // btnEditar (índice 2)
            this.btnEditar.Text = "Editar (F4)";
            this.btnEditar.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + (buttonWidth + buttonSpacing) * 2, buttonY);
            this.btnEditar.Size = new Size(buttonWidth, buttonHeight);
            this.btnEditar.BackColor = Color.FromArgb(51, 67, 85);
            this.btnEditar.ForeColor = Color.White;
            this.btnEditar.FlatStyle = FlatStyle.Flat;
            this.btnEditar.FlatAppearance.BorderColor = Color.White;
            this.btnEditar.FlatAppearance.BorderSize = 2;
            this.btnEditar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Cursor = Cursors.Hand;
            this.btnEditar.Anchor = AnchorStyles.Bottom;
            this.btnEditar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnEditar.ImageAlign = ContentAlignment.MiddleRight;
            this.btnEditar.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.btnEditar.Padding = new Padding(0, 0, 5, 0);

            // btnBuscar (índice 3)
            this.btnBuscar.Text = "Buscar (F11)";
            this.btnBuscar.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + (buttonWidth + buttonSpacing) * 3, buttonY);
            this.btnBuscar.Size = new Size(buttonWidth, buttonHeight);
            this.btnBuscar.BackColor = Color.DarkCyan;
            this.btnBuscar.ForeColor = Color.White;
            this.btnBuscar.FlatStyle = FlatStyle.Flat;
            this.btnBuscar.FlatAppearance.BorderColor = Color.Black;
            this.btnBuscar.FlatAppearance.BorderSize = 2;
            this.btnBuscar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Cursor = Cursors.Hand;
            this.btnBuscar.Anchor = AnchorStyles.Bottom;
            this.btnBuscar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnBuscar.ImageAlign = ContentAlignment.MiddleCenter;
            this.btnBuscar.TextImageRelation = TextImageRelation.Overlay;
            this.btnBuscar.Padding = new Padding(0, 0, 5, 0);

            // btnDuplicar (índice 4)
            this.btnDuplicar.Text = "Duplicar";
            this.btnDuplicar.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + (buttonWidth + buttonSpacing) * 4, buttonY);
            this.btnDuplicar.Size = new Size(buttonWidth, buttonHeight);
            this.btnDuplicar.BackColor = Color.FromArgb(51, 67, 85);
            this.btnDuplicar.ForeColor = Color.White;
            this.btnDuplicar.FlatStyle = FlatStyle.Flat;
            this.btnDuplicar.FlatAppearance.BorderColor = Color.White;
            this.btnDuplicar.FlatAppearance.BorderSize = 2;
            this.btnDuplicar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnDuplicar.UseVisualStyleBackColor = false;
            this.btnDuplicar.Cursor = Cursors.Hand;
            this.btnDuplicar.Anchor = AnchorStyles.Bottom;
            this.btnDuplicar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnDuplicar.ImageAlign = ContentAlignment.MiddleRight;
            this.btnDuplicar.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.btnDuplicar.Padding = new Padding(0, 0, 5, 0);

            // btnVoltar (índice 5)
            this.btnVoltar.Text = "    Voltar (ESC)";
            this.btnVoltar.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + (buttonWidth + buttonSpacing) * 5, buttonY);
            this.btnVoltar.Size = new Size(buttonWidth, buttonHeight);
            this.btnVoltar.BackColor = Color.FromArgb(51, 67, 85);
            this.btnVoltar.ForeColor = Color.White;
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderColor = Color.White;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Anchor = AnchorStyles.Bottom;
            this.btnVoltar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnVoltar.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnVoltar.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnVoltar.Padding = new Padding(5, 0, 0, 0);

            // btnAdicionar (índice 6)
            this.btnAdicionar.Text = "Adicionar (F2)";
            this.btnAdicionar.Location = new Point((1200 - ((buttonWidth * 7) + (buttonSpacing * 6))) / 2 + (buttonWidth + buttonSpacing) * 6, buttonY);
            this.btnAdicionar.Size = new Size(buttonWidth, buttonHeight);
            this.btnAdicionar.BackColor = Color.FromArgb(23, 181, 28);
            this.btnAdicionar.ForeColor = Color.White;
            this.btnAdicionar.FlatStyle = FlatStyle.Flat;
            this.btnAdicionar.FlatAppearance.BorderColor = Color.Black;
            this.btnAdicionar.FlatAppearance.BorderSize = 2;
            this.btnAdicionar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnAdicionar.UseVisualStyleBackColor = false;
            this.btnAdicionar.Cursor = Cursors.Hand;
            this.btnAdicionar.Anchor = AnchorStyles.Bottom;
            this.btnAdicionar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnAdicionar.ImageAlign = ContentAlignment.MiddleRight;
            this.btnAdicionar.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnAdicionar.Padding = new Padding(0, 0, 5, 0);

            // Eventos
            this.Load += VisualizacaoMetasForm_Load;
            this.Resize += VisualizacaoMetasForm_Resize;

            // VisualizacaoMetasForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 720);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.pnlBackground);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnHistorico);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnDuplicar);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.pnlToggleSwitch);
            this.Controls.Add(this.lblBusca);
            this.Controls.Add(this.txtBusca);
            this.Controls.Add(this.btnLimparFiltros);
            this.Controls.Add(this.lblLegendaFiltros);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.lblTotalRegistros);
            this.Name = "VisualizacaoMetasForm";

            ((System.ComponentModel.ISupportInitialize)(this.dgvMetas)).EndInit();
            this.pnlBackground.ResumeLayout(false);
            this.pnlToggleSwitch.ResumeLayout(false);
            this.pnlFiltros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void VisualizacaoMetasForm_Resize(object sender, EventArgs e)
        {
            RecalcularPosicaoBotoes();
            ReposicionarElementosCabecalho();
        }

        private void VisualizacaoMetasForm_Load(object sender, EventArgs e)
        {
            RecalcularPosicaoBotoes();
            ReposicionarElementosCabecalho();
        }

        private void RecalcularPosicaoBotoes()
        {
            int buttonWidth = 150;
            int buttonSpacing = 40;
            int totalButtonsWidth = (buttonWidth * 7) + (buttonSpacing * 6);
            int startX = (this.ClientSize.Width - totalButtonsWidth) / 2;
            int buttonY = this.ClientSize.Height - 90;

            // Ordem: Excluir, Histórico, Editar, Buscar, Duplicar, Voltar, Adicionar
            btnExcluir.Location = new Point(startX, buttonY);
            btnHistorico.Location = new Point(startX + buttonWidth + buttonSpacing, buttonY);
            btnEditar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 2, buttonY);
            btnBuscar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 3, buttonY);
            btnDuplicar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 4, buttonY);
            btnVoltar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 5, buttonY);
            btnAdicionar.Location = new Point(startX + (buttonWidth + buttonSpacing) * 6, buttonY);

            lblTotalRegistros.Location = new Point(8, buttonY - 50);
        }

        private void ReposicionarElementosCabecalho()
        {
            int rightMargin = 100;

            lblBusca.Location = new Point(this.ClientSize.Width - 375 - rightMargin, 15);
            txtBusca.Location = new Point(this.ClientSize.Width - 370 - rightMargin, 35);
            lblLegendaFiltros.Location = new Point(this.ClientSize.Width - 370 - rightMargin, 60);
            btnLimparFiltros.Location = new Point(this.ClientSize.Width - 60 - rightMargin, 33);
            pnlFiltros.Location = new Point(this.ClientSize.Width - 370 - rightMargin, 85);
        }
    }
}