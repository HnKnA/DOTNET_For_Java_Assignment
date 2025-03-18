namespace ClassManager.Shared.DTOs
{
    public class ValidateSubjectDto
    {
        public string Subject { get; set; }  = string.Empty;
        public int? SubjectId { get; set; } 
        public string AccessCode { get; set; } = string.Empty;
    }

}
