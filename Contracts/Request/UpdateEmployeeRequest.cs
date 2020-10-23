using EmployeeTemperatures.Models;
using System;

namespace EmployeeTemperatures.Contracts.Request
{
    public class UpdateEmployeeRequest : Employee
    {
        public double Temperature { get; set; }

        public DateTime RecordDate { get; set; }
    }
}
