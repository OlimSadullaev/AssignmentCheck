using AssignmentCheck.Data.Contexts;
using AssignmentCheck.Data.IRepositories;
using AssignmentCheck.Domains.Commons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
    {
        private readonly AssignmentCheckDbContext dbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AssignmentCheckDbContext dbContext, DbSet<T> dbSet)
        {
            this.dbContext = dbContext;
            this.dbSet = dbSet;
        }

        public async ValueTask<T> CreateAsync(T entity) =>
            (await dbContext.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entity = await GetAsync(expression);

            if(entity == null) 
                return false;

            dbSet.Remove(entity);

            return true;
        }

        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> expression, 
            string[] includes = null, 
            bool isTracing = true)
        {
            IQueryable<T> query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes != null)
                foreach (var include in includes)
                    if (!string.IsNullOrEmpty(include))
                        query = query.Include(include);

            if(!isTracing)
                    query = query.AsNoTracking();

            return query;
        }

        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null) =>
            await GetAll(expression, includes).FirstOrDefaultAsync();

        public T Update(T entity) =>
            dbSet.Update(entity).Entity;
    }
}
