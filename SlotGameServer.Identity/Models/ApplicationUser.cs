using Microsoft.AspNetCore.Identity;


namespace SlotGameServer.Identity.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
        public int TotalGamesPlayed { get; set; }
        public ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
    }

    public class ApplicationRole : IdentityRole<int>
    {

    }
}
