using System;
using System.ComponentModel;
using System.Collections.ObjectModel;

using BusinessSpecification.Entities;

namespace ViewModel.Entities
{
    public class RegistrateEmployeeVM
    {
        public PersonMetadataVM PersonMetadata { get; set; }
        public string Login { get; set; }
        public string Passwd { get; set; }
        public string RepeatPasswd { get; set; }
        public string Appointment { get; set; }
    }
}
