using AssignmentCheck.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Domain.Entities
{
    public class Assignment : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
