using System.Collections.Generic;

namespace Application.Dtos
{
    public class ApiValidationErrorResponseDto : ApiResponseDto
    {
        public IEnumerable<string> Errors { get; set; }
    }
}