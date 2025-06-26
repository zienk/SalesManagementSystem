using DataAccessLayer.Entities;
using Repositories.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepo;

        public EmployeeService()
        {
            _employeeRepo = new EmployeeRepository();
        }

        public Employee? GetEmployee(string username, string password)
        => _employeeRepo.GetEmployee(username, password);


    }
}
