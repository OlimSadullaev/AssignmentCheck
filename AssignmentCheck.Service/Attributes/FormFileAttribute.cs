using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Attributes
{
    public class FormFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                string[] extensions = new string[] { ".zip" };
                var extension = Path.GetExtension(file.FileName);

                if(!extensions.Contains(extension.ToLower())) 
                {
                    return new ValidationResult("Only ZIP files are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
