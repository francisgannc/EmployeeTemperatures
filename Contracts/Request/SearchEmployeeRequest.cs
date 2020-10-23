using EmployeeTemperatures.Models;
using System;

namespace EmployeeTemperatures.Contracts.Request
{
    public class SearchEmployeeRequest : Employee
    {
        public Guid EmployeeNumber { get; set; }
        public double TemperatureFrom { get; set; }
        public double TemperatureTo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
