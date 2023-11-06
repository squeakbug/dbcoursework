using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;

namespace DefaultBusinessLogic.Exceptions
{
    public class ProductManagerException : BaseDefaultBLException
    {
        public IProductManager ProductManager { get; set; }
        public ProductManagerException() { }
        public ProductManagerException(string message)
            : base(message) { }

        public ProductManagerException(string message, Exception inner)
            : base(message, inner) { }
    }
}
