using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadastroMetasVendedores.Forms
{
    partial class HistoricoOperacoesForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvHistorico;
        private Button btnReverter;
        private Button btnFechar;
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
            this.btnFechar = new System.Windows.Forms.Button();
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

            // 
            // btnReverter
            // 
            this.btnReverter.Text = "Reverter Operação";
            this.btnReverter.Location = new Point(300, 510);
            this.btnReverter.Size = new Size(140, 35);
            this.btnReverter.BackColor = Color.FromArgb(255, 72, 72);
            this.btnReverter.ForeColor = Color.White;
            this.btnReverter.FlatStyle = FlatStyle.Flat;
            this.btnReverter.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnReverter.Cursor = Cursors.Hand;
            this.btnReverter.Anchor = AnchorStyles.Bottom;
            this.btnReverter.FlatAppearance.BorderColor = Color.Black;
            this.btnReverter.FlatAppearance.BorderSize = 2;
            this.btnReverter.Click += BtnReverter_Click;

            // 
            // btnFechar
            // 
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Location = new Point(460, 510);
            this.btnFechar.Size = new Size(120, 35);
            this.btnFechar.BackColor = Color.FromArgb(51, 67, 85);
            this.btnFechar.ForeColor = Color.White;
            this.btnFechar.FlatStyle = FlatStyle.Flat;
            this.btnFechar.Font = new Font("Montserrat", 9F, FontStyle.Bold);
            this.btnFechar.Cursor = Cursors.Hand;
            this.btnFechar.Anchor = AnchorStyles.Bottom;
            this.btnFechar.FlatAppearance.BorderColor = Color.White;
            this.btnFechar.FlatAppearance.BorderSize = 2;
            this.btnFechar.Click += (s, e) => this.Close();

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
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.lblTotalOperacoes);
            this.Name = "HistoricoOperacoesForm";

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}