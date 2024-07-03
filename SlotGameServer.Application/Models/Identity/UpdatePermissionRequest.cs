using System.ComponentModel.DataAnnotations;

namespace SlotGameServer.Application.Models.Identity
{
    public class UpdatePermissionRequest
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = null!;
    }
}
