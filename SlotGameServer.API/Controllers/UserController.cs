using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlotGameServer.Application.Contracts.Identity;

namespace SlotGameServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            return Ok(user);
        }


        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Id is required");
            }

            var userToDelete = await _userService.DeleteUser(id);

            if (userToDelete)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest();
        }
    }
}
