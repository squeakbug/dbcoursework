using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.StockmanEntities.TableEntities
{
    public class IncomeProductTableVM
    {
        public string Id { get; set; }
        public string Count { get; set; }
        public string MeasureUnit { get; set; }
        public string Series { get; set; }
        public string VendorPrice { get; set; }
        public string VendorVax { get; set; }
        public string ProductionDate { get; set; }
        public string StorageLocation { get; set; }
    }
}
