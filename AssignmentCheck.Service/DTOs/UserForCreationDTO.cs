using AssignmentCheck.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.DTOs
{
    public class UserForCreationDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [UserEmail, Required]
        public string Email { get; set; }

        [UserPassword, Required]
        public string Password { get; set; }
    }
}
