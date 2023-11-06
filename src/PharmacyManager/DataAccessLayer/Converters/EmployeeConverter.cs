using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class EmployeeConverter
    {
        public Employee MapToBusinessEntity(Entities.Employee employee, Context globalCtx)
        {
            var personMetadataPer = new SqlPersonMetadataRepository(globalCtx);
            var result = new Employee()
            {
                id = employee.id,
                appointment = employee.appointment,
                e_login = employee.e_login,
                person_metadata_id = employee.metadata_id,
                p_hash = employee.p_hash
            };
            return result;
        }

        public Entities.Employee MapFromBusinessEntity(Employee employee)
        {
            return new Entities.Employee()
            {
                id = employee.id,
                appointment = employee.appointment,
                e_login = employee.e_login,
                metadata_id = employee.person_metadata_id,
                p_hash = employee.p_hash
            };
        }
    }
}
