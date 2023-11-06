using System;
using Xunit;
using Autofac;
using Autofac.Extras.Moq;
using System.Collections.Generic;

using DefaultBusinessLogic;
using BusinessSpecification;
using BusinessSpecification.Entities;
using BusinessSpecification.RepositoryInterfaces;

namespace TestBusinessLogic
{
    public class DefaultAuthManagerTest
    {
        //[Fact]
        public void TestInitStockman()
        {
            /*
            using (var mock = AutoMock.GetLoose())
            {
                var empl = GetEmployeeSample()[0];

                var factoryRep = new SqlRepositoryFactory();
                var factoryEmp = new DefaultEmployeeFactory();
                mock.Mock<DefaultAuthManager>()
                    .Setup(x => x.GetInitStockman(empl.e_login, empl.p_hash, empl.id))
                    .Returns(new DefaultStockman(factoryRep, empl.id){  });
                var cls = mock.Create<DefaultAuthManager>(new PositionalParameter(0, factoryRep),
                    new PositionalParameter(1, factoryEmp));
                IStockman expected = new DefaultStockman(factoryRep, empl.id);

                IStockman actual = null;
                cls.InitEmployee(out actual, out IPharmacist pharmacist, out IPharmacyManager pharmacyManager,
                    out IProductManager productManager, empl);
            }
            */
        }
    }
}
