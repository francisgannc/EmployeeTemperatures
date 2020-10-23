using EmployeeTemperatures.Contracts.Request;
using EmployeeTemperatures.Contracts.Response;
using EmployeeTemperatures.Data;
using EmployeeTemperatures.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTemperatures.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILoggerService loggerService;

        public EmployeeService(ApplicationDbContext _dbContext, ILoggerService _loggerService)
        {
            dbContext = _dbContext;
            loggerService = _loggerService;
        }

        public async Task<EmployeeResponse> CreateEmployeeAsync(CreateEmployeeRequest employee)
        {
            loggerService.LogExecute("EmployeeService.CreateEmployeeAsync", "");

            var newEmployee = new EmployeeRecord
            {
                EmployeeNumber = Guid.NewGuid(),
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Temperature = employee.Temperature,
                RecordDate = employee.RecordDate
            };

            await dbContext.EmployeeRecord.AddAsync(newEmployee);

            var saved = await dbContext.SaveChangesAsync();

            loggerService.LogExecutionResult(saved > 0, "EmployeeService.CreateEmployeeAsync", "created new employee record", "failed creating new employee record");

            return new EmployeeResponse
            {
                Success = saved > 0,
                EmployeeRecord = newEmployee
            };
        }

        public async Task<List<EmployeeRecord>> GetEmployeesAsync()
        {
            loggerService.LogExecute("EmployeeService.GetEmployeesAsync", "");

            var employees = await dbContext.EmployeeRecord.ToListAsync();

            loggerService.LogExecutionResult(employees != null, "EmployeeService.GetEmployeesAsync", "finished retrieving", "failed retrieving");

            return employees;
        }

        public async Task<EmployeeResponse> UpdateEmployeeAsync(string employeeNumber, UpdateEmployeeRequest employee)
        {
            loggerService.LogExecute("EmployeeService.UpdateEmployeeAsync", "");

            var _employee = await dbContext.EmployeeRecord.SingleOrDefaultAsync(x => x.EmployeeNumber == new Guid(employeeNumber));
            if (_employee == null)
            {
                return new EmployeeResponse
                {
                    Success = false,
                    Errors = new[] { "No Records found." }
                };
            }

            _employee.FirstName = !string.IsNullOrEmpty(employee.FirstName) ? employee.FirstName : _employee.FirstName;
            _employee.LastName = !string.IsNullOrEmpty(employee.LastName) ? employee.LastName : _employee.LastName;
            _employee.Temperature = employee.Temperature == _employee.Temperature ? employee.Temperature : _employee.Temperature;
            _employee.RecordDate = employee.RecordDate == _employee.RecordDate ? employee.RecordDate : _employee.RecordDate;

            dbContext.EmployeeRecord.Update(_employee);
            var saved = await dbContext.SaveChangesAsync();

            loggerService.LogExecutionResult(saved > 0, "EmployeeService.UpdateEmployeeAsync", "updated employee", "failed updating employee");

            return new EmployeeResponse
            {
                Success = saved > 0,
                EmployeeRecord = _employee
            };
        }

        public async Task<SearchEmployeeResponse> SearchEmployees(SearchEmployeeRequest search)
        {
            loggerService.LogExecute("EmployeeService.SearchEmployees", "");

            var employees = await dbContext.EmployeeRecord.ToListAsync();

            var filteredEmployess = employees.Where(x =>
                                        x.EmployeeNumber == search.EmployeeNumber ||
                                        x.FirstName == search.FirstName ||
                                        x.LastName == search.LastName ||
                                        (x.Temperature >= search.TemperatureFrom && x.Temperature <= search.TemperatureTo) ||
                                        (x.RecordDate >= search.DateFrom && x.RecordDate <= search.DateTo));

            loggerService.LogExecutionResult(filteredEmployess != null, "EmployeeService.SearchEmployees", "finished retrieving", "failed retrieving");

            return new SearchEmployeeResponse
            {
                Success = true,
                EmployeeRecords = filteredEmployess.ToList()
            };
        }
    }
}
