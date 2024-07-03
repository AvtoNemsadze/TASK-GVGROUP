using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotGameServer.Application.Models.Identity
{
    public class AuthResponse
    {
        private string userName = null!;
        public int Id { get; set; }
        public string UserName { get => userName; set => userName = value; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public bool IsSucceed { get; set; }
    }
}
