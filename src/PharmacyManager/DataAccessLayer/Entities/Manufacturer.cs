using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string M_name { get; set; }
        public string Country { get; set; }
        public string Concern { get; set; }
    }
}
