using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification;
using BusinessSpecification.Entities;

namespace DefaultBusinessLogic
{
    public interface IEmployeeFactory
    {
        IPharmacyManager CreatePharmacyManager(IRepositoryFactory factory, Employee employee, IAuthManager authManager);
        IProductManager CreateProductManager(IRepositoryFactory factory, Employee employee);
        IPharmacist CreatePharmacist(IRepositoryFactory factory, Employee employee,
            int cashRegisterNumber, int workShift);
        IStockman CreateStockman(IRepositoryFactory factory, Employee employee);
        IRoot CreateRoot(IRepositoryFactory factory, Employee employee, IAuthManager authManager);
        IAuthManager CreateAuthManager(IRepositoryFactory factory);
    }
}
