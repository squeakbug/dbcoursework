using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class WriteOffProductVM
    {
        public string Id { get; set; }
        public string Mark_info_id { get; set; }
        public string Write_off_employee_id { get; set; }
        public string Write_off_date { get; set; }
        public string Reason { get; set; }
    }
}
