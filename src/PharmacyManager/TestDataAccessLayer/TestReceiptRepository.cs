using System;
using Xunit;
using System.Collections.Generic;

using DataAccessLayer;
using DataAccessLayer.RepositoriesImp;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;

namespace TestDataAccessLayer
{
    public class TestReceiptRepository
    {
        [Fact]
        public void TestReceiptCRUD()
        {
            var factory = new SqlRepositoryFactory
            (
                dataSource: "127.0.0.1,1433",
                initialCatalog: "pharmacy",
                user: "SA",
                passwd: "P@ssword"
            );

            SetupReceiptRepository(factory);

            Receipt tmpReceipt = null;
            using (var rep = factory.CreateReceiptRep())
            {
                tmpReceipt = rep.GetReceiptById(1);
                Assert.NotNull(tmpReceipt);
                Assert.Equal(1, tmpReceipt.Receipt_number);

                var receipts = new List<Receipt>(rep.GetReceipts());
                Assert.Equal(3, receipts.Count);
                tmpReceipt = rep.GetReceiptById(1);
            }

            using (var rep = factory.CreateReceiptRep())
            {
                tmpReceipt.Receipt_number = 4;
                rep.Update(tmpReceipt);

                tmpReceipt = rep.GetReceiptById(1);
                Assert.Equal(4, tmpReceipt.Receipt_number);

                rep.Delete(tmpReceipt.Id);
                var receipts = new List<Receipt>(rep.GetReceipts());
                Assert.Equal(2, receipts.Count);
            }

            ReleaseReceiptRepository(factory);
        }

        public static void SetupReceiptRepository(SqlRepositoryFactory factory)
        {
            TestEmployeeRepository.SetupEmployeeRepository(factory);

            List<Receipt> receiptSample = GetReceiptSample(factory);
            using (var rep = factory.CreateReceiptRep())
            {
                rep.Create(receiptSample[0]);
                rep.Truncate();
            }

            using (var rep = factory.CreateReceiptRep())
            {
                rep.Create(receiptSample[0]);
                rep.Create(receiptSample[1]);
                rep.Create(receiptSample[2]);
            }
        }

        public static void ReleaseReceiptRepository(SqlRepositoryFactory factory)
        {
            using (var rep = factory.CreateReceiptRep())
                rep.Truncate();

            TestEmployeeRepository.ReleaseEmployeeRepository(factory);
        }
        public static List<Receipt> GetReceiptSample(SqlRepositoryFactory factory)
        {
            List<Employee> employees = null;
            using (var rep = factory.CreateEmployeeRep())
                employees = new List<Employee>(rep.GetEmployees());

            return new List<Receipt>
            {
                new Receipt
                {
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[0], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 1, Work_shift = 1
                },
                new Receipt
                {
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[1], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 2, Work_shift = 1
                },
                new Receipt
                {
                    Card_size = 100, Cash_size = 100, Cash_register_number = 1, Discount = 0,
                    Employee = employees[2], Leave_date = DateTime.Now, Payment_type = "card/cash",
                    Receipt_number = 3, Work_shift = 1
                }
            };
        }
    }
}
