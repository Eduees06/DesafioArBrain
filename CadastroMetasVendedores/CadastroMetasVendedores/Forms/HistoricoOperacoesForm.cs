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
    public partial class HistoricoOperacoesForm1 : Form
    {
        private readonly List<HistoricoOperacao> _operacoes;
        private readonly MetaService _metaService;

        public HistoricoOperacoesForm1(List<HistoricoOperacao> operacoes, MetaService metaService)
        {
            _operacoes = operacoes;
            _metaService = metaService;
            InitializeComponent();
            CarregarHistorico();
            SetButtonIcon();
        }

        private void SetButtonIcon()
        {
            try
            {
                Image icon = null;

                // Tenta carregar do Resources primeiro
                try
                {
                    icon = Properties.Resources.icone_voltar;
                }
                catch { }

                // Se não encontrou no Resources, tenta nos arquivos
                if (icon == null)
                {
                    string[] possiblePaths = {
                        Path.Combine(Application.StartupPath, "Assets", "Icons", "icone_voltar.png"),
                        Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Icons", "icone_voltar.png"),
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Icons", "icone_voltar.png"),
                        Path.Combine(Environment.CurrentDirectory, "Assets", "Icons", "icone_voltar.png")
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
                    btnVoltar.Image = resizedIcon;
                    btnVoltar.ImageAlign = ContentAlignment.MiddleLeft;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar ícone de voltar: {ex.Message}");
            }
        }

        private void CarregarHistorico()
        {
            var dadosGrid = _operacoes.Select(op => new
            {
                op.Id,
                TipoOperacao = ObterDescricaoOperacao(op.TipoOperacao),
                DataOperacao = op.DataOperacao.ToString("dd/MM/yyyy HH:mm:ss"),
                op.MetaNome,
                op.VendedorNome,
                op.ProdutoNome,
                op.Descricao
            }).ToList();

            dgvHistorico.DataSource = dadosGrid;
            ConfigurarColunasHistorico();
            lblTotalOperacoes.Text = $"Total de operações: {_operacoes.Count}";
        }

        private void ConfigurarColunasHistorico()
        {
            if (dgvHistorico.Columns["Id"] != null)
                dgvHistorico.Columns["Id"].Visible = false;

            if (dgvHistorico.Columns["TipoOperacao"] != null)
            {
                dgvHistorico.Columns["TipoOperacao"].HeaderText = "Tipo";
                dgvHistorico.Columns["TipoOperacao"].Width = 80;
            }

            if (dgvHistorico.Columns["DataOperacao"] != null)
            {
                dgvHistorico.Columns["DataOperacao"].HeaderText = "Data/Hora";
                dgvHistorico.Columns["DataOperacao"].Width = 130;
            }

            if (dgvHistorico.Columns["MetaNome"] != null)
            {
                dgvHistorico.Columns["MetaNome"].HeaderText = "Meta";
                dgvHistorico.Columns["MetaNome"].Width = 120;
            }

            if (dgvHistorico.Columns["VendedorNome"] != null)
            {
                dgvHistorico.Columns["VendedorNome"].HeaderText = "Vendedor";
                dgvHistorico.Columns["VendedorNome"].Width = 100;
            }

            if (dgvHistorico.Columns["ProdutoNome"] != null)
            {
                dgvHistorico.Columns["ProdutoNome"].HeaderText = "Produto";
                dgvHistorico.Columns["ProdutoNome"].Width = 100;
            }

            if (dgvHistorico.Columns["Descricao"] != null)
            {
                dgvHistorico.Columns["Descricao"].HeaderText = "Descrição";
                dgvHistorico.Columns["Descricao"].Width = 150;
            }
        }

        private string ObterDescricaoOperacao(TipoOperacao tipo)
        {
            switch (tipo)
            {
                case TipoOperacao.Adicao: return "Adição";
                case TipoOperacao.Edicao: return "Edição";
                case TipoOperacao.Exclusao: return "Exclusão";
                default: return "Desconhecido";
            }
        }

        private void BtnReverter_Click(object sender, EventArgs e)
        {
            if (dgvHistorico.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma operação para reverter.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var operacaoId = (Guid)dgvHistorico.SelectedRows[0].Cells["Id"].Value;
                var operacao = _operacoes.FirstOrDefault(op => op.Id == operacaoId);

                if (operacao == null)
                {
                    MessageBox.Show("Operação não encontrada.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string mensagemConfirmacao = "";
                switch (operacao.TipoOperacao)
                {
                    case TipoOperacao.Adicao:
                        mensagemConfirmacao = $"Deseja realmente reverter a adição da meta '{operacao.MetaNome}'?\nA meta será excluída.";
                        break;
                    case TipoOperacao.Exclusao:
                        mensagemConfirmacao = $"Deseja realmente reverter a exclusão da meta '{operacao.MetaNome}'?\nA meta será recriada.";
                        break;
                    case TipoOperacao.Edicao:
                        mensagemConfirmacao = $"Deseja realmente reverter a edição da meta '{operacao.MetaNome}'?\nA meta voltará ao estado anterior à edição.";
                        break;
                }

                var resultado = MessageBox.Show(mensagemConfirmacao, "Confirmar Reversão",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    bool sucesso = ReverterOperacao(operacao);

                    if (sucesso)
                    {
                        MessageBox.Show("Operação revertida com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Remover operação do histórico
                        _operacoes.Remove(operacao);
                        CarregarHistorico();

                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao reverter operação.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao reverter operação: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ReverterOperacao(HistoricoOperacao operacao)
        {
            try
            {
                switch (operacao.TipoOperacao)
                {
                    case TipoOperacao.Adicao:
                        // Reverter adição = excluir a meta
                        return _metaService.ExcluirMeta(operacao.MetaId);

                    case TipoOperacao.Exclusao:
                        // Reverter exclusão = recriar a meta
                        if (operacao.DadosMeta != null)
                        {
                            // Criar nova meta com os dados salvos
                            var novaMeta = new Meta
                            {
                                Nome = operacao.DadosMeta.Nome,
                                VendedorId = operacao.DadosMeta.VendedorId,
                                ProdutoId = operacao.DadosMeta.ProdutoId,
                                TipoMeta = operacao.DadosMeta.TipoMeta,
                                Valor = operacao.DadosMeta.Valor,
                                Periodicidade = operacao.DadosMeta.Periodicidade,
                                Ativo = operacao.DadosMeta.Ativo,
                                DataCriacao = operacao.DadosMeta.DataCriacao
                            };
                            return _metaService.CriarMeta(novaMeta) > 0;
                        }
                        break;

                    case TipoOperacao.Edicao:
                        // Reverter edição = atualizar com os dados anteriores
                        if (operacao.DadosMeta != null)
                        {
                            // Buscar a meta atual primeiro para garantir que existe
                            var metaAtual = _metaService.ObterMetaPorId(operacao.MetaId) ?? throw new InvalidOperationException($"Meta com ID {operacao.MetaId} não foi encontrada.");

                            // Atualizar a meta atual com os dados anteriores
                            metaAtual.Nome = operacao.DadosMeta.Nome;
                            metaAtual.VendedorId = operacao.DadosMeta.VendedorId;
                            metaAtual.ProdutoId = operacao.DadosMeta.ProdutoId;
                            metaAtual.TipoMeta = operacao.DadosMeta.TipoMeta;
                            metaAtual.Valor = operacao.DadosMeta.Valor;
                            metaAtual.Periodicidade = operacao.DadosMeta.Periodicidade;
                            metaAtual.Ativo = operacao.DadosMeta.Ativo;

                            return _metaService.AtualizarMeta(metaAtual);
                        }
                        else
                        {
                            throw new InvalidOperationException("Os dados anteriores da meta não foram encontrados. Não é possível reverter a edição.");
                        }

                    default:
                        throw new InvalidOperationException("Tipo de operação desconhecido.");
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao reverter operação: {ex.Message}");
                throw; // Re-lançar a exceção para ser tratada no método chamador
            }
        }
    }
}