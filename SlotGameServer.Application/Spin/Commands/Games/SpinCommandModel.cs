using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinCommandModel
    {
        public int SessionId { get; set; }
        public int ChosenNumber { get; set; }
        public decimal BetAmount { get; set; }
    }
}
