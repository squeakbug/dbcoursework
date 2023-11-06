using System;
using System.Collections.Generic;

using BusinessSpecification.Entities;

namespace BusinessSpecification.RepositoryInterfaces
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByLogPassword(string login, string password);
        Employee GetEmployeeByLogin(string login);
        void RegistrateEmployee(Employee employee, PersonMetadata metadataBL);
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        void Truncate();
    }
}
