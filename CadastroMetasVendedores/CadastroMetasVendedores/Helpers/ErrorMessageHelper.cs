using System.Windows.Forms;

namespace CadastroMetasVendedores.Helpers
{
    public static class ErrorMessageHelper
    {
        /// <summary>
        /// Exibe uma mensagem de erro padronizada seguindo o formato (1-Erro, 2-Detalhe, 3-Dica)
        /// </summary>
        /// <param name="mensagem">Mensagem formatada com \n separando as seções</param>
        /// <param name="titulo">Título da janela de erro</param>
        public static void ExibirErro(string mensagem, string titulo = "Erro")
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Exibe uma mensagem de sucesso com opção de desfazer
        /// </summary>
        /// <param name="mensagem">Mensagem de sucesso</param>
        /// <param name="permitirDesfazer">Se true, mostra opção para desfazer</param>
        /// <param name="titulo">Título da janela</param>
        /// <returns>True se o usuário escolheu desfazer a operação</returns>
        public static bool ExibirSucessoComDesfazer(string mensagem, bool permitirDesfazer = true, string titulo = "Sucesso")
        {
            if (permitirDesfazer)
            {
                string mensagemCompleta = $"{mensagem}\n\nDeseja desfazer esta operação?";
                var resultado = MessageBox.Show(mensagemCompleta, titulo,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                return resultado == DialogResult.Yes;
            }
            else
            {
                MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// Exibe confirmação para exclusão com aviso sobre reversão
        /// </summary>
        /// <param name="item">Nome do item a ser excluído</param>
        /// <param name="tipo">Tipo do item (Meta, Vendedor, Produto)</param>
        /// <returns>True se confirmou a exclusão</returns>
        public static bool ConfirmarExclusao(string item, string tipo)
        {
            string mensagem = $"Deseja realmente excluir {tipo.ToLower()} '{item}'?\n\n" +
                             "ATENÇÃO: Esta operação NÃO pode ser desfeita.\n" +
                             "Todos os dados relacionados serão perdidos permanentemente.";

            var resultado = MessageBox.Show(mensagem, $"Confirmar Exclusão - {tipo}",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            return resultado == DialogResult.Yes;
        }

        /// <summary>
        /// Exibe confirmação para ativação/inativação com opção de desfazer
        /// </summary>
        /// <param name="item">Nome do item</param>
        /// <param name="tipo">Tipo do item</param>
        /// <param name="ativar">True para ativar, false para inativar</param>
        /// <returns>True se confirmou a operação</returns>
        public static bool ConfirmarAtivacaoInativacao(string item, string tipo, bool ativar)
        {
            string acao = ativar ? "ativar" : "inativar";
            string mensagem = $"Deseja realmente {acao} {tipo.ToLower()} '{item}'?\n\n" +
                             "Esta operação pode ser desfeita a qualquer momento.";

            var resultado = MessageBox.Show(mensagem, $"Confirmar {(ativar ? "Ativação" : "Inativação")} - {tipo}",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return resultado == DialogResult.Yes;
        }

        /// <summary>
        /// Formata uma mensagem de erro no padrão (1-Erro, 2-Detalhe, 3-Dica)
        /// </summary>
        /// <param name="erro">Descrição do erro</param>
        /// <param name="detalhe">Detalhe técnico do erro</param>
        /// <param name="dica">Sugestão para resolver o problema</param>
        /// <returns>Mensagem formatada</returns>
        public static string FormatarMensagemErro(string erro, string detalhe, string dica)
        {
            return $"Erro: {erro}\nDetalhe: {detalhe}\nDica: {dica}";
        }
    }
}