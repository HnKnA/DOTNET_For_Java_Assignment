using System.Text.Json.Serialization;
using ClassManager.Api.Data.Enums;

namespace ClassManager.Api.Data.DTOs
{
    public record AuthResponseDto(LoggedInUser User, string? ErrorMessage = null)
    {
        [JsonIgnore]
        public bool HasError => ErrorMessage != null;
    }
}
