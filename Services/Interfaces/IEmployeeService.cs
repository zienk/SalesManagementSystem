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
        Employee? GetEmployee(string username, string password);
        List<Employee> GetAllEmployees();
    }
}
