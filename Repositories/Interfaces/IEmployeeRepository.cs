using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        public Employee? GetEmployee(string username, string password);
        public List<Employee> GetAllEmployees();
    }
}
