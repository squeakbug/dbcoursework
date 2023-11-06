using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

using BusinessSpecification;
using BusinessSpecification.Entities;
using DataAccessLayer;
using DefaultBusinessLogic.Exceptions;

namespace DefaultBusinessLogic
{
    public static class Facade
    {
        private static IAuthManager _authManager = null;
        public static IAuthManager AuthManager
        {
            get => _authManager;
        }
        private static IPharmacist _pharmacist = null;
        public static IPharmacist Pharmacist
        {
            get => _pharmacist;
        }
        private static IPharmacyManager _pharmacyManager = null;
        public static IPharmacyManager PharmacyManager
        {
            get => _pharmacyManager;
        }
        private static IProductManager _productManager = null;
        public static IProductManager ProductManager
        {
            get => _productManager;
        }
        private static IStockman _stockman = null;
        public static IStockman Stockman
        {
            get => _stockman;
        }
        private static IRoot _root = null;
        public static IRoot Root
        {
            get => _root;
        }

        static Facade()
        {
            if (ConfigurationManager.AppSettings.Get("AuthDataSource") == null)
                throw new ConfigException("DataSource for AuthManager must be provided");
            if (ConfigurationManager.AppSettings.Get("AuthUser") == null)
                throw new ConfigException("User for AuthManager must be provided");
            if (ConfigurationManager.AppSettings.Get("AuthInitialCatalog") == null)
                throw new ConfigException("InitialCatalog for AuthManager must be provided");
            if (ConfigurationManager.AppSettings.Get("AuthPassword") == null)
                throw new ConfigException("Password for AuthManager must be provided");

            IRepositoryFactory factory = new SqlRepositoryFactory
            (
                dataSource : ConfigurationManager.AppSettings.Get("AuthDataSource"),
                user : ConfigurationManager.AppSettings.Get("AuthUser"),
                initialCatalog : ConfigurationManager.AppSettings.Get("AuthInitialCatalog"),
                passwd : ConfigurationManager.AppSettings.Get("AuthPassword")
            );
            _authManager = new DefaultAuthManager(factory, new DefaultEmployeeFactory());
        }

        public static Employee Login(string login, string passwd)
        {
            try
            {
                return _authManager.Login(login, passwd, out _stockman, out _pharmacist,
                    out _pharmacyManager, out _productManager, out _root);
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public static void Logout()
        {
            _authManager.Logout(ref _stockman, ref _pharmacist,
                    ref _pharmacyManager, ref _productManager, ref _root);
        }
    }
}
