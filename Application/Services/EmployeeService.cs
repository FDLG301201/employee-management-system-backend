using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetAllAsync(int page, int pageSize, string searchTerm);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto);
        Task UpdateAsync(int id, CreateEmployeeDto dto);
        Task DeleteAsync(int id);
    }
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var entity = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                HireDate = dto.HireDate,
                JobTitle = dto.JobTitle,
            };

            var created = await _repository.AddAsync(entity);

            return MapToDto(created);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<PagedResult<EmployeeDto>> GetAllAsync(int page, int pageSize, string searchTerm)
        {
            var (items, total) = await _repository.GetAllAsync(page, pageSize, searchTerm);

            var dtos = items.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                JobTitle = e.JobTitle,
                HireDate = e.HireDate
            });

            return new PagedResult<EmployeeDto>
            {
                Items = dtos,
                TotalCount = total,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            if (e == null) return null;
            return MapToDto(e);
        }

        public async Task UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("Employee not found");

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.JobTitle = dto.JobTitle;
            existing.HireDate = dto.HireDate;

            await _repository.UpdateAsync(existing);
        }

        private static EmployeeDto MapToDto(Employee e) => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            JobTitle = e.JobTitle,
            HireDate = e.HireDate
        };
    }
}
