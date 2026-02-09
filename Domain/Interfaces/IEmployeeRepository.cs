using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        // Agregamos paginación aquí para cumplir con el requisito
        Task<(IEnumerable<Employee> Items, int TotalCount)> GetAllAsync(int page, int pageSize, string searchTerm);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<int> CountByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
