using AssignmentCheck.Domain.Commons;
using AssignmentCheck.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Domain.Entities
{
    public class Subject : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public ICollection<Assignment> Assignments { get; set; }
    }
}
