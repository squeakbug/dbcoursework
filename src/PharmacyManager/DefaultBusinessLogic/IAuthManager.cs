using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;
using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IAuthManager
    {
        Employee Login(string login, string passwd, out IStockman stockman, out IPharmacist pharmacist,
            out IPharmacyManager pharmacyManager, out IProductManager productManager, out IRoot root);
        void Logout(ref IStockman stockman, ref IPharmacist pharmacist,
            ref IPharmacyManager pharmacyManager, ref IProductManager productManager, ref IRoot root);
        string HashPassword(string passwd);
        bool IsAppointmentValid(string appointment);
        Employee GetAuthedEmployee();
        Employee GetEmployeeById(int id);
        PersonMetadata GetPersonMetadataById(int id);
        public IEnumerable<string> GetAppointments();
    }
}
