using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    class DefaultRoot : BaseEmployee, IRoot
    {
        private IAuthManager _authManager { get; set; }

        public DefaultRoot(IRepositoryFactory factory, Employee employee, IAuthManager authManager)
            : base(factory, employee)
        {
            _authManager = authManager;
        }

        public void Registrate(Employee employee, PersonMetadata metadata, string passwd, string repeatPasswd)
        {
            if (employee == null || metadata == null || passwd == null || repeatPasswd == null)
                throw new RootException("Registrate args must be not null values");
            if (employee.e_login == null || employee.appointment == null || employee.e_login == null)
                throw new RootException("No enough information to registrate") { Root = this };
            if (metadata.First_name == null || metadata.Middle_name == null || metadata.Phone == null)
                throw new RootException("No enough information to registrate") { Root = this };
            if (passwd != repeatPasswd)
                throw new RootException("Incorrect repeated password") { Root = this };
            using (IEmployeeRepository employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                if (employeeRep.GetEmployeeByLogin(employee.e_login) != null)
                    throw new RootException("Login is occupied") { Root = this };
            }
            if (!_authManager.IsAppointmentValid(employee.appointment))
                throw new RootException("Appoinment is invalid") { Root = this };
            if (passwd.Length < 6)
                throw new PharmacyManagerException("Password length must be greater than 5");

            string hashedPasswd = _authManager.HashPassword(passwd);
            employee.p_hash = hashedPasswd;
            using (IEmployeeRepository employeeRep = RepositoryFactory.CreateEmployeeRep())
            {
                employeeRep.RegistrateEmployee(employee, metadata);
            }
        }
    }
}
