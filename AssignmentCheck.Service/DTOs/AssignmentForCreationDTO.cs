using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.DTOs
{
    public class AssignmentForCreationDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Stream Stream { get; set; }
    }
}
