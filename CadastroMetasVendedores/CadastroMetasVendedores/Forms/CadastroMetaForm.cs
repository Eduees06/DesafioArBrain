using System;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;

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
            CarregarDados();
        }

        // Construtor para edição
        public CadastroMetaForm(MetaService metaService, VendedorService vendedorService,
            ProdutoService produtoService, int metaId) : this(metaService, vendedorService, produtoService)
        {
            _metaId = metaId;
            CarregarMetaParaEdicao();
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
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Meta não encontrada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

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
                MessageBox.Show($"Erro ao carregar meta: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos(out string mensagemErro))
            {
                MessageBox.Show(mensagemErro, "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!int.TryParse(cmbVendedor.SelectedValue.ToString(), out int vendedorId))
                {
                    MessageBox.Show("Erro ao obter ID do vendedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(cmbProduto.SelectedValue.ToString(), out int produtoId))
                {
                    MessageBox.Show("Erro ao obter ID do produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var meta = new Meta
                {
                    Id = _metaId ?? 0,
                    VendedorId = vendedorId,
                    ProdutoId = produtoId,
                    TipoMeta = (TipoMeta)((ComboBoxItem)cmbTipoMeta.SelectedItem).Value,
                    Periodicidade = (PeriodicidadeMeta)((ComboBoxItem)cmbPeriodicidade.SelectedItem).Value,
                    Valor = decimal.Parse(txtValor.Text),
                    Ativo = chkAtivo.Checked
                };

                bool sucesso;

                if (_metaId.HasValue)
                {
                    sucesso = _metaService.AtualizarMeta(meta);
                    MessageBox.Show(sucesso ? "Meta atualizada com sucesso!" : "Erro ao atualizar meta.",
                        sucesso ? "Sucesso" : "Erro", MessageBoxButtons.OK,
                        sucesso ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                }
                else
                {
                    var novoId = _metaService.CriarMeta(meta);
                    if (novoId > 0)
                    {
                        MessageBox.Show($"Meta criada com sucesso! ID: {novoId}", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sucesso = true;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao criar meta.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Erro ao salvar meta: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos(out string mensagemErro)
        {
            mensagemErro = "";

            if (cmbVendedor.SelectedValue == null)
            {
                mensagemErro = "Selecione um vendedor.";
                cmbVendedor.Focus();
                return false;
            }

            if (cmbProduto.SelectedValue == null)
            {
                mensagemErro = "Selecione um produto.";
                cmbProduto.Focus();
                return false;
            }

            if (cmbTipoMeta.SelectedItem == null)
            {
                mensagemErro = "Selecione o tipo de meta.";
                cmbTipoMeta.Focus();
                return false;
            }

            if (cmbPeriodicidade.SelectedItem == null)
            {
                mensagemErro = "Selecione a periodicidade.";
                cmbPeriodicidade.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValor.Text))
            {
                mensagemErro = "Informe o valor da meta.";
                txtValor.Focus();
                return false;
            }

            if (!decimal.TryParse(txtValor.Text, out decimal valor) || valor <= 0)
            {
                mensagemErro = "Informe um valor válido maior que zero.";
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
            // Permite apenas números, vírgula e backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Permite apenas uma vírgula
            if (e.KeyChar == ',' && (sender as TextBox).Text.IndexOf(',') > -1)
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