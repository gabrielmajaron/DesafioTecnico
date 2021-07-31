using System;

namespace DesafioTecnico.JogoGourmet.Logic
{
    using DesafioTecnico.BusinessLogic.Constantes;
    using DesafioTecnico.DataAccess.Context;
    using DesafioTecnico.JogoGourmet.Abstraction;
    using DesafioTecnico.JogoGourmet.Models;

    public class Jogo : Ijogo
    {
        private readonly JogoContext context;

        public Jogo(JogoContext context)
        {
            this.context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        private void insereNovoPrato(Pergunta perguntas, Pergunta noPai, string ultimaResposta)
        {
            string novoPrato = "";
            while (string.IsNullOrEmpty(novoPrato))
            {
                Console.WriteLine(MensagensJogo.PerguntaPratoNovo);
                novoPrato = Console.ReadLine();
            }

            Console.WriteLine(novoPrato + " é ______ mas " + perguntas.RespostaFinal + " não.");

            var adjetivoNovaComida = Console.ReadLine();

            Pergunta novoNo = new Pergunta
            {
                Descricao = adjetivoNovaComida,
                ProximaNao = new Pergunta
                {
                    RespostaFinal = perguntas.RespostaFinal,
                },
                ProximaSim = new Pergunta
                {
                    RespostaFinal = novoPrato
                }
            };
            if (ultimaResposta.Equals(RespostasUsuario.Sim))
                noPai.ProximaSim = novoNo;
            else
                noPai.ProximaNao = novoNo;
        }

        private (bool, Pergunta, Pergunta, string) executaJogo(Pergunta perguntas)
        {
            string respostaUsuario, ultimaResposta = RespostasUsuario.Nao;

            var noPai = perguntas;
            while (string.IsNullOrEmpty(perguntas.RespostaFinal))
            {
                Console.WriteLine(MensagensJogo.PerguntaPadrao + perguntas.Descricao + " (S/N)?");
                respostaUsuario = Console.ReadLine();

                noPai = perguntas;
                if (respostaUsuario.ToUpper().Equals(RespostasUsuario.Sim))
                {
                    perguntas = perguntas.ProximaSim;
                    ultimaResposta = RespostasUsuario.Sim;
                }                    
                else
                {
                    perguntas = perguntas.ProximaNao;
                    ultimaResposta = RespostasUsuario.Nao;
                }
            }

            Console.WriteLine("É " + perguntas.RespostaFinal + "?");

            respostaUsuario = Console.ReadLine();
            return (respostaUsuario.ToUpper().Equals(RespostasUsuario.Sim), noPai, perguntas, ultimaResposta);
        }

        public void Iniciar()
        {
            var perguntas = context.Perguntas;

            Console.WriteLine(MensagensJogo.MensagemInicio);

            var (acertou, noPai, ultimaPergunta, ultimaResposta) = executaJogo(perguntas);

            if (acertou)
                Console.WriteLine(MensagensJogo.AcertouPrato);
            else
            {
                insereNovoPrato(ultimaPergunta, noPai, ultimaResposta);
                Console.WriteLine();
                Iniciar();
            }
        }
    }
}
