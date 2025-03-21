﻿using Microsoft.EntityFrameworkCore;
using ClassManager.Api.Data;
using ClassManager.Api.Data.Entities;
using ClassManager.Shared;
using ClassManager.Shared.DTOs;

namespace ClassManager.Api.Services
{
    public class DepartmentService
    {
        private readonly TaskContext _task;

        public DepartmentService(TaskContext task)
        {
            _task = task;
        }

        public async Task<QuizApiResponse> SaveDepartmentAsync(DepartmentDto dept)
        {
            if (await _task.Departments
                          .AsNoTracking()
                          .AnyAsync(c => c.Name == dept.Name && c.Id != dept.Id))
            {
                return QuizApiResponse.Fail("Department with same name exists already"); ;
            }

            if (dept.Id == 0)
            {
                var department = new Department
                {
                    Name = dept.Name,
                    Subject = dept.Subject,
                    AccessCode = dept.AccessCode
                };
                _task.Departments.Add(department);
            }
            else
            {
                var dbDepartment = await _task.Departments.FirstOrDefaultAsync(c => c.Id == dept.Id);
                if (dbDepartment == null)
                {
                    return QuizApiResponse.Fail("Department does not exist");
                }
                dbDepartment.Name = dept.Name;
                dbDepartment.Subject = dept.Subject;
                dbDepartment.AccessCode = dept.AccessCode;

                _task.Departments.Update(dbDepartment);
            }
            await _task.SaveChangesAsync();
            return QuizApiResponse.Success();


        }

        public async Task<DepartmentDto[]> GetDepartmentsAsync() =>
               await _task.Departments
                  .AsNoTracking()
                  .Select(c => new DepartmentDto
                  {
                      Name = c.Name,
                      Subject = c.Subject, 
                      AccessCode = c.AccessCode,
                      Id = c.Id
                  })
                 .ToArrayAsync();

        public async Task<bool> ValidateSubjectAccessAsync(ValidateSubjectDto dto)
        {
            if (dto.SubjectId.HasValue)
            {
                return await _task.Departments
                    .AsNoTracking()
                    .AnyAsync(d => d.Id == dto.SubjectId.Value && d.AccessCode == dto.AccessCode);
            }
            else if (!string.IsNullOrWhiteSpace(dto.Subject))
            {
                return await _task.Departments
                    .AsNoTracking()
                    .AnyAsync(d => d.Subject == dto.Subject && d.AccessCode == dto.AccessCode);
            }

            return false;
        }

    }
}
