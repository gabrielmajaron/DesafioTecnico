namespace DesafioTecnico.JogoGourmet.Models
{
    public class Pergunta
    {
        public Pergunta()
        {
            ProximaSim = ProximaNao = null;
        }

        public string Descricao { get; set; }
        public string RespostaFinal { get; set; }
        public Pergunta ProximaSim { get; set; }
        public Pergunta ProximaNao { get; set; }
    }
}
