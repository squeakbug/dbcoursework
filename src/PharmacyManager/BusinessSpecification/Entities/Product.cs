using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int Manufacturer_id { get; set; }
        //public IEnumerable<int> Categories_id { get; set; }
        public string P_name { get; set; }
        public string International_name { get; set; }
        public string Gtin { get; set; }
        public string Articul { get; set; }
        public string Primacy_packaging { get; set; }
        public int? Threashold_count { get; set; }
        public string Trademark { get; set; }
        public double? Dosage { get; set; }
        public double Storage_temperature { get; set; }
        public int Maximum_shelf_life { get; set; }
        public string Leave_condition { get; set; }
        public string P_description { get; set; }
        public string Instruction { get; set; }
        public Byte[] Photo { get; set; }
        public int? Count_in_package { get; set; }
        public string Dosage_form { get; set; }
        public Decimal? Maximum_markup { get; set; }
        public Decimal Default_markup { get; set; }
    }
}
