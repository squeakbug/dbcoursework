using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    public class DefaultPharmacist : BaseEmployee, IPharmacist
    {
        private Receipt _receipt = null;
        private List<(int, uint)> _productList = new List<(int, uint)>();

        private int _cashRegisterNumber;
        private int _workShift;
        private int _receiptNumber = 0;

        public DefaultPharmacist(IRepositoryFactory factory, Employee employee, int cashRegisterNumber, int workShift)
            : base(factory, employee)
        {
            _cashRegisterNumber = cashRegisterNumber;
            _workShift = workShift;
        }

        public void InitOperation()
        {
            _receipt = new Receipt
            {
                Cash_register_number = _cashRegisterNumber,
                Employee_id = Employee.id,
                Work_shift = _workShift,
                Discount = 0
            };
        }

        public void AddProductToOperation(int productid, uint cnt)
        {
            _productList.Add((productid, cnt));
        }

        public void DeleteProductFromOperation(int productId)
        {
            _productList.Remove(_productList.Find(x => x.Item1 == productId));
        }

        public void SetDiscount(Decimal discount)
        {
            if (_receipt != null)
                _receipt.Discount = discount;
            else
                throw new PharmacistException("No initialized receipt") { Pharmacist = this };
        }

        public void ConfirmOperation(Decimal cardSize, Decimal cashSize, string paymentType)
        {
            if (_receipt == null)
                throw new PharmacistException("No initialized receipt") { Pharmacist = this };
            Decimal totalPrice = 0;
            using (var productRep = RepositoryFactory.CreateMarkedProductRep())
            {
                totalPrice = productRep.GetMarkedProductTotalPrice(_productList);
            }
            if (cardSize < 0 || cashSize < 0)
                throw new PharmacistException("Cash and card size must be positive");
            if (totalPrice * (1 - _receipt.Discount) > cardSize + cashSize)
                throw new PharmacistException("No enough money to confirm buy operation") { Pharmacist = this };
            if (paymentType != "card" && paymentType != "cash" && paymentType != "cash/card")
                throw new PharmacistException("Payment type must be card or cash") { Pharmacist = this };
            _receipt.Card_size = cardSize;
            _receipt.Cash_size = cashSize;
            _receipt.Leave_date = DateTime.Now;
            _receipt.Payment_type = paymentType;
            _receipt.Receipt_number = _receiptNumber++;
            using (var receiptRep = RepositoryFactory.CreateReceiptRep())
            {
                receiptRep.AddReceipWithProducts(_receipt, _productList);
            }
            _receipt = null;
            _productList.Clear();
        }

        public void CancelOperation()
        {
            _receipt = null;
            _productList.Clear();
        }

        public void ConfirmProductRequest(ProductRequest request, PersonMetadata personMetadata)
        {
            if (request.Pr_count <= 0)
                throw new PharmacistException("Count in product request must be positive") { Pharmacist = this };
            if (request.Product_id != null)
            {
                using (var rep = RepositoryFactory.CreateProductRep())
                {
                    if (rep.GetProductById(request.Product_id.Value) == null)
                        throw new PharmacistException("No product with such id in database") { Pharmacist = this };
                }
            }
            else
            {
                if (request.Approximate_name == null || request.Approximate_name.Length == 0)
                    throw new PharmacistException("You must add product approximate name");
            }
            request.Request_Date = DateTime.Now;
            request.approved = 0;
            request.Request_employee_id = Employee.id;
            using (var productReqRep = RepositoryFactory.CreateProductRequestRep())
            {
                productReqRep.CreateRequestWithPerson(personMetadata, request);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProducts();
            }
        }
        public IEnumerable<MarkedProduct> GetStoredProducts()
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProducts();
            }
        }

        public Manufacturer GetManufacturerById(int id)
        {
            using (var manufacturerRep = RepositoryFactory.CreateManufacturerRep())
            {
                return manufacturerRep.GetManufacturerById(id);
            }
        }

        public IEnumerable<MarkInfo> GetMarkInfos()
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfos();
            }
        }

        public MarkedProduct GetMarkedProductById(int id)
        {
            using (var markedProductRep = RepositoryFactory.CreateMarkedProductRep())
            {
                return markedProductRep.GetMarkedProductById(id);
            }
        }

        public MarkInfo GetMarkInfoById(int id)
        {
            using (var markInfoRep = RepositoryFactory.CreateMarkInfoRep())
            {
                return markInfoRep.GetMarkInfoById(id);
            }
        }

        public IncomeProduct GetIncomeProductById(int id)
        {
            using (var incomeProductRep = RepositoryFactory.CreateIncomeProductRep())
            {
                return incomeProductRep.GetIncomeProductById(id);
            }
        }

        public Product GetProductById(int id)
        {
            using (var productRep = RepositoryFactory.CreateProductRep())
            {
                return productRep.GetProductById(id);
            }
        }
    }
}
