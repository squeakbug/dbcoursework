using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class ProductRequestException : ApplicationException
    {
        public ProductRequestException()
        { }
        public ProductRequestException(string message)
            : base(message) { }

        public ProductRequestException(string message, Exception inner)
            : base(message, inner) { }
    }
}
