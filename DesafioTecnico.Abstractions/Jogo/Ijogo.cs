namespace DesafioTecnico.JogoGourmet.Abstraction
{
    using DesafioTecnico.JogoGourmet.Models;

    public interface Ijogo
    {
        /// <summary>
        /// Executa o jogo.
        /// </summary>
        void Iniciar();

        /// <summary>
        /// Insere um novo prato na base de dados.
        /// </summary>
        /// <param name="ultimaRespostaConsultada">RespostaFinal perguntada para o usuário. Ex: É 'Bolo de Chocolate'?</param>
        /// <param name="noPai">Objeto pai que será alterado com o novo prato.</param>
        /// <param name="ultimaResposta">Último "Sim ou Não" que o usuário fez.</param>
        /// <param name="descricaoNovoPrato">Ex: Bolo de Morango</param>
        /// <param name="caracteristicaNovoPrato">Ex: de frutas cítricas.</param>
        void insereNovoPrato(string ultimaRespostaConsultada, Pergunta noPai, string ultimaResposta, string descricaoNovoPrato, string caracteristicaNovoPrato);
    }
}
