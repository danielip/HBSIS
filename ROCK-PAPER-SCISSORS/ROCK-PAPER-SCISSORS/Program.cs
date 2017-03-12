using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROCK_PAPER_SCISSORS.Configuracao;
using ROCK_PAPER_SCISSORS.Model;
using ROCK_PAPER_SCISSORS.Negocio;

namespace ROCK_PAPER_SCISSORS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AdministradorCampeonato campeonato = new AdministradorCampeonato();
                LeitorArquivo lerArquivoTorneio = new LeitorArquivo();
                Rodada jogadaVencedora = campeonato.rps_tournament_winner(lerArquivoTorneio.LerArquivoTorneio());
                Console.WriteLine("O vencedor do campeonato é: " + jogadaVencedora.NomeJogador);
                Console.Read();
            }
            catch (NoSuchStrategyError)
            {
                Console.WriteLine("Jogada informada é inválida. Jogadas válidas: P, R, S");
                Console.Read();
            }
            catch (WrongNumberOfPlayersError)
            {
                Console.WriteLine("As rodadas devem conter 2 jogadores");
                Console.Read();
            }



        }
    }
}
