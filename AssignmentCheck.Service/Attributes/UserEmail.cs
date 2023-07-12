using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Attributes
{
    public class UserEmail : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string email))
                return false;

            // Regular expression for email format validation
            const string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            return Regex.IsMatch(email, emailPattern);
        }
    }
}
