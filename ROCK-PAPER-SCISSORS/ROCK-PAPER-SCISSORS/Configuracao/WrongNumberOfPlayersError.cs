using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROCK_PAPER_SCISSORS.Configuracao
{
    public class WrongNumberOfPlayersError : Exception
    {
        public WrongNumberOfPlayersError()
        {
        }

        public WrongNumberOfPlayersError(string message)
            : base(message)
        {
        }

        public WrongNumberOfPlayersError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
