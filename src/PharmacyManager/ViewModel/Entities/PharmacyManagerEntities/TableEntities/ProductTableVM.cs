using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.PharmacyManagerEntities.TableEntities
{
    public class ProductTableVM
    {
        public string Id { get; set; }
        public string P_name { get; set; }
        public string Gtin { get; set; }
        public string Articul { get; set; }
        public string Trademark { get; set; }
        public string Threashold_count { get; set; }
        public string Dosage { get; set; }
        public string Storage_temperature { get; set; }
        public string Maximum_shelf_life { get; set; }
        public string Leave_condition { get; set; }
    }
}
