using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class ProductException : ApplicationException
    {
        public ProductException()
        { }
        public ProductException(string message)
            : base(message) { }

        public ProductException(string message, Exception inner)
            : base(message, inner) { }
    }
}
