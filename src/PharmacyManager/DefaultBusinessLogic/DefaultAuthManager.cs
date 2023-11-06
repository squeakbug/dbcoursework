using System;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;

using DataAccessLayer;
using BusinessSpecification;
using BusinessSpecification.RepositoryInterfaces;
using BusinessSpecification.Entities;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    public class DefaultAuthManager : IAuthManager
    {
        private IEmployeeFactory _employeeFactory = null;
        private IRepositoryFactory _repositoryFactory = null;
        private Employee _authedEmployee;

        public DefaultAuthManager(IRepositoryFactory repositoryFactory, IEmployeeFactory employeeFactory)
        {
            _repositoryFactory = repositoryFactory;
            _employeeFactory = employeeFactory;
        }

        public Employee Login(string login, string passwd, out IStockman stockman, out IPharmacist pharmacist,
            out IPharmacyManager pharmacyManager, out IProductManager productManager, out IRoot root)
        {
            Employee empl = null;
            stockman = null;
            pharmacist = null;
            pharmacyManager = null;
            productManager = null;
            root = null;
            using (var employeeRepository = _repositoryFactory.CreateEmployeeRep())
            {
                if (login == ConfigurationManager.AppSettings.Get("RootUser")
                    && passwd == ConfigurationManager.AppSettings.Get("RootPassword"))
                {
                    empl = employeeRepository.GetEmployeeByLogPassword(login, passwd);
                }
                else
                {
                    string hashPasswd = HashPassword(passwd);
                    empl = employeeRepository.GetEmployeeByLogPassword(login, hashPasswd);
                }
                if (empl == null)
                    return null;
            }

            _authedEmployee = empl;
            InitEmployee(out stockman, out pharmacist, out pharmacyManager, out productManager, out root, empl);
            return empl;
        }

        public void Logout(ref IStockman stockman, ref IPharmacist pharmacist,
            ref IPharmacyManager pharmacyManager, ref IProductManager productManager, ref IRoot root)
        {
            stockman = null;
            pharmacist = null;
            pharmacyManager = null;
            productManager = null;
            root = null;
        }

        public string HashPassword(string passwd)
        {
            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString());
                }
            }
            return builder.ToString();
        }

        public Employee GetAuthedEmployee()
        {
            return _authedEmployee;
        }

        private void InitEmployee(out IStockman stockman, out IPharmacist pharmacist,
            out IPharmacyManager pharmacyManager, out IProductManager productManager, out IRoot root, Employee employee)
        {
            stockman = null;
            pharmacist = null;
            pharmacyManager = null;
            productManager = null;
            root = null;
            switch (employee.appointment)
            {
                case "stockman":
                    stockman = GetInitStockman(employee.e_login, employee.p_hash, employee);
                    break;
                case "pharmacist":
                    pharmacist = GetInitPharmacist(employee.e_login, employee.p_hash,
                        employee, GetRegisterNumber(), GetCurrentWorkShift());
                    break;
                case "pharmacy_manager":
                    pharmacyManager = GetInitPharmacyManager(employee.e_login, employee.p_hash, employee);
                    break;
                case "product_manager":
                    productManager = GetInitProductManager(employee.e_login, employee.p_hash, employee);
                    break;
                case "root":
                    root = GetInitRoot(employee.e_login, employee.p_hash, employee);
                    break;
                default:
                    throw new AuthManagerException("Not supported appointment") { AuthManager = this };
            }
        }

        private string[] getAppointments()
        {
            string[] allowedAppointments = { "stockman", "pharmacist", "pharmacy_manager", "product_manager" };
            return allowedAppointments;
        }

        public IEnumerable<string> GetAppointments()
        {
            return getAppointments();
        }

        public bool IsAppointmentValid(string appointment)
        {
            string[] allowedAppointments = getAppointments();
            if (Array.IndexOf(allowedAppointments, appointment) == -1)
                return false;
            else
                return true;
        }

        public IRoot GetInitRoot(string login, string passwd, Employee employee)
        {
            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("RootDataSource"),
                initialCatalog : ConfigurationManager.AppSettings.Get("RootInitialCatalog"),
                user : login,
                passwd : passwd
            );
            IRoot res = _employeeFactory.CreateRoot(factory, employee, this);
            return res;
        }

        public IStockman GetInitStockman(string login, string passwd, Employee employee)
        {
            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("StockmanDataSource"),
                initialCatalog : ConfigurationManager.AppSettings.Get("StockmanInitialCatalog"),
                user : login,
                passwd : passwd
            );
            IStockman res = _employeeFactory.CreateStockman(factory, employee);
            return res;
        }

        private IPharmacist GetInitPharmacist(string login, string passwd, Employee employee, int registerNumber, int workShift)
        {
            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("PharmacistDataSource"),
                initialCatalog : ConfigurationManager.AppSettings.Get("PharmacistInitialCatalog"),
                user : login,
                passwd : passwd
            );
            IPharmacist res = _employeeFactory.CreatePharmacist(factory, employee, registerNumber, workShift);
            return res;
        }

        private IPharmacyManager GetInitPharmacyManager(string login, string passwd, Employee employee)
        {
            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("PharmacyManagerDataSource"),
                initialCatalog : ConfigurationManager.AppSettings.Get("PharmacyManagerInitialCatalog"),
                user : login,
                passwd : passwd
            );
            IPharmacyManager res = _employeeFactory.CreatePharmacyManager(factory, employee, this);
            return res;
        }

        private IProductManager GetInitProductManager(string login, string passwd, Employee employee)
        {
            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("ProductManagerDataSource"),
                initialCatalog : ConfigurationManager.AppSettings.Get("ProductManagerInitialCatalog"),
                user : login,
                passwd : passwd
            );
            IProductManager res = _employeeFactory.CreateProductManager(factory, employee);
            return res;
        }

        public Employee GetEmployeeById(int id)
        {
            using (var employeeRep = _repositoryFactory.CreateEmployeeRep())
            {
                return employeeRep.GetEmployeeById(id);
            }
        }

        public PersonMetadata GetPersonMetadataById(int id)
        {
            using (var metadataRep = _repositoryFactory.CreatePersonMetadataRep())
            {
                return metadataRep.GetPersonMetadataById(id);
            }
        }

        private int GetCurrentWorkShift()
        {
            return 0;
            throw new NotImplementedException("Should be provided by environment");
        }

        private int GetRegisterNumber()
        {
            return 0;
            throw new NotImplementedException("Should be provided by environment");
        }
    }
}
