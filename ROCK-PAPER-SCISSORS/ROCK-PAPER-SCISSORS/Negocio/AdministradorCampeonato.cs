using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROCK_PAPER_SCISSORS.Configuracao;
using ROCK_PAPER_SCISSORS.Model;


namespace ROCK_PAPER_SCISSORS.Negocio
{
    public class AdministradorCampeonato
    {
        /// <summary>
        /// Identifica se as jogadas e quantidade de jogadas são válidas. Caso forem, compara as jogadas
        /// </summary>
        /// <param name="jogadas"></param>
        /// <returns>retorna a jogada vencedora</returns>
        public Rodada rps_game_winner(List<Rodada> jogadas)
        {
            Rodada jogadaVencedora = new Rodada();
            if (jogadas != null)
            {
                if (jogadas.Count != 2)
                    throw new WrongNumberOfPlayersError();

                List<string> jogadasValidas = new List<string> { "R", "P", "S" };
                foreach (var item in jogadas)
                {
                    if (!jogadasValidas.Contains(item.Jogada.ToUpper()))
                        throw new NoSuchStrategyError();
                }

                jogadaVencedora = CompararJogadas(jogadas);
            }

            return jogadaVencedora;
        }

        /// <summary>
        /// A partir do torneio, faz o "maata-mata" para descobrir o vencedor do campeonato
        /// </summary>
        /// <param name="torneio"></param>
        /// <returns></returns>
        public Rodada rps_tournament_winner(Torneio torneio)
        {
            List<Rodada> vencedoresDaRodada = new List<Rodada>();
            List<Rodada> competidoresRodadaFinal = new List<Rodada>();
            foreach (var rodada in torneio.rodadas)
            {
                //verifica o vencedor de cada rodada
                Rodada jogadaVencedora = rps_game_winner(rodada);
                vencedoresDaRodada.Add(jogadaVencedora);

                //monta mais uma rodada a partir dos vencedores das demais rodadas
                if (vencedoresDaRodada.Count == 2)
                {
                   Rodada vencedorPartida = rps_game_winner(vencedoresDaRodada);
                   competidoresRodadaFinal.Add(vencedorPartida);
                   vencedoresDaRodada = new List<Rodada>();
                }

                //monta a rodada de competidores finais 
                if (competidoresRodadaFinal.Count == 2)
                {
                    Rodada vencedorPartida = rps_game_winner(competidoresRodadaFinal);
                    return vencedorPartida;                    
                }
            }

            return new Rodada();
        }

        /// <summary>
        /// Compara as jogadas para determinar o campeão. Baseado nas regras R > S, S > P, P > R
        /// </summary>
        /// <param name="jogadas"></param>
        /// <returns>jogada campeã</returns>
        private Rodada CompararJogadas(List<Rodada> jogadas)
        {
            Rodada jogada1 = jogadas[0];
            Rodada jogada2 = jogadas[1];

            switch (jogada1.Jogada)
            {
                case "R":
                    {
                        if (jogada2.Jogada == "S")
                            return jogada1;
                        else
                            return jogada2;
                    }
                case "P":
                    {
                        if (jogada2.Jogada == "R")
                            return jogada1;
                        else
                            return jogada2;
                    }
                case "S":
                    {
                        if (jogada2.Jogada == "P")
                            return jogada1;
                        else
                            return jogada2;
                    }
            }

            return new Rodada();
        }
    }
}