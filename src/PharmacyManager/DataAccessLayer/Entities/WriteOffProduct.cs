using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class WriteOffProduct
    {
        public int Id { get; set; }
        public int Mark_info_id { get; set; }
        public int Write_off_employee_id { get; set; }
        public DateTime Write_off_date { get; set; }
        public string Reason { get; set; }
    }
}
