using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IEmployee
    {
        Employee GetInnerEmployee();
        PersonMetadata GetInnerEmployeeMetadata();
    }
}
