using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTemperatures.Models
{
    public class EmployeeRecord : Employee
    {
        [Key]
        public Guid EmployeeNumber { get; set; }
        public double Temperature { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
