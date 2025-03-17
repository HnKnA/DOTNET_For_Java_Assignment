using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassManager.Api.Data.DTOs
{
    public class ValidateSubjectDto
    {
        public string Subject { get; set; } 
        public int? SubjectId { get; set; } 
        public string AccessCode { get; set; }
    }

}
