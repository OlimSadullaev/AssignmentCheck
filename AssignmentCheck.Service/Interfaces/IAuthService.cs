﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Interfaces
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string email, string password);
    }
}
