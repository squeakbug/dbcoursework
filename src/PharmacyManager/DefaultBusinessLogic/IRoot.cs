using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace DefaultBusinessLogic
{
    public interface IRoot : IEmployee
    {
        void Registrate(Employee employee, PersonMetadata metadata, string passwd, string repeatPasswd);
    }
}
