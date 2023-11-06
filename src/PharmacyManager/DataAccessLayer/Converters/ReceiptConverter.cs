using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessSpecification.Entities;
using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;

namespace DataAccessLayer.Converters
{
    internal class ReceiptConverter
    {
        public Receipt MapToBusinessEntity(Entities.Receipt receipt, Context globalCtx)
        {
            return new Receipt
            {
                Id = receipt.Id,
                Card_size = receipt.Card_size,
                Cash_register_number = receipt.Cash_register_number,
                Cash_size = receipt.Cash_size,
                Discount = receipt.Discount,
                Employee_id = receipt.Employee_id,
                Leave_date = receipt.Leave_date,
                Payment_type = receipt.Payment_type,
                Receipt_number = receipt.Receipt_number,
                Work_shift = receipt.Work_shift
            };
        }

        public Entities.Receipt MapFromBusinessEntity(Receipt receipt)
        {
            return new Entities.Receipt
            {
                Id = receipt.Id,
                Card_size = receipt.Card_size,
                Cash_register_number = receipt.Cash_register_number,
                Cash_size = receipt.Cash_size,
                Discount = receipt.Discount,
                Employee_id = receipt.Employee_id,
                Leave_date = receipt.Leave_date,
                Payment_type = receipt.Payment_type,
                Receipt_number = receipt.Receipt_number,
                Work_shift = receipt.Work_shift
            };
        }
    }
}
