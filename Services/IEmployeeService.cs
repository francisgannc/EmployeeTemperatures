using EmployeeTemperatures.Contracts.Request;
using EmployeeTemperatures.Contracts.Response;
using EmployeeTemperatures.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTemperatures.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest employee);
        Task<EmployeeResponse> UpdateEmployeeAsync(string employeeNumber, UpdateEmployeeRequest employee);
        Task<List<EmployeeRecord>> GetEmployeesAsync();
        Task<SearchEmployeeResponse> SearchEmployees(SearchEmployeeRequest search);
    }
}
