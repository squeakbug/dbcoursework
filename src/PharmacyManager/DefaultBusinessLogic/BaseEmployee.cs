using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DefaultBusinessLogic
{
    public class BaseEmployee : IEmployee
    {
        public IRepositoryFactory RepositoryFactory { get; set; }
        public Employee Employee { get; }

        public BaseEmployee(IRepositoryFactory repositoryFactory, Employee employee)
        {
            Employee = employee;
            RepositoryFactory = repositoryFactory;
        }

        public Employee GetInnerEmployee()
        {
            return Employee;
        }

        public PersonMetadata GetInnerEmployeeMetadata()
        {
            using (var metadataRep = RepositoryFactory.CreatePersonMetadataRep())
            {
                return metadataRep.GetPersonMetadataById(Employee.person_metadata_id);
            }
        }
    }
}
