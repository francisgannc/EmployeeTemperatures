using EmployeeTemperatures.Models;
using System.Collections.Generic;

namespace EmployeeTemperatures.Contracts.Response
{
    public class EmployeeResponse
    {
        public bool Success { get; set; }
        public EmployeeRecord EmployeeRecord{ get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
