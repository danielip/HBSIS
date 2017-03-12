
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROCK_PAPER_SCISSORS.Model;

namespace ROCK_PAPER_SCISSORS.Configuracao
{
    class LeitorArquivo
    {
        //<summary>
        //Realiza a leitura do arquivo txt com a configuração do campeonato
        //Supõem-se que o txt sempre terá o nome configuracaoTorneio.txt e ficará no diretório c:\temp
        //</summary>
        //<returns>Retorna o objeto do torneio preenchido com base nas informações do arquivo</returns>
        public Torneio LerArquivoTorneio()
        {
            try
            {
                string mensagem = string.Empty;
                List<string> mensagemLinha = new List<string>();
                Torneio torneioPedraPapelTesoura = new Torneio();

                string caminhoDoArquivo = " C:\\temp\\configuracaoTorneio.txt";
                using (StreamReader texto = new StreamReader(caminhoDoArquivo))
                {
                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem.Replace("\t", ""));
                    }
                }

                return ConverterArquivo(mensagemLinha);
            }
            catch (Exception erro)
            {
                throw new Exception("Erro na leitura do arquivo" + erro.Message);
            }
        }

        /// <summary>
        /// Converte as linhas do arquivo txt em um objeto torneio
        /// </summary>
        /// <param name="mensagemLinha"></param>
        /// <returns></returns>
        private Torneio ConverterArquivo(List<string> mensagemLinha)
        {
            Torneio torneio = new Torneio();
            List<string> linhasValidas = new List<string>();
            foreach (var mensagem in mensagemLinha)
            {
                //identifica quais são as linhas que possuem conteúdo das jogadas. ex: 	[ ["Armando", "P"], ["Dave", "S"] ],
                if (mensagem.StartsWith("[") && (mensagem.EndsWith(",") || mensagem.EndsWith("]")))
                {
                    //substitui os colchetes por chaves para facilitar a separação posteriormente
                    //retira os espaçoes em branco e a virgula do final da linha
                    linhasValidas.Add(mensagem.Replace("[ [", "{").Replace("],", "}").Replace(" [", "{").Replace("] ]", "}").
                        Replace("] ]", "}").Replace(" ", "").Replace("[", "").Replace("]", "").TrimEnd(','));
                }

            }
            
            torneio.rodadas = MontarObjetoTorneio(linhasValidas); 

            return torneio;
        }

        /// <summary>
        /// A partir das linhas válidas, preenche a lista de rodadas que serão utilizadas no torneio
        /// </summary>
        /// <param name="rodada"></param>
        /// <param name="rodadasTorneio"></param>
        /// <param name="rodadasTorneioCompleto"></param>
        /// <param name="linhasValidas"></param>
        private static List<Rodada>[,] MontarObjetoTorneio(List<string> linhasValidas)
        {
            Rodada rodada = new Rodada();
            List<Rodada> rodadasTorneio = new List<Rodada>();
            List<Rodada>[,] rodadasTorneioCompleto = new List<Rodada>[100, 2];
            int contadorIndice = 0;
            int contadorRodada = 0;
            foreach (var linha in linhasValidas)
            {
                List<string> listaRodadas = linha.Split('}').ToList();
                foreach (var itemRodada in listaRodadas)
                {
                    //define cada rodada a partir da linha obtida no arquivo, a linha sempre é separada por ",".
                    //A cada "," existe uma nova rodada
                    List<string> definicaoRodada = itemRodada.Split(',').ToList();

                    if (!string.IsNullOrEmpty(definicaoRodada[0]))
                    {
                        rodada = new Rodada();
                        rodada.NomeJogador = definicaoRodada[0].Replace("\"", "").Replace("{", "");
                        rodada.Jogada = definicaoRodada[1].Replace("\"", "");
                        rodadasTorneio.Add(rodada);
                    }
                }

                rodadasTorneioCompleto[contadorIndice, contadorRodada] = rodadasTorneio;
                contadorRodada++;
                rodadasTorneio = new List<Rodada>();
                //quando o contador chegar em dois, significa que inicia uma nova rodada                   
                if (contadorRodada == 2)
                {
                    contadorRodada = 0;
                    contadorIndice++;
                }             
            }
            return rodadasTorneioCompleto;        
        }
    }
}
