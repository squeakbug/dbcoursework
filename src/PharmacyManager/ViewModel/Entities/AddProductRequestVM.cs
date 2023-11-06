using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class AddProductRequestVM
    {
        public int Id { get; set; }
        public string ConsumerFirstName;
        public string ConsumerMiddleName;
        public string ConsumerLastName;
        public string Email;
        public string PhoneNumber;
        public string Pr_count { get; set; }
        public string Approximate_name { get; set; }
    }
}
