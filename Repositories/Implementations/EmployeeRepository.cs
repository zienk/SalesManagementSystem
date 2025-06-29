using DataAccessLayer.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LucySalesDataContext _context;

        public EmployeeRepository()
        {
            _context = new LucySalesDataContext();
        }

        public Employee? GetEmployee(string username, string password)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.UserName == username && e.Password == password);
            return employee;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
    }
}
