using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class PersonMetadata
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Last_name { get; set; }
    }
}
