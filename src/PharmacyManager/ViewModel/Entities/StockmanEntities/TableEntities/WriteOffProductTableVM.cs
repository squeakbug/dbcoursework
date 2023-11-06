using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities.StockmanEntities.TableEntities
{
    public class WriteOffProductTableVM
    {
        public string Id { get; set; }
        public string WriteOffDate { get; set; }
        public string Count { get; set; }
        public string StorageLocation { get; set; }
    }
}
