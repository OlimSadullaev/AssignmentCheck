using AssignmentCheck.Data.Contexts;
using AssignmentCheck.Data.IRepository;
using AssignmentCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AssignmentCheckDbContext dbContext;

        public UnitOfWork(AssignmentCheckDbContext dbContext)
        { 
            this.dbContext = dbContext;
            Users = new GenericRepository<User>(dbContext);
            Assignments = new GenericRepository<Assignment>(dbContext);
            Subjects = new GenericRepository<Subject>(dbContext);
        }

        public IGenericRepository<User> Users { get; }
        public IGenericRepository<Subject> Subjects { get; }
        public IGenericRepository<Assignment> Assignments { get; }

        public async ValueTask SaveChangesAsync() =>
            await dbContext.SaveChangesAsync();
    }
}
