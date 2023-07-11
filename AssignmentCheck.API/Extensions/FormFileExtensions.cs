using AssignmentCheck.Service.DTOs;

namespace AssignmentCheck.Api.Extensions
{
    public static class FormFileExtensions
    {
        public static AssignmentForCreationDTO ToAssignmentOrDefault(this IFormFile formFile)
        {
            if(formFile?.Length > 0) 
            { 
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AssignmentForCreationDTO
                {
                    Name = formFile.Name,
                    Stream = new MemoryStream(fileBytes)
                };
            }
            return null;

        }
    }
}
