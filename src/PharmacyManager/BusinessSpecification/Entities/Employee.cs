using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Entities
{
    public class Employee
    {
        public int id { get; set; }
        public int person_metadata_id { get; set; }
        public string e_login { get; set; }
        public string p_hash { get; set; }
        public string appointment { get; set; }
    }
}
