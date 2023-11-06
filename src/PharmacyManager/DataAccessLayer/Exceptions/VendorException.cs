using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class VendorException : ApplicationException
    {
        public VendorException()
        { }
        public VendorException(string message)
            : base(message) { }

        public VendorException(string message, Exception inner)
            : base(message, inner) { }
    }
}
