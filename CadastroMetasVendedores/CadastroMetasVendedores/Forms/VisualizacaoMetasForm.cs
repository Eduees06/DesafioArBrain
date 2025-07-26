using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;
using System.IO;

namespace CadastroMetasVendedores.Forms
{
    public partial class VisualizacaoMetasForm : Form
    {
        private readonly MetaService _metaService;
        private readonly VendedorService _vendedorService;
        private readonly ProdutoService _produtoService;
        private bool _toggleState = false; // Estado do interruptor
        private List<string> _filtrosAtivos = new List<string>(); // Lista de filtros ativos
        private List<HistoricoOperacao> _historicoOperacoes = new List<HistoricoOperacao>(); // Histórico de operações

        public VisualizacaoMetasForm(MetaService metaService, VendedorService vendedorService, ProdutoService produtoService)
        {
            _metaService = metaService;
            _vendedorService = vendedorService;
            _produtoService = produtoService;

            InitializeComponent();
            ConfigurarEventos();
            LoadLogo();
            SetButtonIcons();
            CarregarDados();
            AtualizarExibicaoFiltros();
        }

        private void ConfigurarEventos()
        {
            // Eventos de teclado
            this.KeyDown += VisualizacaoMetasForm_KeyDown;

            // Eventos dos botões
            btnExcluir.Click += BtnExcluir_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnAdicionar.Click += BtnAdicionar_Click;
            btnVoltar.Click += BtnVoltar_Click;
            btnDuplicar.Click += BtnDuplicar_Click;
            btnLimparFiltros.Click += BtnLimparFiltros_Click;
            btnHistorico.Click += BtnHistorico_Click;

            // Eventos da busca
            txtBusca.KeyPress += TxtBusca_KeyPress;

            // Eventos do DataGridView
            dgvMetas.CellDoubleClick += DgvMetas_CellDoubleClick;
            dgvMetas.ColumnHeaderMouseClick += DgvMetas_ColumnHeaderMouseClick;

            // Evento do painel de fundo
            pnlBackground.Paint += PnlBackground_Paint;

            // Adicionar efeitos hover aos botões
            AddHoverEffects();
        }

        private void VisualizacaoMetasForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    RealizarBusca();
                    e.Handled = true;
                    break;
                case Keys.F2:
                    BtnAdicionar_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.F4:
                    BtnEditar_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    BtnExcluir_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    this.Close();
                    e.Handled = true;
                    break;
            }
        }

        private void BtnHistorico_Click(object sender, EventArgs e)
        {
            try
            {
                using (var historicoForm = new HistoricoOperacoesForm(_historicoOperacoes, _metaService))
                {
                    if (historicoForm.ShowDialog() == DialogResult.OK)
                    {
                        // Recarregar dados se alguma operação foi revertida
                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir histórico: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdicionarAoHistorico(TipoOperacao tipo, Meta meta, string descricao = null)
        {
            var operacao = new HistoricoOperacao
            {
                Id = Guid.NewGuid(),
                TipoOperacao = tipo,
                DataOperacao = DateTime.Now,
                MetaId = meta.Id,
                MetaNome = meta.Nome,
                VendedorNome = meta.Vendedor?.Nome ?? "N/A",
                ProdutoNome = meta.Produto?.Nome ?? "N/A",
                Descricao = descricao ?? $"{tipo} realizada em {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                DadosMeta = CloneMeta(meta) // Salvar cópia dos dados da meta
            };

            _historicoOperacoes.Insert(0, operacao); // Inserir no início da lista

            // Manter apenas os últimos 100 registros
            if (_historicoOperacoes.Count > 100)
            {
                _historicoOperacoes.RemoveRange(100, _historicoOperacoes.Count - 100);
            }
        }

        private Meta CloneMeta(Meta original)
        {
            return new Meta
            {
                Id = original.Id,
                Nome = original.Nome,
                VendedorId = original.VendedorId,
                ProdutoId = original.ProdutoId,
                TipoMeta = original.TipoMeta,
                Valor = original.Valor,
                Periodicidade = original.Periodicidade,
                Ativo = original.Ativo,
                DataCriacao = original.DataCriacao,
                Vendedor = original.Vendedor,
                Produto = original.Produto
            };
        }

        private void BtnDuplicar_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para duplicar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int metaId = Convert.ToInt32(dgvMetas.SelectedRows[0].Cells["Id"].Value);
                // Busca a meta original pela lista de todas as metas
                var metaOriginal = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);

                if (metaOriginal == null)
                {
                    MessageBox.Show("Meta não encontrada.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Criar nova meta baseada na original
                var novaMeta = new Meta
                {
                    Nome = metaOriginal.Nome + " (Cópia)",
                    VendedorId = metaOriginal.VendedorId,
                    ProdutoId = metaOriginal.ProdutoId,
                    TipoMeta = metaOriginal.TipoMeta,
                    Valor = metaOriginal.Valor,
                    Periodicidade = metaOriginal.Periodicidade,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };

                int resultado = _metaService.CriarMeta(novaMeta);
                bool sucesso = resultado > 0;

                if (sucesso)
                {
                    // Buscar a meta criada para adicionar ao histórico
                    var metaCriada = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == resultado);
                    if (metaCriada != null)
                    {
                        AdicionarAoHistorico(TipoOperacao.Adicao, metaCriada, $"Meta duplicada de '{metaOriginal.Nome}'");
                    }

                    MessageBox.Show("Meta duplicada com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarDados();
                }
                else
                {
                    MessageBox.Show("Erro ao duplicar meta.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao duplicar meta: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLimparFiltros_Click(object sender, EventArgs e)
        {
            txtBusca.Text = string.Empty;
            _filtrosAtivos.Clear();
            AtualizarExibicaoFiltros();
            CarregarDados();
        }

        private void AtualizarExibicaoFiltros()
        {
            flpFiltrosAtivos.Controls.Clear();

            if (!_filtrosAtivos.Any())
            {
                btnLimparFiltros.Visible = false;
                return;
            }

            btnLimparFiltros.Visible = true;

            foreach (var filtro in _filtrosAtivos)
            {
                var lblFiltro = new Label
                {
                    Text = $"✕ {filtro}",
                    BackColor = Color.FromArgb(255, 197, 36),
                    ForeColor = Color.Black,
                    Font = new Font("Montserrat", 8F, FontStyle.Bold),
                    Padding = new Padding(8, 4, 8, 4),
                    Margin = new Padding(0, 0, 5, 0),
                    AutoSize = true,
                    Cursor = Cursors.Hand,
                    Tag = filtro
                };

                lblFiltro.Click += (s, e) =>
                {
                    var filtroParaRemover = (s as Label)?.Tag?.ToString();
                    if (!string.IsNullOrEmpty(filtroParaRemover))
                    {
                        RemoverFiltro(filtroParaRemover);
                    }
                };

                flpFiltrosAtivos.Controls.Add(lblFiltro);
            }
        }

        private void RemoverFiltro(string filtro)
        {
            _filtrosAtivos.Remove(filtro);
            AtualizarExibicaoFiltros();
            AplicarFiltros();
        }

        private void DgvMetas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvMetas.DataSource == null) return;

            string columnName = dgvMetas.Columns[e.ColumnIndex].Name;
            var dataSource = dgvMetas.DataSource as System.Collections.Generic.List<object>;

            if (dataSource == null) return;

            try
            {
                var sortedData = dataSource.OrderBy(item =>
                {
                    var property = item.GetType().GetProperty(columnName);
                    var value = property?.GetValue(item);

                    // Tratamento especial para diferentes tipos
                    if (value is DateTime dt)
                        return (object)dt;
                    if (value is decimal dec)
                        return (object)dec;
                    if (value is int i)
                        return (object)i;

                    return (object)(value?.ToString() ?? "");
                }).ToList();

                dgvMetas.DataSource = sortedData;
                ConfigurarColunas();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao ordenar: {ex.Message}");
            }
        }

        private void PnlToggleSlider_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics g = e.Graphics;

            try
            {
                Image switchImage = null;

                // Carregar imagem do Resources
                if (_toggleState)
                {
                    try { switchImage = Properties.Resources.SwitchButton_True; } catch { }
                }
                else
                {
                    try { switchImage = Properties.Resources.SwitchButton_False; } catch { }
                }

                if (switchImage != null)
                {
                    // Desenhar a imagem redimensionada para caber no painel
                    g.DrawImage(switchImage, 0, 0, panel.Width, panel.Height);
                }
                else
                {
                    // Fallback: desenhar switch customizado se as imagens não estiverem disponíveis
                    Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                    Color bgColor = _toggleState ? Color.FromArgb(255, 197, 36) : Color.FromArgb(200, 200, 200);

                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        g.FillRoundedRectangle(brush, rect, 8);
                    }

                    // Desenhar a bolinha do interruptor
                    int circleSize = panel.Height - 4;
                    int circleX = _toggleState ? panel.Width - circleSize - 2 : 2;
                    int circleY = 2;

                    Rectangle circleRect = new Rectangle(circleX, circleY, circleSize, circleSize);
                    using (SolidBrush circleBrush = new SolidBrush(Color.White))
                    {
                        g.FillEllipse(circleBrush, circleRect);
                    }

                    using (Pen circlePen = new Pen(Color.Gray, 1))
                    {
                        g.DrawEllipse(circlePen, circleRect);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao desenhar switch: {ex.Message}");
            }
        }

        private void PnlToggleSlider_Click(object sender, EventArgs e)
        {
            _toggleState = !_toggleState;
            pnlToggleSlider.Invalidate(); // Redesenha o controle
            CarregarDados();
        }

        private void AddHoverEffects()
        {
            // Efeito hover para btnExcluir
            btnExcluir.MouseEnter += (s, e) => btnExcluir.BackColor = Color.FromArgb(255, 92, 92);
            btnExcluir.MouseLeave += (s, e) => btnExcluir.BackColor = Color.FromArgb(255, 72, 72);

            // Efeito hover para btnBuscar
            btnBuscar.MouseEnter += (s, e) => btnBuscar.BackColor = Color.FromArgb(40, 140, 140);
            btnBuscar.MouseLeave += (s, e) => btnBuscar.BackColor = Color.DarkCyan;

            // Efeito hover para btnEditar
            btnEditar.MouseEnter += (s, e) => btnEditar.BackColor = Color.FromArgb(71, 87, 105);
            btnEditar.MouseLeave += (s, e) => btnEditar.BackColor = Color.FromArgb(51, 67, 85);

            // Efeito hover para btnVoltar
            btnVoltar.MouseEnter += (s, e) => btnVoltar.BackColor = Color.FromArgb(71, 87, 105);
            btnVoltar.MouseLeave += (s, e) => btnVoltar.BackColor = Color.FromArgb(51, 67, 85);

            // Efeito hover para btnAdicionar
            btnAdicionar.MouseEnter += (s, e) => btnAdicionar.BackColor = Color.FromArgb(43, 201, 48);
            btnAdicionar.MouseLeave += (s, e) => btnAdicionar.BackColor = Color.FromArgb(23, 181, 28);

            // Efeito hover para btnDuplicar
            btnDuplicar.MouseEnter += (s, e) => btnDuplicar.BackColor = Color.FromArgb(71, 87, 105);
            btnDuplicar.MouseLeave += (s, e) => btnDuplicar.BackColor = Color.FromArgb(51, 67, 85);

            // Efeito hover para btnLimparFiltros
            btnLimparFiltros.MouseEnter += (s, e) => btnLimparFiltros.BackColor = Color.FromArgb(71, 87, 105);
            btnLimparFiltros.MouseLeave += (s, e) => btnLimparFiltros.BackColor = Color.FromArgb(51, 67, 85);

            // Efeito hover para btnHistorico
            btnHistorico.MouseEnter += (s, e) => btnHistorico.BackColor = Color.FromArgb(71, 87, 105);
            btnHistorico.MouseLeave += (s, e) => btnHistorico.BackColor = Color.FromArgb(51, 67, 85);
        }

        private void SetButtonIcons()
        {
            SetButtonIcon(btnExcluir, "icone_excluir.png", ContentAlignment.MiddleRight);
            SetButtonIcon(btnBuscar, "icone_buscar.png", ContentAlignment.MiddleRight);
            SetButtonIcon(btnEditar, "icone_editar.png", ContentAlignment.MiddleRight);
            SetButtonIcon(btnAdicionar, "icone_adicionar.png", ContentAlignment.MiddleRight);
            SetButtonIcon(btnVoltar, "icone_voltar.png", ContentAlignment.MiddleLeft);
            SetButtonIcon(btnDuplicar, "icone_duplicar.png", ContentAlignment.MiddleRight);
            SetButtonIcon(btnHistorico, "icone_historico.png", ContentAlignment.MiddleRight);
        }

        private void SetButtonIcon(Button button, string iconResourceName, ContentAlignment alignment)
        {
            try
            {
                Image icon = null;

                // Tenta carregar do Resources primeiro
                switch (iconResourceName)
                {
                    case "icone_buscar.png":
                        try { icon = Properties.Resources.icone_buscar; } catch { }
                        break;
                    case "icone_adicionar.png":
                        try { icon = Properties.Resources.icone_adicionar; } catch { }
                        break;
                    case "icone_voltar.png":
                        try { icon = Properties.Resources.icone_voltar; } catch { }
                        break;
                    case "icone_excluir.png":
                        try { icon = Properties.Resources.icone_excluir; } catch { }
                        break;
                    case "icone_editar.png":
                        try { icon = Properties.Resources.icone_editar; } catch { }
                        break;
                    case "icone_duplicar.png":
                        try { icon = Properties.Resources.icone_duplicar; } catch { }
                        break;
                    case "icone_historico.png":
                        try { icon = Properties.Resources.icone_historico; } catch { }
                        break;
                }

                // Se não encontrou no Resources, tenta nos arquivos
                if (icon == null)
                {
                    string[] possiblePaths = {
                        Path.Combine(Application.StartupPath, "Assets", "Icons", iconResourceName),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Icons", iconResourceName),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", iconResourceName),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "Icons", iconResourceName)
                    };

                    foreach (string path in possiblePaths)
                    {
                        if (File.Exists(path))
                        {
                            icon = Image.FromFile(path);
                            break;
                        }
                    }
                }

                if (icon != null)
                {
                    // Redimensiona o ícone proporcionalmente
                    Image resizedIcon = new Bitmap(icon, new Size(16, 16));
                    button.Image = resizedIcon;
                    button.ImageAlign = alignment;

                    // Ajusta o espaçamento entre texto e ícone para centralização
                    if (alignment == ContentAlignment.MiddleLeft)
                    {
                        button.TextImageRelation = TextImageRelation.ImageBeforeText;
                        button.Padding = new Padding(5, 0, 0, 0);
                    }
                    else if (alignment == ContentAlignment.MiddleRight)
                    {
                        button.TextImageRelation = TextImageRelation.TextBeforeImage;
                        button.Padding = new Padding(0, 0, 5, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar ícone {iconResourceName}: {ex.Message}");
            }
        }

        private void LoadLogo()
        {
            try
            {
                Image logo = null;

                // Tenta carregar do Resources primeiro
                try
                {
                    logo = Properties.Resources.VectorArBrain;
                }
                catch { }

                // Se não encontrou no Resources, tenta nos arquivos
                if (logo == null)
                {
                    string[] possiblePaths = {
                        Path.Combine(Application.StartupPath, "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "VectorArBrain.png"),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "Icons", "VectorArBrain.png")
                    };

                    foreach (string path in possiblePaths)
                    {
                        if (File.Exists(path))
                        {
                            logo = Image.FromFile(path);
                            break;
                        }
                    }
                }

                if (logo != null)
                {
                    picLogo.Image = logo;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Logo não encontrada: VectorArBrain.png");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar logo: {ex.Message}");
            }
        }

        private void PnlBackground_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(255, 197, 36), 2)) // #ffc524
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void CarregarDados()
        {
            try
            {
                var metas = _toggleState ?
                    _metaService.ObterTodasMetas() :
                    _metaService.ObterMetasAtivas();

                if (_filtrosAtivos.Any())
                {
                    metas = AplicarFiltrosNasMetas(metas);
                }

                CarregarGrid(metas);
                AtualizarTotalRegistros(metas.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<Meta> AplicarFiltrosNasMetas(IEnumerable<Meta> metas)
        {
            var metasFiltradas = metas;

            foreach (var filtro in _filtrosAtivos)
            {
                metasFiltradas = metasFiltradas.Where(m =>
                    (m.Nome != null && m.Nome.ToUpper().Contains(filtro.ToUpper())) ||
                    (m.Vendedor != null && m.Vendedor.Nome.ToUpper().Contains(filtro.ToUpper())) ||
                    (m.Produto != null && m.Produto.Nome.ToUpper().Contains(filtro.ToUpper())) ||
                    ObterDescricaoTipoMeta(m.TipoMeta).ToUpper().Contains(filtro.ToUpper()) ||
                    ObterDescricaoPeriodicidade(m.Periodicidade).ToUpper().Contains(filtro.ToUpper()) ||
                    _metaService.FormatarValorMeta(m.Valor, m.TipoMeta).ToUpper().Contains(filtro.ToUpper()) ||
                    m.DataCriacao.ToString("dd/MM/yyyy").Contains(filtro) ||
                    (m.Ativo ? "Ativo" : "Inativo").ToUpper().Contains(filtro.ToUpper())
                );
            }

            return metasFiltradas;
        }

        private void CarregarGrid(System.Collections.Generic.IEnumerable<Meta> metas)
        {
            dgvMetas.DataSource = null;

            if (!metas.Any())
            {
                lblMensagemVazia.Visible = true;
                dgvMetas.Visible = false;
                return;
            }

            lblMensagemVazia.Visible = false;
            dgvMetas.Visible = true;

            var dadosGrid = metas.Select(m => new
            {
                Id = m.Id,
                Nome = m.Nome ?? "N/A",
                Vendedor = m.Vendedor?.Nome ?? "N/A",
                Produto = m.Produto?.Nome ?? "N/A",
                TipoMeta = ObterDescricaoTipoMeta(m.TipoMeta),
                Valor = _metaService.FormatarValorMeta(m.Valor, m.TipoMeta),
                Periodicidade = ObterDescricaoPeriodicidade(m.Periodicidade),
                DataCriacao = m.DataCriacao.ToString("dd/MM/yyyy"),
                Status = m.Ativo ? "Ativo" : "Inativo"
            }).ToList();

            dgvMetas.DataSource = dadosGrid;
            ConfigurarColunas();

            // Colorir linhas inativas
            foreach (DataGridViewRow row in dgvMetas.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Inativo")
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void ConfigurarColunas()
        {
            // Configurar colunas
            if (dgvMetas.Columns["Id"] != null)
                dgvMetas.Columns["Id"].Visible = false;

            if (dgvMetas.Columns["Nome"] != null)
            {
                dgvMetas.Columns["Nome"].HeaderText = "Nome da Meta";
                dgvMetas.Columns["Nome"].Width = 150;
            }

            if (dgvMetas.Columns["Vendedor"] != null)
            {
                dgvMetas.Columns["Vendedor"].HeaderText = "Vendedor";
                dgvMetas.Columns["Vendedor"].Width = 120;
            }

            if (dgvMetas.Columns["Produto"] != null)
            {
                dgvMetas.Columns["Produto"].HeaderText = "Produto";
                dgvMetas.Columns["Produto"].Width = 120;
            }

            if (dgvMetas.Columns["TipoMeta"] != null)
            {
                dgvMetas.Columns["TipoMeta"].HeaderText = "Tipo";
                dgvMetas.Columns["TipoMeta"].Width = 80;
            }

            if (dgvMetas.Columns["Valor"] != null)
            {
                dgvMetas.Columns["Valor"].HeaderText = "Valor";
                dgvMetas.Columns["Valor"].Width = 100;
            }

            if (dgvMetas.Columns["Periodicidade"] != null)
            {
                dgvMetas.Columns["Periodicidade"].HeaderText = "Periodicidade";
                dgvMetas.Columns["Periodicidade"].Width = 100;
            }

            if (dgvMetas.Columns["DataCriacao"] != null)
            {
                dgvMetas.Columns["DataCriacao"].HeaderText = "Data Criação";
                dgvMetas.Columns["DataCriacao"].Width = 90;
            }

            if (dgvMetas.Columns["Status"] != null)
            {
                dgvMetas.Columns["Status"].HeaderText = "Status";
                dgvMetas.Columns["Status"].Width = 70;
            }
        }

        private void AtualizarTotalRegistros(int total)
        {
            string texto = _toggleState ?
                $"Total de registros: {total}" :
                $"Total de registros ativos: {total}";

            lblTotalRegistros.Text = texto;
        }

        private string ObterDescricaoTipoMeta(TipoMeta tipoMeta)
        {
            switch (tipoMeta)
            {
                case TipoMeta.Monetario: return "Monetário";
                case TipoMeta.Litros: return "Litros";
                case TipoMeta.Unidades: return "Unidades";
                default: return "Desconhecido";
            }
        }

        private string ObterDescricaoPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            switch (periodicidade)
            {
                case PeriodicidadeMeta.Diaria: return "Diária";
                case PeriodicidadeMeta.Semanal: return "Semanal";
                case PeriodicidadeMeta.Mensal: return "Mensal";
                default: return "Desconhecida";
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new CadastroMetaForm(_metaService, _vendedorService, _produtoService))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Buscar a meta recém-criada para adicionar ao histórico
                        var metas = _metaService.ObterTodasMetas().OrderByDescending(m => m.DataCriacao).FirstOrDefault();
                        if (metas != null)
                        {
                            AdicionarAoHistorico(TipoOperacao.Adicao, metas);
                        }
                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir formulário de cadastro: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int metaId = Convert.ToInt32(dgvMetas.SelectedRows[0].Cells["Id"].Value);

                // Salvar estado antes da edição
                var metaAntes = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);

                using (var form = new CadastroMetaForm(_metaService, _vendedorService, _produtoService, metaId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        // Buscar meta após edição para histórico
                        var metaDepois = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);
                        if (metaDepois != null)
                        {
                            AdicionarAoHistorico(TipoOperacao.Edicao, metaDepois, $"Meta editada - dados anteriores salvos");
                        }
                        CarregarDados();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao abrir formulário de edição: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvMetas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma meta para excluir.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int metaId = Convert.ToInt32(dgvMetas.SelectedRows[0].Cells["Id"].Value);
                string nome = dgvMetas.SelectedRows[0].Cells["Nome"].Value?.ToString();
                string vendedor = dgvMetas.SelectedRows[0].Cells["Vendedor"].Value?.ToString();

                // Salvar dados da meta antes da exclusão
                var metaParaExcluir = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);

                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir a meta '{nome}' do vendedor '{vendedor}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool sucesso = _metaService.ExcluirMeta(metaId);

                    if (sucesso)
                    {
                        if (metaParaExcluir != null)
                        {
                            AdicionarAoHistorico(TipoOperacao.Exclusao, metaParaExcluir, $"Meta '{nome}' excluída");
                        }

                        MessageBox.Show("Meta excluída com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarDados();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir meta.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir meta: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusca();
        }

        private void RealizarBusca()
        {
            try
            {
                string filtro = txtBusca.Text.Trim();

                if (!string.IsNullOrEmpty(filtro) && !_filtrosAtivos.Contains(filtro))
                {
                    _filtrosAtivos.Add(filtro);
                    txtBusca.Text = string.Empty; // Limpar campo de busca após adicionar filtro
                    AtualizarExibicaoFiltros();
                }

                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar busca: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                System.Collections.Generic.IEnumerable<Meta> metas;

                metas = _toggleState ?
                    _metaService.ObterTodasMetas() :
                    _metaService.ObterMetasAtivas();

                if (_filtrosAtivos.Any())
                {
                    metas = AplicarFiltrosNasMetas(metas);
                }

                CarregarGrid(metas);
                AtualizarTotalRegistros(metas.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar filtros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtBusca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RealizarBusca();
            }
        }

        private void DgvMetas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditar_Click(sender, e);
            }
        }
    }

    // Classes para o histórico de operações
    public enum TipoOperacao
    {
        Adicao,
        Edicao,
        Exclusao
    }

    public class HistoricoOperacao
    {
        public Guid Id { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public DateTime DataOperacao { get; set; }
        public int MetaId { get; set; }
        public string MetaNome { get; set; }
        public string VendedorNome { get; set; }
        public string ProdutoNome { get; set; }
        public string Descricao { get; set; }
        public Meta DadosMeta { get; set; } // Dados da meta para permitir reversão
    }

    // Extensão para desenhar retângulos arredondados
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                graphics.FillPath(brush, path);
            }
        }

        private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            System.Drawing.Drawing2D.GraphicsPath roundedRect = new System.Drawing.Drawing2D.GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddArc(rect.X, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.CloseAllFigures();
            return roundedRect;
        }
    }
}