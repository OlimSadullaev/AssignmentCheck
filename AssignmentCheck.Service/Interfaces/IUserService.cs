using AssignmentCheck.Domain.Configurations;
using AssignmentCheck.Domain.Entities;
using AssignmentCheck.Domain.Enums;
using AssignmentCheck.Service.Attributes;
using AssignmentCheck.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO);
        ValueTask<UserForViewDTO> UpdateAsync(string email, string pasword, UserForUpdateDTO userForUpdateDTO);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(PaginationParams @object, 
                                                      Expression<Func<User, bool>> expression);
        ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<bool> ChangeRoleAsync(Guid id, UserRole userRole);
        ValueTask<bool> ChangePasswordAsync(string oldPassword, [UserPassword] string newPassword);
    }
}
