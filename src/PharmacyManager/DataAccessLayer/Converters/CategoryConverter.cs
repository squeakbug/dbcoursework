using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;
using DataAccessLayer;

namespace DataAccessLayer.Converters
{
    internal class CategoryConverter
    {
        public Category MapToBusinessEntity(Entities.Category category)
        {
            return new Category()
            {
                Id = category.Id,
                C_description = category.C_description,
                Parent_category_id = category.Parent_category_id,
                Title = category.Title
            };
        }

        public Entities.Category MapFromBusinessEntity(Category category)
        {
            return new Entities.Category()
            {
                Id = category.Id,
                C_description = category.C_description,
                Parent_category_id = category.Parent_category_id,
                Title = category.Title
            };
        }
    }
}
