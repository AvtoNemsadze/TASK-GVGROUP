using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.Models.Identity
{
    public class BaseAuthResponse
    {
        public bool IsSucceed { get; set; }
        public string? Message { get; set; }
    }
}
