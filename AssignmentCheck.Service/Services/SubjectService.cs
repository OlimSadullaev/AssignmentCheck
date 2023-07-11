﻿using AssignmentCheck.Data.IRepository;
using AssignmentCheck.Domain.Configurations;
using AssignmentCheck.Domain.Entities;
using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Exceptions;
using AssignmentCheck.Service.Extensions;
using AssignmentCheck.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<Subject> CreateAsync(SubjectForCreationDTO subjectForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Subjects.GetAsync(
                s => s.Name == subjectForCreationDTO.Name);

            if (alreadyExists != null)
                throw new AssignmentCheckException(400, "Subject with this name is already exists.");

            var subject = await unitOfWork.Subjects.CreateAsync(subjectForCreationDTO.Adapt<Subject>());
            await unitOfWork.SaveChangesAsync();

            return subject;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Subject, bool>> expression)
        {
            if(!(await unitOfWork.Subjects.DeleteAsync(expression)))
                throw new AssignmentCheckException(404, "Subject not found.");

            await unitOfWork.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<Subject>> GetAllAsync(PaginationParams @params = null, Expression<Func<Subject, bool>> expression = null)
        {
            throw new NotImplementedException();
            /*var subjects = unitOfWork.Subjects.GetAll(expression, new string[] { "Assignment" }, false);

            if (@params != null)
                return await subjects.ToPagedList(@params.Adapt<>).ToList();

            return subjects.ToListAsync();*/
        }

        /*public ValueTask<IEnumerable<SubjectForCreationDTO>> GetAll(Expression<Func<Subject, bool>> expression)
        {
            var subjectRepository = unitOfWork.Subjects;

            var subjects = subjectRepository.GetAll(expression);

            //SubjectForCreationDTO subjectForCreationDTO = new SubjectForCreationDTO();

            var subjectDTOs = subjects.Select(s => new SubjectForCreationDTO
            {
                Name = s.Name,
                Description = s.Description,
                // Map other properties as needed
            });

            return subjectDTOs.ToList();
        }*/

        public async ValueTask<Subject> GetAsync(Expression<Func<Subject, bool>> expression) =>
            await unitOfWork.Subjects.GetAsync(expression, new string[] { "Assignment" }) ??
                throw new AssignmentCheckException(404, "Subject not found");

        public async ValueTask<Subject> UpdateAsync(Guid id, SubjectForCreationDTO subjectForCreationDTO)
        {
            var alreadyExists = await unitOfWork.Subjects.GetAsync(
                s => s.Name == subjectForCreationDTO.Name && s.Id != id);

            if (alreadyExists != null)
                throw new AssignmentCheckException(400, "Subject with such a name already exists");

            var subject = await GetAsync(s => s.Id == id);
            
            subject.UpdatedAt = DateTime.UtcNow;

            subject = unitOfWork.Subjects.Update(subjectForCreationDTO.Adapt(subject));
            await unitOfWork.SaveChangesAsync();

            return subject;
        }
    }
}