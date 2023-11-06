using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.ProductManagerEntities
{
    public class MarkedProductDetailsVM
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ApprovedDate { get; set; }
        public string Count { get; set; }
        public string StorageLocation { get; set; }
    }
}
