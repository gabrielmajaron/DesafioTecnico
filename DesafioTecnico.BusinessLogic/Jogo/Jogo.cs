using System;

namespace DesafioTecnico.JogoGourmet.Logic
{
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

        private void insereNovoPrato(Pergunta perguntas, Pergunta noPai)
        {
            string novoPrato = "";
            while (string.IsNullOrEmpty(novoPrato))
            {
                Console.WriteLine("O que é então?");
                novoPrato = Console.ReadLine();
            }

            Console.WriteLine(novoPrato + " é ______ mas " + perguntas.RespostaFinal + " não.");

            var adjetivoNovaComida = Console.ReadLine();

            Pergunta novoNo = new Pergunta
            {
                Descricao = "É " + adjetivoNovaComida + "?",
                ProximaNao = new Pergunta
                {
                    RespostaFinal = perguntas.RespostaFinal,
                },
                ProximaSim = new Pergunta
                {
                    RespostaFinal = novoPrato
                }
            };

            noPai.ProximaNao = novoNo;
        }

        private (bool, Pergunta, Pergunta) executaJogo(Pergunta perguntas)
        {
            string respostaUsuario;

            var noPai = perguntas;
            while (string.IsNullOrEmpty(perguntas.RespostaFinal))
            {
                Console.WriteLine(perguntas.Descricao);
                respostaUsuario = Console.ReadLine();

                noPai = perguntas;
                if (respostaUsuario.ToUpper().Equals("S"))
                    perguntas = perguntas.ProximaSim;
                else
                    perguntas = perguntas.ProximaNao;
            }

            Console.WriteLine("É " + perguntas.RespostaFinal + "?");

            respostaUsuario = Console.ReadLine();
            return (respostaUsuario.ToUpper().Equals("S"), noPai, perguntas);
        }

        public void Iniciar()
        {
            var perguntas = context.Perguntas;

            Console.WriteLine("Pense no seu prato favorito");

            var (acertou, noPai, ultimaPergunta) = executaJogo(perguntas);

            if (acertou)
                Console.WriteLine("Acertei!");
            else
            {
                insereNovoPrato(ultimaPergunta, noPai);
                Console.WriteLine();
                Iniciar();
            }
        }
    }
}
