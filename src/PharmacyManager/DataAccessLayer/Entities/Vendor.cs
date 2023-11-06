using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Short_name { get; set; }
        public string Full_name { get; set; }
        public string Phone { get; set; }
        public int? Inn { get; set; }
        public int? Kpp { get; set; }
    }
}
