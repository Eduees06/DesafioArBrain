using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadastroMetasVendedores.Forms
{
    partial class HistoricoOperacoesForm1
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvHistorico;
        private Button btnReverter;
        private Button btnVoltar;
        private Label lblTotalOperacoes;

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
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.btnReverter = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.lblTotalOperacoes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.SuspendLayout();

            // 
            // Form
            // 
            this.Text = "Histórico de Operações";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            this.BackColor = Color.FromArgb(51, 67, 85);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.KeyPreview = true;
            this.KeyDown += HistoricoOperacoesForm_KeyDown;

            // 
            // dgvHistorico
            // 
            this.dgvHistorico.Location = new Point(10, 10);
            this.dgvHistorico.Size = new Size(760, 480);
            this.dgvHistorico.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorico.MultiSelect = false;
            this.dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorico.BackgroundColor = Color.White;
            this.dgvHistorico.GridColor = Color.FromArgb(255, 197, 36);
            this.dgvHistorico.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(51, 67, 85),
                ForeColor = Color.White,
                Font = new Font("Montserrat", 9F, FontStyle.Bold)
            };
            this.dgvHistorico.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Montserrat", 9F)
            };
            this.dgvHistorico.RowHeadersVisible = false;
            this.dgvHistorico.ColumnHeadersHeight = 35;

            // Calcular posições dos botões centralizados
            int buttonWidth = 180;
            int buttonHeight = 35;
            int buttonSpacing = 15;
            int totalButtonsWidth = (buttonWidth * 2) + buttonSpacing;
            int startX = (800 - totalButtonsWidth) / 2;
            int buttonY = 510;

            // 
            // btnVoltar
            // 
            this.btnVoltar.Text = "      Voltar (ESC)";
            this.btnVoltar.Location = new Point(startX, buttonY);
            this.btnVoltar.Size = new Size(buttonWidth, buttonHeight);
            this.btnVoltar.BackColor = Color.FromArgb(51, 67, 85);
            this.btnVoltar.ForeColor = Color.White;
            this.btnVoltar.FlatStyle = FlatStyle.Flat;
            this.btnVoltar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnVoltar.Cursor = Cursors.Hand;
            this.btnVoltar.Anchor = AnchorStyles.Bottom;
            this.btnVoltar.FlatAppearance.BorderColor = Color.White;
            this.btnVoltar.FlatAppearance.BorderSize = 2;
            this.btnVoltar.TextAlign = ContentAlignment.MiddleCenter;
            this.btnVoltar.ImageAlign = ContentAlignment.MiddleLeft;
            this.btnVoltar.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnVoltar.Padding = new Padding(5, 0, 0, 0);
            this.btnVoltar.Click += BtnVoltar_Click;

            // 
            // btnReverter
            // 
            this.btnReverter.Text = "Reverter Operação (F2)";
            this.btnReverter.Location = new Point(startX + buttonWidth + buttonSpacing, buttonY);
            this.btnReverter.Size = new Size(buttonWidth, buttonHeight);
            this.btnReverter.BackColor = Color.FromArgb(255, 197, 36); // #FFC524
            this.btnReverter.ForeColor = Color.Black;
            this.btnReverter.FlatStyle = FlatStyle.Flat;
            this.btnReverter.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnReverter.Cursor = Cursors.Hand;
            this.btnReverter.Anchor = AnchorStyles.Bottom;
            this.btnReverter.FlatAppearance.BorderColor = Color.Black;
            this.btnReverter.FlatAppearance.BorderSize = 2;
            this.btnReverter.Click += BtnReverter_Click;

            // 
            // lblTotalOperacoes
            // 
            this.lblTotalOperacoes.Location = new Point(10, 520);
            this.lblTotalOperacoes.Size = new Size(200, 20);
            this.lblTotalOperacoes.Font = new Font("Montserrat", 9F);
            this.lblTotalOperacoes.ForeColor = Color.White;
            this.lblTotalOperacoes.BackColor = Color.Transparent;
            this.lblTotalOperacoes.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // 
            // HistoricoOperacoesForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.dgvHistorico);
            this.Controls.Add(this.btnReverter);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.lblTotalOperacoes);
            this.Name = "HistoricoOperacoesForm";
            this.Resize += HistoricoOperacoesForm_Resize;

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void HistoricoOperacoesForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    BtnReverter_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HistoricoOperacoesForm_Resize(object sender, EventArgs e)
        {
            // Recalcular posição dos botões quando a tela for redimensionada
            RecalcularPosicaoBotoes();
        }

        private void RecalcularPosicaoBotoes()
        {
            int buttonWidth = 180;
            int buttonSpacing = 15;
            int totalButtonsWidth = (buttonWidth * 2) + buttonSpacing; // 2 botões
            int startX = (this.ClientSize.Width - totalButtonsWidth) / 2;
            int buttonY = this.ClientSize.Height - 90; // 90 pixels do bottom

            btnVoltar.Location = new Point(startX, buttonY);
            btnReverter.Location = new Point(startX + buttonWidth + buttonSpacing, buttonY);

            // Reposicionar label total operações
            lblTotalOperacoes.Location = new Point(10, buttonY + 10);
        }
    }
}