using System;
using System.Windows.Forms;
using System.Drawing;

namespace CadastroMetasVendedores.Forms
{
    partial class CadastroMetaForm
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpDados;
        private Label lblVendedor;
        private ComboBox cmbVendedor;
        private Label lblProduto;
        private ComboBox cmbProduto;
        private Label lblTipoMeta;
        private ComboBox cmbTipoMeta;
        private Label lblPeriodicidade;
        private ComboBox cmbPeriodicidade;
        private Label lblValor;
        private TextBox txtValor;
        private CheckBox chkAtivo;
        private Button btnSalvar;
        private Button btnCancelar;
        private Button btnVoltar;

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
            this.grpDados = new System.Windows.Forms.GroupBox();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.cmbVendedor = new System.Windows.Forms.ComboBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.lblTipoMeta = new System.Windows.Forms.Label();
            this.cmbTipoMeta = new System.Windows.Forms.ComboBox();
            this.lblPeriodicidade = new System.Windows.Forms.Label();
            this.cmbPeriodicidade = new System.Windows.Forms.ComboBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.grpDados.SuspendLayout();
            this.SuspendLayout();

            // 
            // grpDados
            // 
            this.grpDados.Controls.Add(this.chkAtivo);
            this.grpDados.Controls.Add(this.txtValor);
            this.grpDados.Controls.Add(this.lblValor);
            this.grpDados.Controls.Add(this.cmbPeriodicidade);
            this.grpDados.Controls.Add(this.lblPeriodicidade);
            this.grpDados.Controls.Add(this.cmbTipoMeta);
            this.grpDados.Controls.Add(this.lblTipoMeta);
            this.grpDados.Controls.Add(this.cmbProduto);
            this.grpDados.Controls.Add(this.lblProduto);
            this.grpDados.Controls.Add(this.cmbVendedor);
            this.grpDados.Controls.Add(this.lblVendedor);
            this.grpDados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDados.Location = new System.Drawing.Point(20, 20);
            this.grpDados.Name = "grpDados";
            this.grpDados.Size = new System.Drawing.Size(480, 220);
            this.grpDados.TabIndex = 0;
            this.grpDados.TabStop = false;
            this.grpDados.Text = "Dados da Meta";
            this.grpDados.BackColor = System.Drawing.ColorTranslator.FromHtml("#464646");
            this.grpDados.ForeColor = System.Drawing.Color.White;
            this.grpDados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(20, 30);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(70, 15);
            this.lblVendedor.TabIndex = 0;
            this.lblVendedor.Text = "Vendedor *:";
            this.lblVendedor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbVendedor
            // 
            this.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendedor.Location = new System.Drawing.Point(20, 48);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(200, 23);
            this.cmbVendedor.TabIndex = 1;
            this.cmbVendedor.BackColor = System.Drawing.Color.White;
            this.cmbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbVendedor.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Location = new System.Drawing.Point(250, 30);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(62, 15);
            this.lblProduto.TabIndex = 2;
            this.lblProduto.Text = "Produto *:";
            this.lblProduto.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbProduto
            // 
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduto.Location = new System.Drawing.Point(250, 48);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(200, 23);
            this.cmbProduto.TabIndex = 3;
            this.cmbProduto.BackColor = System.Drawing.Color.White;
            this.cmbProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbProduto.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblTipoMeta
            // 
            this.lblTipoMeta.AutoSize = true;
            this.lblTipoMeta.Location = new System.Drawing.Point(20, 77); // 48 + 29 = 77
            this.lblTipoMeta.Name = "lblTipoMeta";
            this.lblTipoMeta.Size = new System.Drawing.Size(90, 15);
            this.lblTipoMeta.TabIndex = 4;
            this.lblTipoMeta.Text = "Tipo de Meta *:";
            this.lblTipoMeta.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblTipoMeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbTipoMeta
            // 
            this.cmbTipoMeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMeta.Location = new System.Drawing.Point(20, 95); // 77 + 18 = 95
            this.cmbTipoMeta.Name = "cmbTipoMeta";
            this.cmbTipoMeta.Size = new System.Drawing.Size(150, 23);
            this.cmbTipoMeta.TabIndex = 5;
            this.cmbTipoMeta.BackColor = System.Drawing.Color.White;
            this.cmbTipoMeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbTipoMeta.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblPeriodicidade
            // 
            this.lblPeriodicidade.AutoSize = true;
            this.lblPeriodicidade.Location = new System.Drawing.Point(190, 77);
            this.lblPeriodicidade.Name = "lblPeriodicidade";
            this.lblPeriodicidade.Size = new System.Drawing.Size(95, 15);
            this.lblPeriodicidade.TabIndex = 6;
            this.lblPeriodicidade.Text = "Periodicidade *:";
            this.lblPeriodicidade.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblPeriodicidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbPeriodicidade
            // 
            this.cmbPeriodicidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodicidade.Location = new System.Drawing.Point(190, 95);
            this.cmbPeriodicidade.Name = "cmbPeriodicidade";
            this.cmbPeriodicidade.Size = new System.Drawing.Size(120, 23);
            this.cmbPeriodicidade.TabIndex = 7;
            this.cmbPeriodicidade.BackColor = System.Drawing.Color.White;
            this.cmbPeriodicidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPeriodicidade.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(330, 77);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(45, 15);
            this.lblValor.TabIndex = 8;
            this.lblValor.Text = "Valor *:";
            this.lblValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(330, 95);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 23);
            this.txtValor.TabIndex = 9;
            this.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Location = new System.Drawing.Point(20, 130); // 95 + 29 + 6 = 130
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(54, 19);
            this.chkAtivo.TabIndex = 10;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            this.chkAtivo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#BBBBBB");
            this.chkAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(425, 260);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 30);
            this.btnSalvar.TabIndex = 11;
            this.btnSalvar.Text = "  Salvar";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            this.btnSalvar.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.btnSalvar.ForeColor = System.Drawing.Color.Black;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            try
            {
                this.btnSalvar.Image = Properties.Resources.icone_salvar;
            }
            catch
            {
                // Se não encontrar o ícone, continua sem ele
            }

            // 
            // btnVoltar
            // 
            this.btnVoltar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVoltar.Location = new System.Drawing.Point(340, 260);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(75, 30);
            this.btnVoltar.TabIndex = 12;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnVoltar.BackColor = System.Drawing.ColorTranslator.FromHtml("#464646");
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.Location = new System.Drawing.Point(20, 260);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Excluir";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF4848");
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // CadastroMetaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 311);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.grpDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(540, 350);
            this.Name = "CadastroMetaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cadastro de Meta";
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#334355");
            this.grpDados.ResumeLayout(false);
            this.grpDados.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}