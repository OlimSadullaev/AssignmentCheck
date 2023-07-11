using AssignmentCheck.Domain.Configurations;
using AssignmentCheck.Domain.Entities;
using AssignmentCheck.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Interfaces
{
    public interface ISubjectService
    {
        ValueTask<Subject> CreateAsync(SubjectForCreationDTO subjectForCreationDTO);
        ValueTask<Subject> UpdateAsync(Guid Id, SubjectForCreationDTO subjectForCreationDTO);
        ValueTask<bool> DeleteAsync(Expression<Func<Subject, bool>> expression);
        ValueTask<IEnumerable<Subject>> GetAllAsync(
            PaginationParams @params = null, 
            Expression<Func<Subject, bool>> expression = null);
        ValueTask<Subject> GetAsync(Expression<Func<Subject, bool>> expression);
    }
}
