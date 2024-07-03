using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlotGameServer.Application.Contracts.Identity;
using SlotGameServer.Application.Models.Identity;
using SlotGameServer.Identity.Models;


namespace SlotGameServer.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponseModel> GetUser(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"User ({id}) Was Not Found");

            var roles = await _userManager.GetRolesAsync(user);

            return new UserResponseModel
            {
                Id = user.Id,
                Email = user?.Email,
                Firstname = user?.FirstName,
                Lastname = user?.LastName,
                UserName = user?.UserName,
                CreatedAt = user?.CreatedAt,
                Role = roles.FirstOrDefault(),
            };
        }

        public async Task<List<UserResponseModel>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userResponseList = users.Select(user => new UserResponseModel
            {
                Email = user.Email,
                Id = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                UserName = user.UserName,
                Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault()
            }).ToList();

            return userResponseList;
        }

        public async Task<List<UserResponseModel>> GetAdministrators()
        {
            var users = await _userManager.GetUsersInRoleAsync("Administrator");

            var nonDeletedUsers = users.Where(u => !u.IsDeleted);

            return nonDeletedUsers.Select(x => new UserResponseModel
            {
                Email = x.Email,
                Id = x.Id,
                Firstname = x.FirstName,
                Lastname = x.LastName,
                UserName = x.UserName,
                CreatedAt = x.CreatedAt,
                Role = _userManager.GetRolesAsync(x).Result.FirstOrDefault()
            }).ToList();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"user ({id}) not found or unable to delete.");

            user.IsDeleted = true;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> UserExistAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user != null;
        }
    }
}
