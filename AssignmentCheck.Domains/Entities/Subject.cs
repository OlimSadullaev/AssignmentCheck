using AssignmentCheck.Domains.Commons;
using AssignmentCheck.Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Domains.Entities
{
    public class Subject : Auditable
    {
        public string Name { get; set; }
        public string  Description { get; set; }
        public string AssignmentPath { get; set; }
        public UserRole Role { get; set; }

        /*public Guid UserId { get; set; }
        public User User { get; set; }*/

        /*public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }*/
    }
}
