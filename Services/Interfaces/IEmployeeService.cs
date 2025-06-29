using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        public Employee? GetEmployee(string username, string password);
        public List<Employee> GetAllEmployees();
    }
}
