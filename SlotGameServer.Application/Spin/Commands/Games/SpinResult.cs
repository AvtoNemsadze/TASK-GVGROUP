﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinResult
    {
        public bool IsWin { get; set; }  
        public int ResultNumber { get; set; } 
        public decimal BetAmount { get; set; } 
        public decimal Winnings { get; set; } 
        public string? Message { get; set; }
    }
}