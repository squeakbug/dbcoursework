using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class MarkInfoConverter
    {
        public MarkInfo MapToBusinessEntity(Entities.MarkInfo markInfo, Context globalCtx)
        {
            return new MarkInfo()
            {
                Id = markInfo.Id,
                approved_date = markInfo.approved_date,
                Approved_employee_id = markInfo.Approved_employee_id,
                Income_product_id = markInfo.Income_product_id,
                markup = markInfo.markup,
                P_count = markInfo.P_count,
                Storage_location = markInfo.Storage_location
            };
        }

        public Entities.MarkInfo MapFromBusinessEntity(MarkInfo markInfo)
        {
            return new Entities.MarkInfo()
            {
                Id = markInfo.Id,
                approved_date = markInfo.approved_date,
                Approved_employee_id = markInfo.Approved_employee_id,
                Income_product_id = markInfo.Income_product_id,
                markup = markInfo.markup,
                P_count = markInfo.P_count,
                Storage_location = markInfo.Storage_location
            };
        }
    }
}
