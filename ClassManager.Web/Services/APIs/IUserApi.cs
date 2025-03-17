using Refit;
using ClassManager.Shared;
using ClassManager.Shared.DTOs;

namespace ClassManager.Web.Services.APIs
{
    [Headers("Authorization : Bearer")]
    public interface IUserApi
    {
        [Get("/api/users")]
        Task<PagedResult<UserDto>> GetUsersAsync(int startIndex, int pageSize);

        [Patch("/api/users/{userId}/toggle-status")]
        Task ToggleUserApprovedStatus(int userId);

        [Delete("/api/users/{userId}")]
        Task DeleteUserAsync(int userId);

    }
}
