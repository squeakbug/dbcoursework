using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using DataAccessLayer.Converters;
using DataAccessLayer.Exceptions;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DataAccessLayer.RepositoriesImp
{
    public class SqlEmployeeRepository : IEmployeeRepository
    {
        private Context _globalCtx;

        public SqlEmployeeRepository(Context globalCtx)
        {
            _globalCtx = globalCtx;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var converter = new EmployeeConverter();
            var result = new List<Employee>();
            foreach (var item in _globalCtx.Employee)
                result.Add(converter.MapToBusinessEntity(item, _globalCtx));
            return result;
        }

        public Employee GetEmployeeById(int id)
        {
            var converter = new EmployeeConverter();
            var employee = _globalCtx.Employee.Find(id);
            return employee == null ? null : converter.MapToBusinessEntity(employee, _globalCtx);
        }

        public Employee GetEmployeeByLogPassword(string login, string hashedPassword)
        {
            var converter = new EmployeeConverter();
            var employee = (from empl in _globalCtx.Employee
                            where empl.e_login == login && empl.p_hash == hashedPassword
                            select empl);
            if (employee.Count() == 0)
                return null;
            return converter.MapToBusinessEntity(employee.FirstOrDefault(), _globalCtx);
        }

        public Employee GetEmployeeByLogin(string login)
        {
            var converter = new EmployeeConverter();
            var employee = from empl in _globalCtx.Employee
                           where empl.e_login == login
                           select empl;
            if (employee.Count() == 0)
                return null;
            return converter.MapToBusinessEntity(employee.FirstOrDefault(), _globalCtx);
        }

        public void RegistrateEmployee(Employee employee, PersonMetadata metadataBL)
        {
            var emplConverter = new EmployeeConverter();
            var metadataConverter = new PersonMetadataConverter();
            Entities.PersonMetadata metadataDB = metadataConverter.MapFromBusinessEntity(metadataBL);
            _globalCtx.Add(metadataDB);
            _globalCtx.SaveChanges();

            employee.person_metadata_id = metadataDB.Id;
            _globalCtx.Employee.Add(emplConverter.MapFromBusinessEntity(employee));
            _globalCtx.SaveChanges();
            _globalCtx.Database.ExecuteSqlInterpolated(
                $"EXEC dbo.RegistrateUser {employee.e_login}, {employee.p_hash}, {employee.appointment}");
            _globalCtx.SaveChanges();
        }

        public void Create(Employee employee)
        {
            var converter = new EmployeeConverter();
            _globalCtx.Employee.Add(converter.MapFromBusinessEntity(employee));
            _globalCtx.SaveChanges();
        }

        public void Update(Employee employee)
        {
            var converter = new EmployeeConverter();
            _globalCtx.Employee.Update(converter.MapFromBusinessEntity(employee));
            _globalCtx.SaveChanges();
        }

        public void Delete(int id)
        {
            var employee = _globalCtx.Employee.Find(id);
            if (employee == null)
                throw new EmployeeException("No employee with such id");

            _globalCtx.Employee.Remove(employee);
            _globalCtx.SaveChanges();
        }

        public void Truncate()
        {
            _globalCtx.Database.ExecuteSqlInterpolated($"DELETE dbo.employee");
            _globalCtx.SaveChanges();
        }

        private bool disposed = false;

        private void CleanUp(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _globalCtx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }
    }
}
