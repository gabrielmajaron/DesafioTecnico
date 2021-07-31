
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xunit;

namespace DesafioTecnico.Tests
{
    using DesafioTecnico.BusinessLogic.Constantes;
    using DesafioTecnico.DataAccess.Context;
    using DesafioTecnico.JogoGourmet.Abstraction;
    using DesafioTecnico.JogoGourmet.Logic;
    using DesafioTecnico.JogoGourmet.Models;

    [TestClass]
    public class TesteUnitario
    {
        [Fact]
        public void InsereNovoPrato_DadoValoresDeInsercao()
        {
            // Arranje
            JogoContext context = new JogoContext();
            Ijogo jogo = new Jogo(context);

            var noPai = context.Perguntas;
            var ultimaRespostaUsuario = RespostasUsuario.Nao;
            var ultimaRespostaBase = context.Perguntas.ProximaNao.RespostaFinal;
            var descricaoNovoPrato = "Bolo de Morango";
            var caracteristicaNovoPrato = "de morango";

            // Act
            jogo.insereNovoPrato(ultimaRespostaBase, noPai, ultimaRespostaUsuario, descricaoNovoPrato, caracteristicaNovoPrato);

            // Assert

            var novosValores = new Pergunta
            {
                Descricao = "massa",
                ProximaSim = new Pergunta
                {
                    RespostaFinal = "Lasanha"
                },
                ProximaNao = new Pergunta
                {
                    Descricao = caracteristicaNovoPrato,
                    ProximaSim = new Pergunta
                    {
                        RespostaFinal = descricaoNovoPrato
                    },
                    ProximaNao = new Pergunta
                    {
                        RespostaFinal = "Bolo de Chocolate"
                    }
                }
            };

            var valoresEsperados = JsonConvert.SerializeObject(novosValores);
            var valoresExistentes = JsonConvert.SerializeObject(context.Perguntas);
            Xunit.Assert.Equal(valoresEsperados, valoresExistentes);
        }
    }
}
