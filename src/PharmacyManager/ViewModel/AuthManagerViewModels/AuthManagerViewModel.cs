using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;
using DefaultBusinessLogic;
using ViewModel.Entities;

namespace ViewModel.AuthManagerViewModels
{
    public class AuthManagerViewModel : BaseNotify
    {
        public IAuthManager AuthManager { get; }

        public AuthManagerViewModel()
        {
            AuthManager = Facade.AuthManager;
        }

        public AuthEmployeeVM Auth(string login, string passwd)
        {
            Employee empl = Facade.Login(login, passwd);
            if (empl == null)
                throw new ApplicationException("No employee with such login and password");
            var metadata = AuthManager.GetPersonMetadataById(empl.person_metadata_id);
            if (metadata == null)
                throw new ApplicationException("No metadata for loggining employee");

            return new AuthEmployeeVM
            {
                Appointment = empl.appointment,
                Id = empl.id,
                Login = empl.e_login,
                Passwd = empl.p_hash,
                PersonMetadata = new PersonMetadataVM
                {
                    Email = metadata.Email,
                    First_name = metadata.First_name,
                    Id = metadata.Id,
                    Last_name = metadata.Last_name,
                    Middle_name = metadata.Middle_name,
                    Phone = metadata.Phone
                }
            };
        }
    }
}
