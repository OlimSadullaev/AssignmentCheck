using AssignmentCheck.Data.IRepository;
using AssignmentCheck.Domain.Entities;
using AssignmentCheck.Service.DTOs;
using AssignmentCheck.Service.Exceptions;
using AssignmentCheck.Service.Helpers;
using AssignmentCheck.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IUnitOfWork unitOfWork;

        public AssignmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<Assignment> CreateAsync(string fileName, string filePath)
        {
            var file = new Assignment()
            {
                Name = fileName,
                Path = filePath,
            };

            file = await unitOfWork.Assignments.CreateAsync(file);
            await unitOfWork.SaveChangesAsync();

            return file;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Assignment, bool>> expression)
        {
            var file = await unitOfWork.Assignments.GetAsync(expression);

            if (file is null)
                throw new AssignmentCheckException(404, "File not found.");

            //delete file from wwwroot
            string fullPath = Path.Combine(EnvironmentHelper.WebRootPath, file.Path);

            if(File.Exists(fullPath))
                File.Delete(fullPath);

            //delete database information
            FileHelper.Remove(file.Path);

            await unitOfWork.Assignments.DeleteAsync(expression);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<Assignment> GetAsync(Expression<Func<Assignment, bool>> expression)
        {
            var existAssignment = await unitOfWork.Assignments.GetAsync(expression);

            return existAssignment ?? throw new AssignmentCheckException(404, "File not found.");
        }

        public async ValueTask<Assignment> UpdateAsync(Guid id, Stream stream)
        {
            var existAttachment = await unitOfWork.Assignments.GetAsync(a => a.Id == id, null);

            if (existAttachment is null)
                throw new AssignmentCheckException(404, "Attachment not found.");

            string fileName = existAttachment.Name;
            string filePath = Path.Combine(EnvironmentHelper.WebRootPath, fileName);

            //copy images to the destionation as stream
            FileStream fileStream = File.OpenRead(filePath);
            await stream.CopyToAsync(fileStream);

            //clear
            await fileStream.FlushAsync();
            fileStream.Close();

            await unitOfWork.SaveChangesAsync();

            return existAttachment;
        }

        public async ValueTask<Assignment> UploadAsync(AssignmentForCreationDTO dto)
        {
            // generate file destination
            string fileName = Guid.NewGuid().ToString("N") + "-" + dto.Name;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);
            
            if(!Directory.Exists(EnvironmentHelper.WebRootPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            //copy image to the destionation as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            await fileStream.FlushAsync();
            fileStream.Close();

            return await CreateAsync(fileName, Path.Combine(EnvironmentHelper.FilePath, fileName));
        }
    }
}
