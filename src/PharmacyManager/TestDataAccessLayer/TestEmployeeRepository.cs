using System;
using Xunit;
using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestEmployeeRepository
    {
        private static bool _setuped = false;

        [Fact]
        public void TestEmployeeCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource : "127.0.0.1,1433",
                initialCatalog : "pharmacy",
                user : "SA",
                passwd : "P@ssword"
            );

            SetupEmployeeRepository(factory);

            Employee tmpEmployee = null;
            using (var rep = factory.CreateEmployeeRep())
            {
                tmpEmployee = rep.GetEmployeeById(1);
            }
            using (var rep = factory.CreateEmployeeRep())
            {
                Assert.NotNull(tmpEmployee);
                Assert.Equal("EL1", tmpEmployee.e_login);
                Assert.Equal("PMFN1", tmpEmployee.person_metadata.First_name);

                var employees = new List<Employee>(rep.GetEmployees());
                Assert.Equal(3, employees.Count);
            }

            using (var rep = factory.CreateEmployeeRep())
            {
                tmpEmployee = rep.GetEmployeeById(1);
            }

            using (var rep = factory.CreateEmployeeRep())
            {
                tmpEmployee.e_login = "EL4";
                rep.Update(tmpEmployee);

                tmpEmployee = rep.GetEmployeeById(1);
                Assert.Equal("EL4", tmpEmployee.e_login);

                rep.Delete(tmpEmployee.id);
                var employees = new List<Employee>(rep.GetEmployees());
                Assert.Equal(2, employees.Count);
            }

            ReleaseEmployeeRepository(factory);
        }

        public static void SetupEmployeeRepository(SqlRepositoryFactory factory)
        {
            if (_setuped)
                return;

            TestPersonMetadataRepository.SetupPersonMetadataRepository(factory);

            List<Employee> employeeSample = GetEmployeeSample(factory);
            using (var rep = factory.CreateEmployeeRep())
            {
                rep.Create(employeeSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateEmployeeRep())
            {
                rep.Create(employeeSample[0]);
                rep.Create(employeeSample[1]);
                rep.Create(employeeSample[2]);
            }
            _setuped = true;
        }

        public static void ReleaseEmployeeRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateEmployeeRep())
                rep.Truncate();

            TestPersonMetadataRepository.ReleasePersonMetadataRepository(factory);

            _setuped = false;
        }

        public static List<Employee> GetEmployeeSample(SqlRepositoryFactory factory)
        {
            List<PersonMetadata> personMetadatas = null;
            using (var rep = factory.CreatePersonMetadataRep())
                personMetadatas = new List<PersonMetadata>(rep.GetPersonMetadatas());

            return new List<Employee>
            {
                new Employee
                { 
                    e_login = "EL1", appointment = "EA1",
                    person_metadata = personMetadatas[0], p_hash = "EPH1"
                },
                new Employee
                {
                    e_login = "EL2", appointment = "EA2",
                    person_metadata = personMetadatas[1], p_hash = "EPH2"
                },
                new Employee
                {
                    e_login = "EL3", appointment = "EA3",
                    person_metadata = personMetadatas[2], p_hash = "EPH3"
                },
            };
        }
    }
}
