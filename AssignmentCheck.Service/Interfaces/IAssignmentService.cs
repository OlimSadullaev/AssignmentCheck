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
    public interface IAssignmentService
    {
        ValueTask<Assignment> UploadAsync(AssignmentForCreationDTO dto);
        ValueTask<Assignment> UpdateAsync(Guid id, Stream stream);
        ValueTask<bool> DeleteAsync(Expression<Func<Assignment, bool>> expression);
        ValueTask<Assignment> GetAsync(Expression<Func<Assignment, bool>> expression);
        ValueTask<Assignment> CreateAsync(string fileName, string filePath);
    }
}
