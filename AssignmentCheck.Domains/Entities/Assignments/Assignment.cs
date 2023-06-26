using AssignmentCheck.Domains.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Domains.Entities.Assignments
{
    public class Assignment : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
