using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic.Validators
{
    public static class EmployeeValidator
    {
        public static void Validate(Employee employee)
        {
            if (employee.appointment != "pharmacy_manager"
                && employee.appointment != "product_manager"
                && employee.appointment != "pharmacist"
                && employee.appointment != "stockman"
                && employee.appointment != "root")
            {
                throw new ApplicationException("Bad employee appointment");
            }

            if (employee.e_login == null || employee.e_login.Length < 4)
                throw new ApplicationException("Bad login");
        }
    }
}
