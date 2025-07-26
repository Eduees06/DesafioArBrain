using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CadastroMetasVendedores.Models;
using CadastroMetasVendedores.Services;

namespace CadastroMetasVendedores.Forms
{
    public partial class HistoricoOperacoesForm : Form
    {
        private readonly List<HistoricoOperacao> _operacoes;
        private readonly MetaService _metaService;

        public HistoricoOperacoesForm(List<HistoricoOperacao> operacoes, MetaService metaService)
        {
            _operacoes = operacoes;
            _metaService = metaService;
            InitializeComponent();
            CarregarHistorico();
        }

        private void CarregarHistorico()
        {
            var dadosGrid = _operacoes.Select(op => new
            {
                Id = op.Id,
                TipoOperacao = ObterDescricaoOperacao(op.TipoOperacao),
                DataOperacao = op.DataOperacao.ToString("dd/MM/yyyy HH:mm:ss"),
                MetaNome = op.MetaNome,
                VendedorNome = op.VendedorNome,
                ProdutoNome = op.ProdutoNome,
                Descricao = op.Descricao
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
                dgvHistorico.Columns["MetaNome"].Width = 150;
            }

            if (dgvHistorico.Columns["VendedorNome"] != null)
            {
                dgvHistorico.Columns["VendedorNome"].HeaderText = "Vendedor";
                dgvHistorico.Columns["VendedorNome"].Width = 120;
            }

            if (dgvHistorico.Columns["ProdutoNome"] != null)
            {
                dgvHistorico.Columns["ProdutoNome"].HeaderText = "Produto";
                dgvHistorico.Columns["ProdutoNome"].Width = 120;
            }

            if (dgvHistorico.Columns["Descricao"] != null)
            {
                dgvHistorico.Columns["Descricao"].HeaderText = "Descrição";
                dgvHistorico.Columns["Descricao"].Width = 200;
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

                var resultado = MessageBox.Show(
                    $"Deseja realmente reverter a operação '{ObterDescricaoOperacao(operacao.TipoOperacao)}' da meta '{operacao.MetaNome}'?",
                    "Confirmar Reversão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

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
                        // Reverter edição = restaurar dados anteriores
                        if (operacao.DadosMeta != null)
                        {
                            return _metaService.AtualizarMeta(operacao.DadosMeta);
                        }
                        break;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}