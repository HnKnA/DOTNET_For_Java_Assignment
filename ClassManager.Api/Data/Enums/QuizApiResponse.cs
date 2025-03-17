namespace ClassManager.Api.Data.Enums
{
    public record QuizApiResponse(bool IsSuccess, string? ErrorMessage)
    {
        public static QuizApiResponse Success() => new(true, null);

        public static QuizApiResponse Fail(string errorMessage) => new(false, errorMessage);
    }
}
