using System;
using System.Collections.Generic;
using System.Text;

using ViewModel.Entities;
using BusinessSpecification.Entities;

namespace ViewModel.Converters
{
    public static class WriteOffProductConverter
    {
        public static WriteOffProductVM MapToViewModel(WriteOffProduct writeOffProductBL)
        {
            var writeOffProductVM = new WriteOffProductVM();

            writeOffProductVM.Id = writeOffProductBL.Id.ToString();
            writeOffProductVM.Mark_info_id = writeOffProductBL.Mark_info_id.ToString();
            writeOffProductVM.Write_off_date = writeOffProductBL.Write_off_date.ToString();
            writeOffProductVM.Reason = writeOffProductBL.Reason;
            writeOffProductVM.Write_off_employee_id = writeOffProductBL.ToString();

            return writeOffProductVM;
        }
    }
}
