using AssignmentCheck.Data.IRepository;
using AssignmentCheck.Domain.Configurations;
using AssignmentCheck.Domain.Entities;
using AssignmentCheck.Domain.Enums;
using AssignmentCheck.Service.Attributes;
using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Exceptions;
using AssignmentCheck.Service.Extensions;
using AssignmentCheck.Service.Helpers;
using AssignmentCheck.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<bool> ChangePasswordAsync(string oldPassword, [UserPassword] string newPassword)
        {
            var user = await unitOfWork.Users.GetAsync(u => u.Id == HttpContextHelper.UserId);

            if (user == null)
                throw new AssignmentCheckException(404, "User not found");

            if (user.Password != oldPassword.Encrypt());
            {
                throw new AssignmentCheckException(404, "Password is incorrect");
            }
            user.Password = newPassword.Encrypt();

            unitOfWork.Users.Update(user);
            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async ValueTask<bool> ChangeRoleAsync(Guid id, UserRole userRole)
        {
            var existUser = await unitOfWork.Users.GetAsync(u => u.Id == id);
            if (existUser == null)
                throw new AssignmentCheckException(404, "User not found");

            existUser.Role = userRole;

            unitOfWork.Users.Update(existUser);
            await unitOfWork.SaveChangesAsync();    

            return true;
        }

        public async ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            var alreadyExistUser = await unitOfWork.Users.GetAsync(u => u.Email == userForCreationDTO.Email);

            if (alreadyExistUser != null)
                throw new AssignmentCheckException(400, "User with such an email already exists.");

            userForCreationDTO.Password = userForCreationDTO.Password.Encrypt();

            var user = await unitOfWork.Users.CreateAsync(userForCreationDTO.Adapt<User>());
            user.CreatedAt = DateTime.UtcNow;
            await unitOfWork.SaveChangesAsync();
            return user.Adapt<UserForViewDTO>();
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var deleted = await unitOfWork.Users.DeleteAsync(expression);
            if (!deleted)
                throw new AssignmentCheckException(404, "User not found.");
            
            return true;
        }

        public async ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(PaginationParams @object, Expression<Func<User, bool>> expression)
        {
            var users = unitOfWork.Users.GetAll(expression: expression, isTracing: false);

            return (await users.ToPagedList(@object).ToListAsync()).Adapt<List<UserForViewDTO>>();
        }

        public async ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await unitOfWork.Users.GetAsync(expression);

            if (user is null)
                throw new AssignmentCheckException(404, "User not found");

            return user.Adapt<UserForViewDTO>();
        }

        public async ValueTask<UserForViewDTO> UpdateAsync(string pasword, UserForUpdateDTO userForUpdateDTO)
        {
            var alreadyExistUser = await unitOfWork.Users.GetAsync(u => u.Email == userForUpdateDTO.Email && 
                                                                        u.Id != HttpContextHelper.UserId);

            if (alreadyExistUser != null)
                throw new AssignmentCheckException(404, "User with such email already exists.");

            var existsUser = await GetAsync(/*u => u.Email == email &&*/u => u.Password == pasword.Encrypt());

            if(existsUser == null)
                throw new AssignmentCheckException(404, "Email or Password incorrect.");

            var user = existsUser.Adapt<User>();

            user.UpdatedAt = DateTime.UtcNow;

            user = unitOfWork.Users.Update(user = userForUpdateDTO.Adapt(user));
            await unitOfWork.SaveChangesAsync();
            return user.Adapt<UserForViewDTO>();
        }

        public async Task<Guid?> GetUserIdByEmail(string email)
        {
            // Assuming you have a user repository or data access layer
            // that provides access to the user data
            var user = await unitOfWork.Users.GetUserByEmail(email);

            // Check if the user exists
            if (user != null)
            {
                return user; // Return the user ID
            }

            return null; // User not found, return null
        }
    }
}
