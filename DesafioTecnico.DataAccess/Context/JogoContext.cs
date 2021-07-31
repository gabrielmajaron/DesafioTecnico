namespace DesafioTecnico.DataAccess.Context
{
    using DesafioTecnico.JogoGourmet.Models;
    public class JogoContext
    {
        public Pergunta Perguntas { get; set; }

        public JogoContext()
        {
            // Dados existentes no Banco de dados.

            Perguntas = new Pergunta
            {
                Descricao = "massa",
                ProximaSim = new Pergunta
                {
                    RespostaFinal = "Lasanha"
                },
                ProximaNao = new Pergunta
                {
                    RespostaFinal = "Bolo de Chocolate"
                }               
            };
        }
    }
}
