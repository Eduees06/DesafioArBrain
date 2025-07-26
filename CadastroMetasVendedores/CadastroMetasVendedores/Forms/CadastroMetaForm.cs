using System;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;
using CadastroMetasVendedores.Helpers;
using System.Drawing;

namespace CadastroMetasVendedores.Forms
{
    public partial class CadastroMetaForm : Form
    {
        private readonly MetaService _metaService;
        private readonly VendedorService _vendedorService;
        private readonly ProdutoService _produtoService;

        private int? _metaId; // Para edição
        private Meta _metaAtual;

        public CadastroMetaForm(MetaService metaService, VendedorService vendedorService, ProdutoService produtoService)
        {
            _metaService = metaService;
            _vendedorService = vendedorService;
            _produtoService = produtoService;

            InitializeComponent();
            ConfigurarEventos();
            CarregarDados();
        }

        // Construtor para edição
        public CadastroMetaForm(MetaService metaService, VendedorService vendedorService,
            ProdutoService produtoService, int metaId) : this(metaService, vendedorService, produtoService)
        {
            _metaId = metaId;
            CarregarMetaParaEdicao();
        }

        private void ConfigurarEventos()
        {
            // Configurar atalhos
            this.KeyPreview = true;
            this.KeyDown += CadastroMetaForm_KeyDown;

            // Configurar eventos para campos obrigatórios
            txtNome.Enter += Campo_Enter;
            cmbVendedor.Enter += Campo_Enter;
            cmbProduto.Enter += Campo_Enter;
            cmbTipoMeta.Enter += Campo_Enter;
            cmbPeriodicidade.Enter += Campo_Enter;
            txtValor.Enter += Campo_Enter;

            // Configurar TabIndex para ordem de navegação
            txtNome.TabIndex = 0;
            cmbVendedor.TabIndex = 1;
            cmbProduto.TabIndex = 2;
            cmbTipoMeta.TabIndex = 3;
            cmbPeriodicidade.TabIndex = 4;
            txtValor.TabIndex = 5;
            chkAtivo.TabIndex = 6;
            btnSalvar.TabIndex = 7;
            btnVoltar.TabIndex = 8;
        }

        private void CadastroMetaForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    btnSalvar_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        private void Campo_Enter(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.BackColor == Color.FromArgb(252, 199, 194))
            {
                campo.BackColor = Color.White;
            }
        }

        private void CarregarDados()
        {
            try
            {
                CarregarVendedores();
                CarregarProdutos();
                CarregarTiposMeta();
                CarregarPeriodicidades();

                // Valores padrão
                chkAtivo.Checked = true;
                if (cmbTipoMeta.Items.Count > 0)
                    cmbTipoMeta.SelectedIndex = 0;
                if (cmbPeriodicidade.Items.Count > 2)
                    cmbPeriodicidade.SelectedIndex = 2;
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.ExibirErro($"Erro: Falha ao carregar dados.\nDetalhe: {ex.Message}\nDica: Verifique a conexão e tente novamente.");
            }
        }

        private void CarregarVendedores()
        {
            var vendedores = _vendedorService.ObterVendedoresParaComboBox();
            if (vendedores != null && vendedores.Any())
            {
                cmbVendedor.DataSource = vendedores.ToList();
                cmbVendedor.DisplayMember = "Value";
                cmbVendedor.ValueMember = "Key";
            }
            cmbVendedor.SelectedIndex = -1;
        }

        private void CarregarProdutos()
        {
            var produtos = _produtoService.ObterProdutosParaComboBox();
            if (produtos != null && produtos.Any())
            {
                cmbProduto.DataSource = produtos.ToList();
                cmbProduto.DisplayMember = "Value";
                cmbProduto.ValueMember = "Key";
            }
            cmbProduto.SelectedIndex = -1;
        }

        private void CarregarTiposMeta()
        {
            cmbTipoMeta.Items.Clear();
            cmbTipoMeta.Items.Add(new ComboBoxItem { Value = TipoMeta.Monetario, Text = "Monetário (R$)" });
            cmbTipoMeta.Items.Add(new ComboBoxItem { Value = TipoMeta.Litros, Text = "Litros (L)" });
            cmbTipoMeta.Items.Add(new ComboBoxItem { Value = TipoMeta.Unidades, Text = "Unidades (UN)" });

            cmbTipoMeta.DisplayMember = "Text";
            cmbTipoMeta.ValueMember = "Value";
            cmbTipoMeta.SelectedIndex = -1;
        }

        private void CarregarPeriodicidades()
        {
            cmbPeriodicidade.Items.Clear();
            cmbPeriodicidade.Items.Add(new ComboBoxItem { Value = PeriodicidadeMeta.Diaria, Text = "Diária" });
            cmbPeriodicidade.Items.Add(new ComboBoxItem { Value = PeriodicidadeMeta.Semanal, Text = "Semanal" });
            cmbPeriodicidade.Items.Add(new ComboBoxItem { Value = PeriodicidadeMeta.Mensal, Text = "Mensal" });

            cmbPeriodicidade.DisplayMember = "Text";
            cmbPeriodicidade.ValueMember = "Value";
            cmbPeriodicidade.SelectedIndex = -1;
        }

        private void CarregarMetaParaEdicao()
        {
            if (!_metaId.HasValue) return;

            try
            {
                _metaAtual = _metaService.ObterMetaPorId(_metaId.Value);
                if (_metaAtual == null)
                {
                    ErrorMessageHelper.ExibirErro("Erro: Meta não encontrada.\nDetalhe: A meta selecionada não existe no sistema.\nDica: A meta pode ter sido removida por outro usuário.");
                    Close();
                    return;
                }

                txtNome.Text = _metaAtual.Nome;
                cmbVendedor.SelectedValue = _metaAtual.VendedorId;
                cmbProduto.SelectedValue = _metaAtual.ProdutoId;

                // Selecionar tipo de meta
                for (int i = 0; i < cmbTipoMeta.Items.Count; i++)
                {
                    var item = (ComboBoxItem)cmbTipoMeta.Items[i];
                    if ((TipoMeta)item.Value == _metaAtual.TipoMeta)
                    {
                        cmbTipoMeta.SelectedIndex = i;
                        break;
                    }
                }

                // Selecionar periodicidade
                for (int i = 0; i < cmbPeriodicidade.Items.Count; i++)
                {
                    var item = (ComboBoxItem)cmbPeriodicidade.Items[i];
                    if ((PeriodicidadeMeta)item.Value == _metaAtual.Periodicidade)
                    {
                        cmbPeriodicidade.SelectedIndex = i;
                        break;
                    }
                }

                txtValor.Text = _metaAtual.Valor.ToString("N2");
                chkAtivo.Checked = _metaAtual.Ativo;

                Text = "Editar Meta";
                btnSalvar.Text = "Atualizar";
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.ExibirErro($"Erro: Falha ao carregar meta.\nDetalhe: {ex.Message}\nDica: Verifique se a meta ainda existe e tente novamente.");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(out string mensagemErro))
            {
                ErrorMessageHelper.ExibirErro(mensagemErro);
                return;
            }

            try
            {
                if (!int.TryParse(cmbVendedor.SelectedValue.ToString(), out int vendedorId))
                {
                    ErrorMessageHelper.ExibirErro("Erro: ID do vendedor inválido.\nDetalhe: Não foi possível obter o identificador do vendedor.\nDica: Selecione novamente o vendedor.");
                    return;
                }

                if (!int.TryParse(cmbProduto.SelectedValue.ToString(), out int produtoId))
                {
                    ErrorMessageHelper.ExibirErro("Erro: ID do produto inválido.\nDetalhe: Não foi possível obter o identificador do produto.\nDica: Selecione novamente o produto.");
                    return;
                }

                var meta = new Meta
                {
                    Id = _metaId ?? 0,
                    Nome = txtNome.Text.Trim(),
                    VendedorId = vendedorId,
                    ProdutoId = produtoId,
                    TipoMeta = (TipoMeta)((ComboBoxItem)cmbTipoMeta.SelectedItem).Value,
                    Periodicidade = (PeriodicidadeMeta)((ComboBoxItem)cmbPeriodicidade.SelectedItem).Value,
                    Valor = decimal.Parse(txtValor.Text.Replace('.', ',')),
                    Ativo = chkAtivo.Checked
                };

                bool sucesso;

                if (_metaId.HasValue)
                {
                    sucesso = _metaService.AtualizarMeta(meta);
                    if (sucesso)
                    {
                        MessageBox.Show("Meta atualizada com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var novoId = _metaService.CriarMeta(meta);
                    if (novoId > 0)
                    {
                        MessageBox.Show($"Meta '{meta.Nome}' criada com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sucesso = true;
                    }
                    else
                    {
                        ErrorMessageHelper.ExibirErro("Erro: Falha ao criar meta.\nDetalhe: A operação não foi concluída.\nDica: Verifique os dados e tente novamente.");
                        sucesso = false;
                    }
                }

                if (sucesso)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessageHelper.ExibirErro($"Erro: Falha ao salvar meta.\nDetalhe: {ex.Message}\nDica: Verifique os dados informados e tente novamente.");
            }
        }

        private bool ValidarCampos(out string mensagemErro)
        {
            mensagemErro = "";
            bool camposValidos = true;

            // Resetar cores dos campos
            txtNome.BackColor = Color.White;
            cmbVendedor.BackColor = Color.White;
            cmbProduto.BackColor = Color.White;
            cmbTipoMeta.BackColor = Color.White;
            cmbPeriodicidade.BackColor = Color.White;
            txtValor.BackColor = Color.White;

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                mensagemErro = "Erro: Nome da meta obrigatório.\nDetalhe: O campo nome da meta deve ser preenchido.\nDica: Digite um nome descritivo para identificar a meta.";
                txtNome.BackColor = Color.FromArgb(252, 199, 194);
                txtNome.Focus();
                return false;
            }

            if (cmbVendedor.SelectedValue == null)
            {
                mensagemErro = "Erro: Vendedor obrigatório.\nDetalhe: É necessário selecionar um vendedor para a meta.\nDica: Escolha um vendedor da lista suspensa.";
                cmbVendedor.BackColor = Color.FromArgb(252, 199, 194);
                cmbVendedor.Focus();
                return false;
            }

            if (cmbProduto.SelectedValue == null)
            {
                mensagemErro = "Erro: Produto obrigatório.\nDetalhe: É necessário selecionar um produto para a meta.\nDica: Escolha um produto da lista suspensa.";
                cmbProduto.BackColor = Color.FromArgb(252, 199, 194);
                cmbProduto.Focus();
                return false;
            }

            if (cmbTipoMeta.SelectedItem == null)
            {
                mensagemErro = "Erro: Tipo de meta obrigatório.\nDetalhe: É necessário selecionar o tipo da meta.\nDica: Escolha entre Monetário, Litros ou Unidades.";
                cmbTipoMeta.BackColor = Color.FromArgb(252, 199, 194);
                cmbTipoMeta.Focus();
                return false;
            }

            if (cmbPeriodicidade.SelectedItem == null)
            {
                mensagemErro = "Erro: Periodicidade obrigatória.\nDetalhe: É necessário selecionar a periodicidade da meta.\nDica: Escolha entre Diária, Semanal ou Mensal.";
                cmbPeriodicidade.BackColor = Color.FromArgb(252, 199, 194);
                cmbPeriodicidade.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                mensagemErro = "Erro: Valor da meta obrigatório.\nDetalhe: É necessário informar o valor da meta.\nDica: Digite um valor numérico maior que zero.";
                txtValor.BackColor = Color.FromArgb(252, 199, 194);
                txtValor.Focus();
                return false;
            }

            string valorLimpo = txtValor.Text.Replace('.', ',');
            if (!decimal.TryParse(valorLimpo, out decimal valor) || valor <= 0)
            {
                mensagemErro = "Erro: Valor inválido.\nDetalhe: O valor da meta deve ser um número maior que zero.\nDica: Digite apenas números e vírgula para decimais.";
                txtValor.BackColor = Color.FromArgb(252, 199, 194);
                txtValor.Focus();
                txtValor.SelectAll();
                return false;
            }

            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números, vírgula, ponto e backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Permite apenas uma vírgula ou ponto
            TextBox textBox = sender as TextBox;
            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                (textBox.Text.IndexOf(',') > -1 || textBox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CadastroMetaForm_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    // Classe auxiliar para ComboBox
    public class ComboBoxItem
    {
        public object Value { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}