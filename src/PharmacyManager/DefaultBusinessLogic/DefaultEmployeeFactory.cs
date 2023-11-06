using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public class DefaultEmployeeFactory : IEmployeeFactory
    {
        public IPharmacyManager CreatePharmacyManager(IRepositoryFactory factory, Employee employee, IAuthManager authManager)
        {
            return new DefaultPharmacyManager(factory, employee, authManager);
        }
        public IProductManager CreateProductManager(IRepositoryFactory factory, Employee employee)
        {
            return new DefaultProductManager(factory, employee);
        }
        public IPharmacist CreatePharmacist(IRepositoryFactory factory, Employee employee,
            int cashRegisterNumber, int workShift)
        {
            return new DefaultPharmacist(factory, employee, cashRegisterNumber, workShift);
        }
        public IStockman CreateStockman(IRepositoryFactory factory, Employee employee)
        {
            return new DefaultStockman(factory, employee);
        }
        public IRoot CreateRoot(IRepositoryFactory factory, Employee employee, IAuthManager authManager)
        {
            return new DefaultRoot(factory, employee, authManager);
        }
        public IAuthManager CreateAuthManager(IRepositoryFactory factory)
        {
            return new DefaultAuthManager(factory, this);
        }
    }
}
