using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Exceptions
{
    public class AssignmentCheckException : Exception
    {
        public int Code { get; set; }

        public AssignmentCheckException(int code, string messege) : 
            base(messege) =>
            this.Code = code;
    }
}
