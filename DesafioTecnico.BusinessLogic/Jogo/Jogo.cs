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

        private (string descricao, string caracteristica) perguntaDadosNovoPrato(string respostaFinal)
        {
            var descricaoNovoPrato = "";
            while (string.IsNullOrEmpty(descricaoNovoPrato))
            {
                Console.WriteLine(MensagensJogo.PerguntaPratoNovo);
                descricaoNovoPrato = Console.ReadLine();
            }

            var caracteristicaNovoPrato = "";
            while (string.IsNullOrEmpty(caracteristicaNovoPrato))
            {
                Console.WriteLine(descricaoNovoPrato + " é ______ mas " + respostaFinal + " não.");
                caracteristicaNovoPrato = Console.ReadLine();
            }

            return (descricaoNovoPrato, caracteristicaNovoPrato);
        }

        public void insereNovoPrato(string ultimaRespostaConsultada, Pergunta noPai, string ultimaResposta, string descricaoNovoPrato, string caracteristicaNovoPrato)
        {
            Pergunta novoNo = new Pergunta
            {
                Descricao = caracteristicaNovoPrato,
                ProximaNao = new Pergunta
                {
                    RespostaFinal = ultimaRespostaConsultada,
                },
                ProximaSim = new Pergunta
                {
                    RespostaFinal = descricaoNovoPrato
                }
            };
            if (ultimaResposta.Equals(RespostasUsuario.Sim))
                noPai.ProximaSim = novoNo;
            else
                noPai.ProximaNao = novoNo;
        }

        private (bool, Pergunta, string, string) executaJogo(Pergunta perguntas)
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

            return (respostaUsuario.ToUpper().Equals(RespostasUsuario.Sim), noPai, perguntas.RespostaFinal, ultimaResposta);
        }

        public void Iniciar()
        {
            var perguntas = context.Perguntas;

            Console.WriteLine(MensagensJogo.MensagemInicio);

            var (acertou, noPai, ultimaRespostaConsultada, ultimaRespostaUsuario) = executaJogo(perguntas);

            if (acertou)
                Console.WriteLine(MensagensJogo.AcertouPrato);
            else
            {
                var (descricaoNovoPrato, caracteristicaNovoPrato) = perguntaDadosNovoPrato(ultimaRespostaConsultada);
                insereNovoPrato(ultimaRespostaConsultada, noPai, ultimaRespostaUsuario, descricaoNovoPrato, caracteristicaNovoPrato);
                Console.WriteLine();
                Iniciar();
            }
        }
    }
}
