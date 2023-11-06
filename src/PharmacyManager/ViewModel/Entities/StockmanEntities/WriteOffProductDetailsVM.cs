using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.StockmanEntities
{
    public class WriteOffProductDetailsVM
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string WriteOffDate { get; set; }
        public string Count { get; set; }
        public string StorageLocation { get; set; }
        public string Reason { get; set; }
    }
}
