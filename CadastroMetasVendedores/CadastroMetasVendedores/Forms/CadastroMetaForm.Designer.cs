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
        private Label lblAtivo;
        private PictureBox toggleAtivo;
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
            this.toggleAtivo = new System.Windows.Forms.PictureBox();
            this.lblAtivo = new System.Windows.Forms.Label();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.cmbPeriodicidade = new System.Windows.Forms.ComboBox();
            this.lblPeriodicidade = new System.Windows.Forms.Label();
            this.cmbTipoMeta = new System.Windows.Forms.ComboBox();
            this.lblTipoMeta = new System.Windows.Forms.Label();
            this.cmbProduto = new System.Windows.Forms.ComboBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.cmbVendedor = new System.Windows.Forms.ComboBox();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.grpDados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleAtivo)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDados
            // 
            this.grpDados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.grpDados.Controls.Add(this.toggleAtivo);
            this.grpDados.Controls.Add(this.lblAtivo);
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
            this.grpDados.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.grpDados.ForeColor = System.Drawing.Color.White;
            this.grpDados.Location = new System.Drawing.Point(20, 20);
            this.grpDados.Name = "grpDados";
            this.grpDados.Size = new System.Drawing.Size(480, 270);
            this.grpDados.TabIndex = 0;
            this.grpDados.TabStop = false;
            this.grpDados.Text = "Dados da Meta";
            // 
            // toggleAtivo
            // 
            this.toggleAtivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toggleAtivo.Location = new System.Drawing.Point(50, 190);
            this.toggleAtivo.Name = "toggleAtivo";
            this.toggleAtivo.Size = new System.Drawing.Size(60, 25);
            this.toggleAtivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.toggleAtivo.TabIndex = 6;
            this.toggleAtivo.TabStop = false;
            this.toggleAtivo.Click += new System.EventHandler(this.ToggleAtivo_Click);
            // 
            // lblAtivo
            // 
            this.lblAtivo.AutoSize = true;
            this.lblAtivo.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblAtivo.ForeColor = System.Drawing.Color.White;
            this.lblAtivo.Location = new System.Drawing.Point(10, 190);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Size = new System.Drawing.Size(35, 15);
            this.lblAtivo.TabIndex = 12;
            this.lblAtivo.Text = "Ativo:";
            // 
            // txtValor
            // 
            this.txtValor.BackColor = System.Drawing.Color.White;
            this.txtValor.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.txtValor.Location = new System.Drawing.Point(330, 148);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(120, 21);
            this.txtValor.TabIndex = 5;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtValor_KeyPress);
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblValor.Location = new System.Drawing.Point(330, 130);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(46, 15);
            this.lblValor.TabIndex = 10;
            this.lblValor.Text = "Valor *:";
            // 
            // cmbPeriodicidade
            // 
            this.cmbPeriodicidade.BackColor = System.Drawing.Color.White;
            this.cmbPeriodicidade.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbPeriodicidade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodicidade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPeriodicidade.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.cmbPeriodicidade.Location = new System.Drawing.Point(190, 148);
            this.cmbPeriodicidade.Name = "cmbPeriodicidade";
            this.cmbPeriodicidade.Size = new System.Drawing.Size(120, 23);
            this.cmbPeriodicidade.TabIndex = 4;
            // 
            // lblPeriodicidade
            // 
            this.lblPeriodicidade.AutoSize = true;
            this.lblPeriodicidade.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblPeriodicidade.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblPeriodicidade.Location = new System.Drawing.Point(190, 130);
            this.lblPeriodicidade.Name = "lblPeriodicidade";
            this.lblPeriodicidade.Size = new System.Drawing.Size(94, 15);
            this.lblPeriodicidade.TabIndex = 8;
            this.lblPeriodicidade.Text = "Periodicidade *:";
            // 
            // cmbTipoMeta
            // 
            this.cmbTipoMeta.BackColor = System.Drawing.Color.White;
            this.cmbTipoMeta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipoMeta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoMeta.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.cmbTipoMeta.Location = new System.Drawing.Point(20, 148);
            this.cmbTipoMeta.Name = "cmbTipoMeta";
            this.cmbTipoMeta.Size = new System.Drawing.Size(150, 23);
            this.cmbTipoMeta.TabIndex = 3;
            // 
            // lblTipoMeta
            // 
            this.lblTipoMeta.AutoSize = true;
            this.lblTipoMeta.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblTipoMeta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblTipoMeta.Location = new System.Drawing.Point(20, 130);
            this.lblTipoMeta.Name = "lblTipoMeta";
            this.lblTipoMeta.Size = new System.Drawing.Size(90, 15);
            this.lblTipoMeta.TabIndex = 6;
            this.lblTipoMeta.Text = "Tipo de Meta *:";
            // 
            // cmbProduto
            // 
            this.cmbProduto.BackColor = System.Drawing.Color.White;
            this.cmbProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbProduto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProduto.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.cmbProduto.Location = new System.Drawing.Point(250, 95);
            this.cmbProduto.Name = "cmbProduto";
            this.cmbProduto.Size = new System.Drawing.Size(200, 23);
            this.cmbProduto.TabIndex = 2;
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblProduto.Location = new System.Drawing.Point(250, 77);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(61, 15);
            this.lblProduto.TabIndex = 4;
            this.lblProduto.Text = "Produto *:";
            // 
            // cmbVendedor
            // 
            this.cmbVendedor.BackColor = System.Drawing.Color.White;
            this.cmbVendedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVendedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVendedor.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.cmbVendedor.Location = new System.Drawing.Point(20, 95);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(200, 23);
            this.cmbVendedor.TabIndex = 1;
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblVendedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblVendedor.Location = new System.Drawing.Point(20, 77);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(71, 15);
            this.lblVendedor.TabIndex = 2;
            this.lblVendedor.Text = "Vendedor *:";
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.White;
            this.txtNome.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.txtNome.Location = new System.Drawing.Point(20, 48);
            this.txtNome.MaxLength = 100;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(430, 21);
            this.txtNome.TabIndex = 0;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.lblNome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.lblNome.Location = new System.Drawing.Point(20, 30);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(100, 15);
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome da Meta *:";
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(67)))), ((int)(((byte)(85)))));
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.btnVoltar.ForeColor = System.Drawing.Color.White;
            this.btnVoltar.Image = global::CadastroMetasVendedores.Properties.Resources.icone_voltar;
            this.btnVoltar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVoltar.Location = new System.Drawing.Point(255, 310);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(120, 35);
            this.btnVoltar.TabIndex = 7;
            this.btnVoltar.Text = "   Voltar (ESC)";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.BtnVoltar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(36)))));
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.btnSalvar.ForeColor = System.Drawing.Color.Black;
            this.btnSalvar.Image = global::CadastroMetasVendedores.Properties.Resources.icone_salvar;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(380, 310);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(120, 35);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "   Salvar (F2)";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.BtnSalvar_Click);
            // 
            // CadastroMetaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(67)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(520, 365);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.grpDados);
            this.Font = new System.Drawing.Font("Montserrat", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CadastroMetaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Meta";
            this.Load += new System.EventHandler(this.CadastroMetaForm_Load);
            this.grpDados.ResumeLayout(false);
            this.grpDados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleAtivo)).EndInit();
            this.ResumeLayout(false);

        }
    }
}