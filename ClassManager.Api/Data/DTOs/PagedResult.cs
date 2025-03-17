namespace ClassManager.Api.Data.DTOs
{
    public record PagedResult<TRecord>(TRecord[] Records, int TotalCount);
}
