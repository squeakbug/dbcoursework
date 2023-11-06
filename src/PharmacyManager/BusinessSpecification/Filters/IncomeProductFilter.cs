using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Filters
{
    public class IncomeProductFilter : BaseFilter
    {
        public int? MinProductCount { get; set; }
        public int? MaxProductCount { get; set; }
        public int? Series { get; set; }
        public int? MaxVendorPrice { get; set; }
        public int? MinVendorPrice { get; set; }
        public DateTime? MinProductionDate { get; set; }
        public DateTime? MaxProductionDate { get; set; }
        public string StorageLocationRegex { get; set; }
        public string NameRegex { get; set; }
    }
}
