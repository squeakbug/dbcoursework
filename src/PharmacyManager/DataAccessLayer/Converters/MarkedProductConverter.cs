using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class MarkedProductConverter
    {
        public MarkedProduct MapToBusinessEntity(Entities.MarkedProduct markedProduct, Context globalCtx)
        {
            var markInfoRep = new SqlMarkInfoRepository(globalCtx);
            return new MarkedProduct()
            {
                Id = markedProduct.Id,
                Mark_info_id = markedProduct.Mark_info_id,
                Sold = markedProduct.Sold
            };
        }

        public Entities.MarkedProduct MapFromBusinessEntity(MarkedProduct markedProduct)
        {
            return new Entities.MarkedProduct()
            {
                Id = markedProduct.Id,
                Mark_info_id = markedProduct.Mark_info_id,
                Sold = markedProduct.Sold
            };
        }
    }
}
