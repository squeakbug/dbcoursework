using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using BusinessSpecification.Filters;

namespace DefaultBusinessLogic
{
    public class MarkedProductFilter : BaseFilter
    {
        public int? MinProductCount { get; set; }
        public int? MaxProductCount { get; set; }
        public string StorageLocationRegex { get; set; }
        public string NameRegex { get; set; }
    }
}
