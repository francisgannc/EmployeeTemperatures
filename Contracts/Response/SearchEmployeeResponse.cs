using EmployeeTemperatures.Models;
using System.Collections.Generic;

namespace EmployeeTemperatures.Contracts.Response
{
    public class SearchEmployeeResponse
    {
        public bool Success { get; set; }
        public List<EmployeeRecord> EmployeeRecords { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
