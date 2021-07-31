using DesafioTecnico.DataAccess.Context;
using DesafioTecnico.JogoGourmet.Abstraction;
using DesafioTecnico.JogoGourmet.Logic;

namespace DesafioTecnico
{
    class Program
    {
        static void Main(string[] args)
        {
            JogoContext context = new JogoContext();
            Ijogo jogo = new Jogo(context);
            jogo.Iniciar();
        }
    }
}