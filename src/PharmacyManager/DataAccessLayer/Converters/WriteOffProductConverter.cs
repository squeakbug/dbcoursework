using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class WriteOffProductConverter
    {
        public WriteOffProduct MapToBusinessEntity(Entities.WriteOffProduct writeOffProduct, Context globalCtx)
        {
            return new WriteOffProduct
            {
                Id = writeOffProduct.Id,
                Mark_info_id = writeOffProduct.Mark_info_id,
                Reason = writeOffProduct.Reason,
                Write_off_date = writeOffProduct.Write_off_date,
                Write_off_employee_id = writeOffProduct.Write_off_employee_id
            };
        }

        public Entities.WriteOffProduct MapFromBusinessEntity(WriteOffProduct writeOffProduct)
        {
            return new Entities.WriteOffProduct
            {
                Id = writeOffProduct.Id,
                Mark_info_id = writeOffProduct.Mark_info_id,
                Reason = writeOffProduct.Reason,
                Write_off_date = writeOffProduct.Write_off_date,
                Write_off_employee_id = writeOffProduct.Write_off_employee_id
            };
        }
    }
}
