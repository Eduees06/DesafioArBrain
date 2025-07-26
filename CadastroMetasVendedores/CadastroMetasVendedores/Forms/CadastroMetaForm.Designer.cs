using System;
using System.Windows.Forms;
using System.Drawing;

namespace CadastroMetasVendedores.Forms
{
    partial class CadastroMetaForm
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpDados;
        private Label lblNome;
        private TextBox txtNome;
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
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
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
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
            this.grpDados.Controls.Add(this.txtNome);
            this.grpDados.Controls.Add(this.lblNome);
            this.grpDados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDados.Location = new System.Drawing.Point(20, 20);
            this.grpDados.Name = "grpDados";
            this.grpDados.Size = new System.Drawing.Size(480, 270);
            this.grpDados.TabIndex = 0;
            this.grpDados.TabStop = false;
            this.grpDados.Text = "Dados da Meta";
            this.grpDados.BackColor = System.Drawing.ColorTranslator.FromHtml("#464646");
            this.grpDados.ForeColor = System.Drawing.Color.White;
            this.grpDados.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(20, 30);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(102, 15);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome da Meta *:";
            this.lblNome.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblNome.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(20, 48);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(430, 23);
            this.txtNome.TabIndex = 0;
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNome.MaxLength = 100;

            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(20, 80);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(70, 15);
            this.lblVendedor.TabIndex = 2;
            this.lblVendedor.Text = "Vendedor *:";
            this.lblVendedor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblVendedor.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbVendedor
            // 
            this.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendedor.Location = new System.Drawing.Point(20, 98);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(200, 23);
            this.cmbVendedor.TabIndex = 1;
            this.cmbVendedor.BackColor = System.Drawing.Color.White;
            this.cmbVendedor.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbVendedor.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Location = new System.Drawing.Point(250, 80);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(62, 15);
            this.lblProduto.TabIndex = 4;
            this.lblProduto.Text = "Produto *:";
            this.lblProduto.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblProduto.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbProduto
            // 
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduto.Location = new System.Drawing.Point(250, 98);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(200, 23);
            this.cmbProduto.TabIndex = 2;
            this.cmbProduto.BackColor = System.Drawing.Color.White;
            this.cmbProduto.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbProduto.SelectedIndexChanged += new System.EventHandler(this.cmbProduto_SelectedIndexChanged);

            // 
            // lblTipoMeta
            // 
            this.lblTipoMeta.AutoSize = true;
            this.lblTipoMeta.Location = new System.Drawing.Point(20, 130);
            this.lblTipoMeta.Name = "lblTipoMeta";
            this.lblTipoMeta.Size = new System.Drawing.Size(90, 15);
            this.lblTipoMeta.TabIndex = 6;
            this.lblTipoMeta.Text = "Tipo de Meta *:";
            this.lblTipoMeta.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblTipoMeta.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbTipoMeta
            // 
            this.cmbTipoMeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMeta.Location = new System.Drawing.Point(20, 148);
            this.cmbTipoMeta.Name = "cmbTipoMeta";
            this.cmbTipoMeta.Size = new System.Drawing.Size(150, 23);
            this.cmbTipoMeta.TabIndex = 3;
            this.cmbTipoMeta.BackColor = System.Drawing.Color.White;
            this.cmbTipoMeta.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbTipoMeta.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblPeriodicidade
            // 
            this.lblPeriodicidade.AutoSize = true;
            this.lblPeriodicidade.Location = new System.Drawing.Point(190, 130);
            this.lblPeriodicidade.Name = "lblPeriodicidade";
            this.lblPeriodicidade.Size = new System.Drawing.Size(95, 15);
            this.lblPeriodicidade.TabIndex = 8;
            this.lblPeriodicidade.Text = "Periodicidade *:";
            this.lblPeriodicidade.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblPeriodicidade.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // cmbPeriodicidade
            // 
            this.cmbPeriodicidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodicidade.Location = new System.Drawing.Point(190, 148);
            this.cmbPeriodicidade.Name = "cmbPeriodicidade";
            this.cmbPeriodicidade.Size = new System.Drawing.Size(120, 23);
            this.cmbPeriodicidade.TabIndex = 4;
            this.cmbPeriodicidade.BackColor = System.Drawing.Color.White;
            this.cmbPeriodicidade.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbPeriodicidade.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(330, 130);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(46, 15);
            this.lblValor.TabIndex = 10;
            this.lblValor.Text = "Valor *:";
            this.lblValor.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.lblValor.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(330, 148);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 23);
            this.txtValor.TabIndex = 5;
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);

            // 
            // chkAtivo
            // 
            this.chkAtivo.AutoSize = true;
            this.chkAtivo.Location = new System.Drawing.Point(20, 190);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(56, 19);
            this.chkAtivo.TabIndex = 6;
            this.chkAtivo.Text = "Ativo";
            this.chkAtivo.UseVisualStyleBackColor = true;
            this.chkAtivo.ForeColor = System.Drawing.Color.White;
            this.chkAtivo.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAtivo.Cursor = System.Windows.Forms.Cursors.Hand;

            // 
            // btnVoltar
            // 
            this.btnVoltar.Location = new System.Drawing.Point(255, 310);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(120, 35);
            this.btnVoltar.TabIndex = 7;
            this.btnVoltar.Text = "   Voltar (ESC)";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.BackColor = System.Drawing.ColorTranslator.FromHtml("#334355");
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVoltar.FlatAppearance.BorderSize = 1;
            this.btnVoltar.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.Image = Properties.Resources.icone_voltar;
            this.btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);

            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(380, 310);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(120, 35);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "   Salvar (F2)";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFC524");
            this.btnSalvar.ForeColor = System.Drawing.Color.Black;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSalvar.FlatAppearance.BorderSize = 1;
            this.btnSalvar.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Image = Properties.Resources.icone_salvar;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            // 
            // CadastroMetaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 365);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.grpDados);
            this.Name = "CadastroMetaForm";
            this.Text = "Cadastro de Meta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#334355");
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Load += new System.EventHandler(this.CadastroMetaForm_Load);
            this.grpDados.ResumeLayout(false);
            this.grpDados.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}