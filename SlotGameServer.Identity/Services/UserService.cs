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
        private readonly SlotGameServerIdentityDbContext _identityDbContext;
        public UserService(UserManager<ApplicationUser> userManager, SlotGameServerIdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _identityDbContext = identityDbContext;
        }

        public async Task UpdateBalanceAsync(int userId, decimal amount, bool isWin)
        {
            var user = await _identityDbContext.Users.FindAsync(userId);
            if (user != null)
            {
                if (isWin)
                    user.Balance += amount;  
                else
                    user.Balance -= amount; 

                _identityDbContext.Users.Update(user);
                await _identityDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> CanUserPlaceBetAsync(int userId, decimal betAmount)
        {
            var user = await _identityDbContext.Users.FindAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found.");

            return user.Balance >= betAmount;
        }

        public async Task UpdatePlayerStatistics(int userId, bool isWin)
        {
            var user = await _identityDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found."); 
            }

            user.TotalGamesPlayed++;
            if (isWin)
            {
                user.TotalWins++; 
            }
            else
            {
                user.TotalLosses++;
            }

            _identityDbContext.Users.Update(user);
            await _identityDbContext.SaveChangesAsync();
        }


        public async Task<bool> UserExistAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user != null;
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
            };
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"user ({id}) not found or unable to delete.");

            user.IsDeleted = true;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}
