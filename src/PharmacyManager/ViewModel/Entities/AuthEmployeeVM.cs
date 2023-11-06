using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class AuthEmployeeVM
    {
        public int Id { get; set; }
        public PersonMetadataVM PersonMetadata { get; set; }
        public string Login { get; set; }
        public string Passwd { get; set; }
        public string Appointment { get; set; }
    }
}
