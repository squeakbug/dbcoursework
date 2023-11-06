using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class MarkInfoConverter
    {
        public static MarkInfoVM MapToViewModel(MarkInfo markInfo)
        {
            return new MarkInfoVM
            {
                approved_date = markInfo.approved_date.ToString(),
                Approved_employee_id = markInfo.Approved_employee_id.ToString(),
                Id = markInfo.Id.ToString(),
                Income_product_id = markInfo.Income_product_id.ToString(),
                markup = markInfo.markup.ToString(),
                P_count = markInfo.P_count.ToString(),
                Storage_location = markInfo.Storage_location
            };
        }

        public static MarkInfo MapToModel(MarkInfoVM markInfoVM)
        {
            var markInfoBL = new MarkInfo();
            if (markInfoVM.Id != null && markInfoVM.Id.Length != 0)
            {
                if (!int.TryParse(markInfoVM.Id, out int markInfoId))
                    throw new ApplicationException("Bad mark info id");
                markInfoBL.Id = markInfoId;
            }
            if (markInfoVM.approved_date != null && markInfoVM.approved_date.Length != 0)
            {
                if (!DateTime.TryParse(markInfoVM.approved_date, out DateTime approvedDate))
                    throw new ApplicationException("Bad approved date");
                markInfoBL.approved_date = approvedDate;
            }
            if (markInfoVM.Approved_employee_id != null && markInfoVM.Approved_employee_id.Length != 0)
            {
                if (!int.TryParse(markInfoVM.Approved_employee_id, out int approvedEmployeeId))
                    throw new ApplicationException("Bad approved employee id");
                markInfoBL.Approved_employee_id = approvedEmployeeId;
            }
            if (markInfoVM.Income_product_id != null && markInfoVM.Income_product_id.Length != 0)
            {
                if (!int.TryParse(markInfoVM.Income_product_id, out int incomeProductId))
                    throw new ApplicationException("Bad income product Id");
                markInfoBL.Income_product_id = incomeProductId;
            }
            if (markInfoVM.markup != null && markInfoVM.markup.Length != 0)
            {
                if (!decimal.TryParse(markInfoVM.markup, out decimal markup))
                    throw new ApplicationException("Bad markup");
                markInfoBL.markup = markup;
            }
            if (markInfoVM.P_count != null && markInfoVM.P_count.Length != 0)
            {
                if (!int.TryParse(markInfoVM.P_count, out int productCount))
                    throw new ApplicationException("Bad mark info count");
                markInfoBL.P_count = productCount;
            }

            markInfoBL.Storage_location = markInfoVM.Storage_location;

            return markInfoBL;
        }
    }
}
