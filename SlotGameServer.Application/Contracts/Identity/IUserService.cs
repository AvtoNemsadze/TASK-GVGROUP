using SlotGameServer.Application.Models.Identity;

namespace SlotGameServer.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<bool> UserExistAsync(int userId);
        Task UpdateBalanceAsync(int userId, decimal amount, bool isWin);
        Task<bool> CanUserPlaceBetAsync(int userId, decimal betAmount);
        Task UpdatePlayerStatistics(int userId, bool isWin);
        Task<UserResponseModel> GetUser(int id);
        Task<bool> DeleteUser(int id);
    }
}
