using System;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;
using System.Drawing;
using System.Collections.Generic;

namespace CadastroMetasVendedores.Forms
{
    public partial class CadastroMetaForm : Form
    {
        private readonly MetaService _metaService;
        private readonly VendedorService _vendedorService;
        private readonly ProdutoService _produtoService;

        private int? _metaId; // Para edição
        private Meta _metaAtual;
        private bool _toggleAtivoState = true; // Estado do toggle de ativação

        // DICTIONARY PARA CONTROLAR ESTADO DE ERRO DOS COMBOBOXES
        private Dictionary<ComboBox, bool> combosComErro = new Dictionary<ComboBox, bool>();

        public CadastroMetaForm(MetaService metaService, VendedorService vendedorService, ProdutoService produtoService)
        {
            _metaService = metaService;
            _vendedorService = vendedorService;
            _produtoService = produtoService;

            InitializeComponent();
            ConfigurarEventos();
            CarregarDados();
            ConfigurarValidacaoVisual();
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

            // Configurar eventos para campos obrigatórios - resetar cor de fundo ao entrar no campo
            txtNome.Enter += Campo_Enter;
            cmbVendedor.Enter += Campo_Enter;
            cmbProduto.Enter += Campo_Enter;
            cmbTipoMeta.Enter += Campo_Enter;
            cmbPeriodicidade.Enter += Campo_Enter;
            txtValor.Enter += Campo_Enter;

            // ADICIONANDO EVENTOS DE CLICK PARA COMBOBOXES
            cmbVendedor.Click += Campo_Enter;
            cmbProduto.Click += Campo_Enter;
            cmbTipoMeta.Click += Campo_Enter;
            cmbPeriodicidade.Click += Campo_Enter;

            // NOVO: Adicionar eventos TextChanged para garantir que a cor seja resetada
            txtNome.TextChanged += Campo_TextChanged;
            txtValor.TextChanged += Campo_TextChanged;

            // Configurar TabIndex para ordem de navegação
            txtNome.TabIndex = 0;
            cmbVendedor.TabIndex = 1;
            cmbProduto.TabIndex = 2;
            cmbTipoMeta.TabIndex = 3;
            cmbPeriodicidade.TabIndex = 4;
            txtValor.TabIndex = 5;
            toggleAtivo.TabIndex = 6;
            btnSalvar.TabIndex = 7;
            btnVoltar.TabIndex = 8;
        }
        private void Campo_TextChanged(object sender, EventArgs e)
        {
            Control campo = sender as Control;
            if (campo != null && campo.BackColor != Color.White)
            {
                campo.BackColor = Color.White;
                campo.Invalidate();
                campo.Refresh();
            }
        }


        // CONFIGURAÇÃO PARA VALIDAÇÃO VISUAL DOS COMBOBOXES
        private void ConfigurarValidacaoVisual()
        {
            ComboBox[] combos = { cmbVendedor, cmbProduto, cmbTipoMeta, cmbPeriodicidade };

            foreach (ComboBox combo in combos)
            {
                // Mantém as configurações que você especificou
                combo.FlatStyle = FlatStyle.Flat;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.BackColor = Color.White;

                // Configura para desenho personalizado
                combo.DrawMode = DrawMode.OwnerDrawFixed;
                combo.DrawItem += ComboBox_DrawItem;

                // Inicializa sem erro
                combosComErro[combo] = false;
            }
        }

        // MÉTODO QUE DESENHA O FUNDO INTERNO VERMELHO DOS COMBOBOXES
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo == null || e.Index < 0) return;

            // Determina a cor de fundo baseada no estado de erro
            Color corFundo = (combosComErro.ContainsKey(combo) && combosComErro[combo])
                ? Color.FromArgb(252, 199, 194)  // Vermelho quando há erro
                : Color.White;                   // Branco quando normal

            // Desenha o fundo INTERNO completamente
            using (Brush brushFundo = new SolidBrush(corFundo))
            {
                e.Graphics.FillRectangle(brushFundo, e.Bounds);
            }

            // Desenha o texto do item
            string texto = combo.Items[e.Index].ToString();
            Color corTexto = Color.Black;

            // Se item está selecionado, usa cor de destaque
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                corTexto = SystemColors.HighlightText;
                using (Brush brushDestaque = new SolidBrush(SystemColors.Highlight))
                {
                    e.Graphics.FillRectangle(brushDestaque, e.Bounds);
                }
            }

            using (Brush brushTexto = new SolidBrush(corTexto))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };
                e.Graphics.DrawString(texto, combo.Font, brushTexto, e.Bounds, sf);
            }

            // Desenha o foco se necessário
            if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
            {
                e.DrawFocusRectangle();
            }
        }

        private void CadastroMetaForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    BtnSalvar_Click(sender, e);
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
            if (campo == null) return;

            if (campo is ComboBox combo)
            {
                // Remove o erro do ComboBox
                combosComErro[combo] = false;
                combo.BackColor = Color.White;
                combo.Invalidate();
                combo.Refresh();
            }
            else
            {
                // Remove erro de TextBox
                campo.BackColor = Color.White;
                campo.Invalidate();
                campo.Refresh();
            }
        }

        // MÉTODO PRINCIPAL PARA MARCAR CAMPOS COM ERRO
        private void MarcarCampoComErro(Control campo)
        {
            Color corErro = Color.FromArgb(252, 199, 194);

            if (campo is ComboBox combo)
            {
                // Marca o ComboBox como tendo erro
                combosComErro[combo] = true;

                // FORÇA o fundo a ficar vermelho
                combo.BackColor = corErro;
                combo.Invalidate(); // Força redesenho com a nova cor
                combo.Refresh();

                // Timer adicional para garantir que persista
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer
                {
                    Interval = 50
                };
                timer.Tick += (s, e) =>
                {
                    if (!combo.IsDisposed && combosComErro[combo])
                    {
                        combo.BackColor = corErro;
                        combo.Invalidate();
                    }
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            else if (campo is TextBox txt)
            {
                // Para TextBox funciona direto
                txt.BackColor = corErro;
                txt.Invalidate();
                txt.Refresh();

                // Timer de reforço para TextBox também
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 50;
                timer.Tick += (s, e) =>
                {
                    if (!txt.IsDisposed)
                    {
                        txt.BackColor = corErro;
                        txt.Invalidate();
                    }
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
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
                _toggleAtivoState = true;
                // Deixar todos os campos sem seleção inicial
            }
            catch (Exception)
            {
                string mensagemErro = "ERRO: Falha ao carregar dados do sistema.\n\n" +
                                     "DETALHE: Não foi possível conectar com o banco de dados.\n\n" +
                                     "DICA: Verifique a conexão e reinicie o sistema.";
                MessageBox.Show(mensagemErro, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarregarVendedores()
        {
            var vendedores = _vendedorService.ObterVendedoresParaComboBox();
            if (vendedores != null && vendedores.Any())
            {
                // Mapeia o dicionário para uma lista de ComboBoxItem
                var vendedoresParaExibir = vendedores.Select(v => new ComboBoxItem { Value = v.Key, Text = v.Value }).ToList();

                cmbVendedor.DataSource = vendedoresParaExibir;
                cmbVendedor.DisplayMember = "Text"; // Exibe a propriedade Text do ComboBoxItem (que é o nome)
                cmbVendedor.ValueMember = "Value";  // Usa a propriedade Value do ComboBoxItem (que é o ID)
            }
            cmbVendedor.SelectedIndex = -1;
        }

        private void CarregarProdutos()
        {
            var produtos = _produtoService.ObterProdutosParaComboBox();
            if (produtos != null && produtos.Any())
            {
                // Mapeia o dicionário para uma lista de ComboBoxItem
                var produtosParaExibir = produtos.Select(p => new ComboBoxItem { Value = p.Key, Text = p.Value }).ToList();

                cmbProduto.DataSource = produtosParaExibir;
                cmbProduto.DisplayMember = "Text"; // Exibe a propriedade Text do ComboBoxItem (que é o nome)
                cmbProduto.ValueMember = "Value";  // Usa a propriedade Value do ComboBoxItem (que é o ID)
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
                    string mensagemErro = "ERRO: Meta não encontrada.\n\n" +
                                         "DETALHE: A meta selecionada não existe mais no sistema.\n\n" +
                                         "DICA: Verifique a lista de metas e tente novamente.";
                    MessageBox.Show(mensagemErro, "Meta Não Encontrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                txtValor.Text = _metaAtual.Valor.ToString("F2", System.Globalization.CultureInfo.CurrentCulture);
                _toggleAtivoState = _metaAtual.Ativo;

                Text = "Editar Meta";
                btnSalvar.Text = "     Atualizar (F2)";

                Application.DoEvents();
                LimparCoresValidacao();
            }
            catch (Exception)
            {
                string mensagemErro = "ERRO: Falha ao carregar meta para edição.\n\n" +
                                     "DETALHE: Não foi possível recuperar os dados da meta.\n\n" +
                                     "DICA: Verifique a conexão e tente novamente.";
                MessageBox.Show(mensagemErro, "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                if (!int.TryParse(cmbVendedor.SelectedValue.ToString(), out int vendedorId))
                {
                    string mensagemErro = "ERRO: ID do vendedor inválido.\n\n" +
                                         "DETALHE: O vendedor selecionado contém dados inconsistentes.\n\n" +
                                         "DICA: Selecione novamente o vendedor da lista.";
                    MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(cmbProduto.SelectedValue.ToString(), out int produtoId))
                {
                    string mensagemErro = "ERRO: ID do produto inválido.\n\n" +
                                         "DETALHE: O produto selecionado contém dados inconsistentes.\n\n" +
                                         "DICA: Selecione novamente o produto da lista.";
                    MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var tipoMetaSelecionado = (TipoMeta)((ComboBoxItem)cmbTipoMeta.SelectedItem).Value;
                var periodicidadeSelecionada = (PeriodicidadeMeta)((ComboBoxItem)cmbPeriodicidade.SelectedItem).Value;

                // VALIDAR TIPO DE META COM PRODUTO USANDO O SERVICE
                if (!_metaService.ValidarTipoMetaProduto(tipoMetaSelecionado, produtoId, out string mensagemTipoMeta))
                {
                    MessageBox.Show(mensagemTipoMeta, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // VERIFICAR META DUPLICADA
                bool metaDuplicada = _metaService.VerificarMetaDuplicada(
                    vendedorId,
                    produtoId,
                    tipoMetaSelecionado,
                    periodicidadeSelecionada,
                    _metaId ?? 0
                );

                if (metaDuplicada)
                {
                    string mensagemErro = "ERRO: Meta duplicada detectada.\n\n" +
                                         "DETALHE: Já existe uma meta com as mesmas características para este vendedor e produto.\n\n" +
                                         "DICA: Verifique se não há uma meta igual já cadastrada ou altere os parâmetros.";
                    MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var meta = new Meta
                {
                    Id = _metaId ?? 0,
                    Nome = txtNome.Text.Trim(),
                    VendedorId = vendedorId,
                    ProdutoId = produtoId,
                    TipoMeta = tipoMetaSelecionado,
                    Periodicidade = periodicidadeSelecionada,
                    Valor = decimal.Parse(txtValor.Text.Replace('.', ',')),
                    Ativo = _toggleAtivoState
                };

                bool sucesso;

                if (_metaId.HasValue)
                {
                    sucesso = _metaService.AtualizarMeta(meta);
                    if (sucesso)
                    {
                        MessageBox.Show($"Meta '{meta.Nome}' atualizada com sucesso!", "Operação Realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    var novoId = _metaService.CriarMeta(meta);
                    if (novoId > 0)
                    {
                        MessageBox.Show($"Meta '{meta.Nome}' cadastrada com sucesso!", "Meta Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        sucesso = true;
                    }
                    else
                    {
                        string mensagemErro = "ERRO: Falha ao criar meta.\n\n" +
                                             "DETALHE: Não foi possível salvar a meta no banco de dados.\n\n" +
                                             "DICA: Verifique se já existe uma meta com o mesmo nome.";
                        MessageBox.Show(mensagemErro, "Erro ao Salvar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sucesso = false;
                    }
                }

                if (sucesso)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (InvalidOperationException ex)
            {
                // CAPTURA ERROS DE VALIDAÇÃO DO SERVICE
                MessageBox.Show(ex.Message, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                string mensagemErro = "ERRO: Falha crítica ao salvar meta.\n\n" +
                                     $"DETALHE: {ex.Message}\n\n" +
                                     "DICA: Verifique todos os campos e tente novamente.";
                MessageBox.Show(mensagemErro, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidarCampos()
        {
            LimparCoresValidacao();

            bool todosValidos = true;
            Control primeiroCampoComErro = null;
            var camposComErro = new System.Collections.Generic.List<string>();

            // USAR VALIDAÇÃO DOS CAMPOS OBRIGATÓRIOS DO SERVICE
            List<string> camposInvalidos;
            string nome = txtNome.Text.Trim();
            int vendedorId = cmbVendedor.SelectedValue != null ? (int)cmbVendedor.SelectedValue : 0;
            int produtoId = cmbProduto.SelectedValue != null ? (int)cmbProduto.SelectedValue : 0;
            TipoMeta tipoMeta = cmbTipoMeta.SelectedItem != null ? (TipoMeta)((ComboBoxItem)cmbTipoMeta.SelectedItem).Value : (TipoMeta)(-1);
            PeriodicidadeMeta periodicidade = cmbPeriodicidade.SelectedItem != null ? (PeriodicidadeMeta)((ComboBoxItem)cmbPeriodicidade.SelectedItem).Value : (PeriodicidadeMeta)(-1);

            decimal valor = 0;
            bool valorValido;

            if (!string.IsNullOrWhiteSpace(txtValor.Text))
            {
                string valorLimpo = txtValor.Text.Replace('.', ',');
                valorValido = decimal.TryParse(valorLimpo, out valor);

                // Se conseguiu fazer parse mas valor é <= 0, também é inválido
                if (valorValido && valor <= 0)
                {
                    valorValido = false;
                }
            }
            else
            {
                // Se campo está vazio, é inválido
                valorValido = false;
            }

            // Se valor não é válido, usar 0 para validação do service
            if (!valorValido)
            {
                valor = 0;
            }

            bool camposObrigatoriosValidos = _metaService.ValidarCamposObrigatorios(
                nome, vendedorId, produtoId, tipoMeta, valor, periodicidade, out camposInvalidos);

            if (!camposObrigatoriosValidos)
            {
                // Marcar campos com erro baseado na validação do service
                foreach (string campo in camposInvalidos)
                {
                    switch (campo)
                    {
                        case "Nome da Meta":
                            MarcarCampoComErro(txtNome);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = txtNome;
                            break;
                        case "Vendedor":
                            MarcarCampoComErro(cmbVendedor);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = cmbVendedor;
                            break;
                        case "Produto":
                            MarcarCampoComErro(cmbProduto);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = cmbProduto;
                            break;
                        case "Tipo de Meta":
                            MarcarCampoComErro(cmbTipoMeta);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = cmbTipoMeta;
                            break;
                        case "Periodicidade":
                            MarcarCampoComErro(cmbPeriodicidade);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = cmbPeriodicidade;
                            break;
                        case "Valor":
                            MarcarCampoComErro(txtValor);
                            if (primeiroCampoComErro == null) primeiroCampoComErro = txtValor;
                            break;
                    }
                }

                camposComErro.AddRange(camposInvalidos);
                todosValidos = false;
            }

            // VALIDAÇÃO ADICIONAL DE ASPAS SIMPLES NO NOME
            if (!string.IsNullOrWhiteSpace(nome) && nome.Contains("'"))
            {
                MarcarCampoComErro(txtNome);
                if (primeiroCampoComErro == null) primeiroCampoComErro = txtNome;
                todosValidos = false;

                primeiroCampoComErro?.Focus();
                string mensagemErro = "ERRO: Caractere inválido no nome.\n\n" +
                                     "DETALHE: O nome da meta não pode conter aspas simples (').\n\n" +
                                     "DICA: Remova as aspas simples do nome da meta.";
                MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Dar foco no primeiro campo com erro
            if (!todosValidos)
            {
                primeiroCampoComErro?.Focus();
                if (primeiroCampoComErro == txtValor && !string.IsNullOrWhiteSpace(txtValor.Text))
                {
                    txtValor.SelectAll();
                }

                // Mostrar mensagem de campos obrigatórios
                string camposTexto = string.Join(", ", camposComErro);
                string mensagemErro = "ERRO: Campos obrigatórios não preenchidos.\n\n" +
                                     $"DETALHE: Os seguintes campos são obrigatórios: {camposTexto}\n\n" +
                                     "DICA: Preencha todos os campos marcados em vermelho.";
                MessageBox.Show(mensagemErro, "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return todosValidos;
        }

        private void LimparCoresValidacao()
        {
            // Limpa TextBoxes
            txtNome.BackColor = Color.White;
            txtValor.BackColor = Color.White;
            txtNome.Invalidate();
            txtNome.Refresh();
            txtValor.Invalidate();
            txtValor.Refresh();

            // Limpa ComboBoxes
            ComboBox[] combos = { cmbVendedor, cmbProduto, cmbTipoMeta, cmbPeriodicidade };

            foreach (ComboBox combo in combos)
            {
                combosComErro[combo] = false; // Remove o estado de erro
                combo.BackColor = Color.White;
                combo.Invalidate(); // Força redesenho sem erro
                combo.Refresh();
            }

            Application.DoEvents();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permite apenas números, vírgula, ponto e backspace (campos numéricos)
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

        private void ToggleAtivo_Click(object sender, EventArgs e)
        {
            _toggleAtivoState = !_toggleAtivoState;
            AtualizarImagemToggle();
        }

        private void AtualizarImagemToggle()
        {
            if (_toggleAtivoState)
            {
                toggleAtivo.Image = Properties.Resources.SwitchButton_True;
            }
            else
            {
                toggleAtivo.Image = Properties.Resources.SwitchButton_False;
            }
        }

        private void CadastroMetaForm_Load(object sender, EventArgs e)
        {
            AtualizarImagemToggle();
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
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