using Refit;
using ClassManager.Shared.DTOs;
using ClassManager.Shared;

namespace ClassManager.Web.Services.APIs
{
    public interface IAuthApi
    {
        [Post("/api/auth/login")]
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        [Post("/api/auth/register")]
        Task<QuizApiResponse> RegisterAsync(RegisterDto dto);
    }
}
