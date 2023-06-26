﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Attributes
{
    public class UserPassword : ValidationAttribute
    {
        public override bool IsValid(object value)
            => value is string password &&
                password.Length >= 8 &&
                    password.Any(c => char.IsDigit(c)) &&
                        password.Any(c => char.IsLetter(c));
    }
}
