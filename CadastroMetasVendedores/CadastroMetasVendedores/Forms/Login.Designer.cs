namespace CadastroMetasVendedores.Forms
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logoArBrain = new System.Windows.Forms.PictureBox();
            this.textBoxUsuario = new System.Windows.Forms.TextBox();
            this.textBoxSenha = new System.Windows.Forms.TextBox();
            this.labelUsuario = new System.Windows.Forms.Label();
            this.labelSenha = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonSair = new System.Windows.Forms.Button();
            this.labelLoginIncorreto = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoArBrain)).BeginInit();
            this.SuspendLayout();
            // 
            // logoArBrain
            // 
            this.logoArBrain.Image = global::CadastroMetasVendedores.Properties.Resources.VectorArBrain;
            this.logoArBrain.Location = new System.Drawing.Point(508, 150);
            this.logoArBrain.Name = "logoArBrain";
            this.logoArBrain.Size = new System.Drawing.Size(178, 177);
            this.logoArBrain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logoArBrain.TabIndex = 0;
            this.logoArBrain.TabStop = false;
            // 
            // textBoxUsuario
            // 
            this.textBoxUsuario.Location = new System.Drawing.Point(499, 345);
            this.textBoxUsuario.Name = "textBoxUsuario";
            this.textBoxUsuario.Size = new System.Drawing.Size(206, 22);
            this.textBoxUsuario.TabIndex = 1;
            // 
            // textBoxSenha
            // 
            this.textBoxSenha.Location = new System.Drawing.Point(499, 388);
            this.textBoxSenha.Name = "textBoxSenha";
            this.textBoxSenha.PasswordChar = '*';
            this.textBoxSenha.Size = new System.Drawing.Size(206, 22);
            this.textBoxSenha.TabIndex = 2;
            // 
            // labelUsuario
            // 
            this.labelUsuario.AutoSize = true;
            this.labelUsuario.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsuario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelUsuario.Location = new System.Drawing.Point(417, 346);
            this.labelUsuario.Name = "labelUsuario";
            this.labelUsuario.Size = new System.Drawing.Size(76, 19);
            this.labelUsuario.TabIndex = 3;
            this.labelUsuario.Text = "Usuário :";
            // 
            // labelSenha
            // 
            this.labelSenha.AutoSize = true;
            this.labelSenha.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSenha.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSenha.Location = new System.Drawing.Point(429, 389);
            this.labelSenha.Name = "labelSenha";
            this.labelSenha.Size = new System.Drawing.Size(64, 19);
            this.labelSenha.TabIndex = 4;
            this.labelSenha.Text = "Senha :";
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(181)))), ((int)(((byte)(28)))));
            this.buttonLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLogin.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonLogin.Location = new System.Drawing.Point(601, 444);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(104, 49);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "Logar";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            this.buttonLogin.MouseEnter += new System.EventHandler(this.buttonLogin_MouseEnter_1);
            this.buttonLogin.MouseLeave += new System.EventHandler(this.buttonLogin_MouseLeave_1);
            // 
            // buttonSair
            // 
            this.buttonSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.buttonSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSair.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSair.ForeColor = System.Drawing.Color.White;
            this.buttonSair.Location = new System.Drawing.Point(482, 444);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(104, 49);
            this.buttonSair.TabIndex = 6;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = false;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            this.buttonSair.MouseEnter += new System.EventHandler(this.buttonSair_MouseEnter);
            this.buttonSair.MouseLeave += new System.EventHandler(this.buttonSair_MouseLeave);
            // 
            // labelLoginIncorreto
            // 
            this.labelLoginIncorreto.AutoSize = true;
            this.labelLoginIncorreto.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoginIncorreto.ForeColor = System.Drawing.Color.IndianRed;
            this.labelLoginIncorreto.Location = new System.Drawing.Point(442, 514);
            this.labelLoginIncorreto.Name = "labelLoginIncorreto";
            this.labelLoginIncorreto.Size = new System.Drawing.Size(0, 19);
            this.labelLoginIncorreto.TabIndex = 7;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(67)))), ((int)(((byte)(85)))));
            this.ClientSize = new System.Drawing.Size(1182, 673);
            this.Controls.Add(this.labelLoginIncorreto);
            this.Controls.Add(this.buttonSair);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelSenha);
            this.Controls.Add(this.labelUsuario);
            this.Controls.Add(this.textBoxSenha);
            this.Controls.Add(this.textBoxUsuario);
            this.Controls.Add(this.logoArBrain);
            this.MaximumSize = new System.Drawing.Size(1200, 720);
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "Login";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.logoArBrain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoArBrain;
        private System.Windows.Forms.TextBox textBoxUsuario;
        private System.Windows.Forms.TextBox textBoxSenha;
        private System.Windows.Forms.Label labelUsuario;
        private System.Windows.Forms.Label labelSenha;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.Label labelLoginIncorreto;
    }
}