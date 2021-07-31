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
                Descricao = "É doce?",
                ProximaNao = new Pergunta
                {
                    Descricao = "É uma massa?",
                    ProximaSim = new Pergunta
                    {
                        RespostaFinal = "Lasanha"
                    },
                    ProximaNao = new Pergunta
                    {
                        RespostaFinal = "Pipoca salgada"
                    }
                },
                ProximaSim = new Pergunta
                {
                    Descricao = "É de amendoim?",
                    ProximaSim = new Pergunta
                    {
                        RespostaFinal = "Paçoca"
                    },
                    ProximaNao = new Pergunta
                    {
                        RespostaFinal = "Pipoca doce"
                    }
                }
            };
        }
    }
}
