using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class MarkedProductConverter
    {
        public static MarkedProductVM MapToViewModel(MarkedProduct markedProduct)
        {
            return new MarkedProductVM
            {
                Id = markedProduct.Id.ToString(),
                Mark_info_id = markedProduct.Mark_info_id.ToString(),
                Sold = markedProduct.Sold.ToString()
            };
        }
    }
}
