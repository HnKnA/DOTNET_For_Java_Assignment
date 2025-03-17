namespace ClassManager.Shared.DTOs
{
    public record PagedResult<TRecord>(TRecord[] Records, int TotalCount);
}
