using AssignmentCheck.Domains.Commons;
using AssignmentCheck.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Domains.Entities
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        // Collection navigation containing dependents
        // public ICollection<Subject> Subjects { get; set; }
    }
}
