using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;
using CadastroMetasVendedores.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CadastroMetasVendedores.Forms
{
    public partial class VisualizacaoMetasForm : Form
    {
        private readonly MetaService _metaService;
        private readonly VendedorService _vendedorService;
        private readonly ProdutoService _produtoService;
        private bool _toggleState = false;
        private readonly List<string> _filtrosAtivos = new List<string>();
        private List<HistoricoOperacao> _historicoOperacoes = new List<HistoricoOperacao>();
        private string _ultimaColunaOrdenada = "";
        private bool _ordemCrescente = true;

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
            this.KeyDown += VisualizacaoMetasForm_KeyDown;

            btnExcluir.Click += BtnExcluir_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnAdicionar.Click += BtnAdicionar_Click;
            btnVoltar.Click += BtnVoltar_Click;
            btnDuplicar.Click += BtnDuplicar_Click;
            btnLimparFiltros.Click += BtnLimparFiltros_Click;
            btnHistorico.Click += BtnHistorico_Click;

            txtBusca.KeyPress += TxtBusca_KeyPress;

            dgvMetas.CellDoubleClick += DgvMetas_CellDoubleClick;
            dgvMetas.ColumnHeaderMouseClick += DgvMetas_ColumnHeaderMouseClick;

            pnlBackground.Paint += PnlBackground_Paint;

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
                using (var historicoForm = new HistoricoOperacoesForm1(_historicoOperacoes, _metaService))
                {
                    if (historicoForm.ShowDialog() == DialogResult.OK)
                    {
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
                DadosMeta = CloneMeta(meta)
            };

            _historicoOperacoes.Insert(0, operacao);

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
                var metaOriginal = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);

                if (metaOriginal == null)
                {
                    MessageBox.Show("Meta não encontrada.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

            if (_ultimaColunaOrdenada == columnName)
            {
                _ordemCrescente = !_ordemCrescente;
            }
            else
            {
                _ultimaColunaOrdenada = columnName;
                _ordemCrescente = true;
            }

            OrdenarDados(columnName, _ordemCrescente);
            AtualizarIndicadorOrdenacao(columnName, _ordemCrescente);
        }

        private void OrdenarDados(string columnName, bool crescente)
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

                IEnumerable<Meta> metasOrdenadas;

                // Substituindo switch expressions por switch tradicional
                switch (columnName)
                {
                    case "Nome":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.Nome ?? "") :
                            metas.OrderByDescending(m => m.Nome ?? "");
                        break;
                    case "Vendedor":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.Vendedor?.Nome ?? "") :
                            metas.OrderByDescending(m => m.Vendedor?.Nome ?? "");
                        break;
                    case "Produto":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.Produto?.Nome ?? "") :
                            metas.OrderByDescending(m => m.Produto?.Nome ?? "");
                        break;
                    case "TipoMeta":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => ObterDescricaoTipoMeta(m.TipoMeta)) :
                            metas.OrderByDescending(m => ObterDescricaoTipoMeta(m.TipoMeta));
                        break;
                    case "Valor":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.Valor) :
                            metas.OrderByDescending(m => m.Valor);
                        break;
                    case "Periodicidade":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => ObterDescricaoPeriodicidade(m.Periodicidade)) :
                            metas.OrderByDescending(m => ObterDescricaoPeriodicidade(m.Periodicidade));
                        break;
                    case "DataCriacao":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.DataCriacao) :
                            metas.OrderByDescending(m => m.DataCriacao);
                        break;
                    case "Status":
                        metasOrdenadas = crescente ?
                            metas.OrderBy(m => m.Ativo ? "Ativo" : "Inativo") :
                            metas.OrderByDescending(m => m.Ativo ? "Ativo" : "Inativo");
                        break;
                    default:
                        metasOrdenadas = metas;
                        break;
                }

                CarregarGrid(metasOrdenadas);
                AtualizarTotalRegistros(metasOrdenadas.Count());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao ordenar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarIndicadorOrdenacao(string columnName, bool crescente)
        {
            foreach (DataGridViewColumn col in dgvMetas.Columns)
            {
                if (col.HeaderText.EndsWith(" ↑") || col.HeaderText.EndsWith(" ↓"))
                {
                    col.HeaderText = col.HeaderText.Substring(0, col.HeaderText.Length - 2);
                }
            }

            if (dgvMetas.Columns[columnName] != null)
            {
                string indicador = crescente ? " ↑" : " ↓";
                dgvMetas.Columns[columnName].HeaderText += indicador;
            }
        }

        private void PnlToggleSlider_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics g = e.Graphics;

            try
            {
                Image switchImage = null;

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
                    g.DrawImage(switchImage, 0, 0, panel.Width, panel.Height);
                }
                else
                {
                    Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
                    Color bgColor = _toggleState ? Color.FromArgb(255, 197, 36) : Color.FromArgb(200, 200, 200);

                    using (SolidBrush brush = new SolidBrush(bgColor))
                    {
                        g.FillRoundedRectangle(brush, rect, 8);
                    }

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
            pnlToggleSlider.Invalidate();
            CarregarDados();
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
                m.Id,
                Nome = m.Nome ?? "N/A",
                Vendedor = m.Vendedor?.Nome ?? "N/A",
                Produto = m.Produto?.Nome ?? "N/A",
                TipoMeta = ObterDescricaoTipoMeta(m.TipoMeta),
                Valor = _metaService.FormatarValorMeta(m.Valor, m.TipoMeta),
                Periodicidade = ObterDescricaoPeriodicidade(m.Periodicidade),
                DataCriacao = m.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss"),
                Status = m.Ativo ? "Ativo" : "Inativo"
            }).ToList();

            dgvMetas.DataSource = dadosGrid;
            ConfigurarColunas();

            foreach (DataGridViewRow row in dgvMetas.Rows)
            {
                if (row.Cells["Status"].Value?.ToString() == "Inativo")
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }

            // Configurar cursor de mãozinha nos cabeçalhos
            dgvMetas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(71, 87, 105);
            dgvMetas.EnableHeadersVisualStyles = false;
        }

        private void AddHoverEffects()
        {
            btnExcluir.MouseEnter += (s, e) => btnExcluir.BackColor = Color.FromArgb(255, 92, 92);
            btnExcluir.MouseLeave += (s, e) => btnExcluir.BackColor = Color.FromArgb(255, 72, 72);

            btnBuscar.MouseEnter += (s, e) => btnBuscar.BackColor = Color.FromArgb(40, 140, 140);
            btnBuscar.MouseLeave += (s, e) => btnBuscar.BackColor = Color.DarkCyan;

            btnEditar.MouseEnter += (s, e) => btnEditar.BackColor = Color.FromArgb(71, 87, 105);
            btnEditar.MouseLeave += (s, e) => btnEditar.BackColor = Color.FromArgb(51, 67, 85);

            btnVoltar.MouseEnter += (s, e) => btnVoltar.BackColor = Color.FromArgb(71, 87, 105);
            btnVoltar.MouseLeave += (s, e) => btnVoltar.BackColor = Color.FromArgb(51, 67, 85);

            btnAdicionar.MouseEnter += (s, e) => btnAdicionar.BackColor = Color.FromArgb(43, 201, 48);
            btnAdicionar.MouseLeave += (s, e) => btnAdicionar.BackColor = Color.FromArgb(23, 181, 28);

            btnDuplicar.MouseEnter += (s, e) => btnDuplicar.BackColor = Color.FromArgb(71, 87, 105);
            btnDuplicar.MouseLeave += (s, e) => btnDuplicar.BackColor = Color.FromArgb(51, 67, 85);

            btnLimparFiltros.MouseEnter += (s, e) => btnLimparFiltros.BackColor = Color.FromArgb(71, 87, 105);
            btnLimparFiltros.MouseLeave += (s, e) => btnLimparFiltros.BackColor = Color.FromArgb(51, 67, 85);

            btnHistorico.MouseEnter += (s, e) => btnHistorico.BackColor = Color.FromArgb(71, 87, 105);
            btnHistorico.MouseLeave += (s, e) => btnHistorico.BackColor = Color.FromArgb(51, 67, 85);
        }

        private void SetButtonIcon(Button button, string iconResourceName, ContentAlignment alignment)
        {
            try
            {
                Image icon = GetIconFromResources(iconResourceName) ?? GetIconFromFile(iconResourceName);

                if (icon != null)
                {
                    Image resizedIcon = new Bitmap(icon, new Size(16, 16));
                    button.Image = resizedIcon;
                    button.ImageAlign = alignment;

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

        private Image GetIconFromResources(string iconResourceName)
        {
            try
            {
                // Substituindo switch expression por switch tradicional
                switch (iconResourceName)
                {
                    case "icone_buscar.png":
                        return Properties.Resources.icone_buscar;
                    case "icone_adicionar.png":
                        return Properties.Resources.icone_adicionar;
                    case "icone_voltar.png":
                        return Properties.Resources.icone_voltar;
                    case "icone_excluir.png":
                        return Properties.Resources.icone_excluir;
                    case "icone_editar.png":
                        return Properties.Resources.icone_editar;
                    case "icone_duplicar.png":
                        return Properties.Resources.icone_duplicar;
                    case "icone_historico.png":
                        return Properties.Resources.icone_historico;
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private Image GetIconFromFile(string iconResourceName)
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
                    return Image.FromFile(path);
                }
            }
            return null;
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

        private void LoadLogo()
        {
            try
            {
                Image logo = null;

                try { logo = Properties.Resources.VectorArBrain; } catch { }

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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar logo: {ex.Message}");
            }
        }

        private void PnlBackground_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            using (Pen pen = new Pen(Color.FromArgb(255, 197, 36), 2))
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
                    m.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss").Contains(filtro) ||
                    (m.Ativo ? "Ativo" : "Inativo").ToUpper().Contains(filtro.ToUpper())
                );
            }

            return metasFiltradas;
        }

        private void ConfigurarColunas()
        {
            var colunas = new Dictionary<string, (string header, int width)>
            {
                ["Id"] = ("", 0),
                ["Nome"] = ("Nome da Meta", 150),
                ["Vendedor"] = ("Vendedor", 120),
                ["Produto"] = ("Produto", 120),
                ["TipoMeta"] = ("Tipo", 80),
                ["Valor"] = ("Valor", 100),
                ["Periodicidade"] = ("Periodicidade", 100),
                ["DataCriacao"] = ("Data Criação", 130),
                ["Status"] = ("Status", 70)
            };

            foreach (var coluna in colunas)
            {
                string key = coluna.Key;
                string header = coluna.Value.header;
                int width = coluna.Value.width;

                if (dgvMetas.Columns[key] != null)
                {
                    dgvMetas.Columns[key].HeaderText = header;
                    dgvMetas.Columns[key].Width = width;
                    dgvMetas.Columns[key].Visible = key != "Id";
                    if (key == "Valor")
                    {
                        dgvMetas.Columns[key].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else if (key == "DataCriacao")
                    {
                        dgvMetas.Columns[key].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //
                    }
                }
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
                case TipoMeta.Monetario:
                    return "Monetário";
                case TipoMeta.Litros:
                    return "Litros";
                case TipoMeta.Unidades:
                    return "Unidades";
                default:
                    return "Desconhecido";
            }
        }

        private string ObterDescricaoPeriodicidade(PeriodicidadeMeta periodicidade)
        {
            switch (periodicidade)
            {
                case PeriodicidadeMeta.Diaria:
                    return "Diária";
                case PeriodicidadeMeta.Semanal:
                    return "Semanal";
                case PeriodicidadeMeta.Mensal:
                    return "Mensal";
                default:
                    return "Desconhecida";
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

                var metaOriginal = _metaService.ObterTodasMetas().FirstOrDefault(m => m.Id == metaId);
                if (metaOriginal == null)
                {
                    MessageBox.Show("Meta não encontrada.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var vendedorOriginal = metaOriginal.Vendedor ?? _vendedorService.ObterVendedorPorId(metaOriginal.VendedorId);
                var produtoOriginal = metaOriginal.Produto ?? _produtoService.ObterProdutoPorId(metaOriginal.ProdutoId);

                var dadosAnteriores = new Meta
                {
                    Id = metaOriginal.Id,
                    Nome = metaOriginal.Nome,
                    VendedorId = metaOriginal.VendedorId,
                    ProdutoId = metaOriginal.ProdutoId,
                    TipoMeta = metaOriginal.TipoMeta,
                    Valor = metaOriginal.Valor,
                    Periodicidade = metaOriginal.Periodicidade,
                    Ativo = metaOriginal.Ativo,
                    DataCriacao = metaOriginal.DataCriacao,
                    Vendedor = vendedorOriginal,
                    Produto = produtoOriginal
                };

                using (var form = new CadastroMetaForm(_metaService, _vendedorService, _produtoService, metaId))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        AdicionarAoHistorico(TipoOperacao.Edicao, dadosAnteriores,
                            $"Meta '{metaOriginal.Nome}' editada - estado anterior salvo para reversão");
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
                    txtBusca.Text = string.Empty;
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
        public Meta DadosMeta { get; set; }
    }

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