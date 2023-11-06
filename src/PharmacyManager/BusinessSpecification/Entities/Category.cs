using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessSpecification.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Parent_category_id { get; set; }
        public string C_description { get; set; }
    }
}
