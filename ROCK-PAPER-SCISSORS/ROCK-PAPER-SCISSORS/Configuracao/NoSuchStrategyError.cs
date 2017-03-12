using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROCK_PAPER_SCISSORS.Configuracao
{
    public class NoSuchStrategyError : Exception
    {
        public NoSuchStrategyError()
        {
        }

        public NoSuchStrategyError(string message)
            : base(message)
        {
        }

        public NoSuchStrategyError(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
