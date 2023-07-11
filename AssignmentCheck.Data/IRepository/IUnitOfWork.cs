using AssignmentCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Data.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<User> Users { get; }
        IGenericRepository<Assignment> Assignments { get; }
        IGenericRepository<Subject> Subjects { get; }
        ValueTask SaveChangesAsync();
    }
}
