using System.Text.Json.Serialization;

namespace ClassManager.Shared.DTOs
{
    public record AuthResponseDto(LoggedInUser? User, string? ErrorMessage = null)
    {
        [JsonIgnore]
        public bool HasError => ErrorMessage != null;
    }
}
