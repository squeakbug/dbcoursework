using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.StockmanEntities
{
    public class IncomeProductDetailsVM
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Count { get; set; }
        public string MeasureUnit { get; set; }
        public string Series { get; set; }
        public string ProductionDate { get; set; }
        public string StorageLocation { get; set; }
    }
}
