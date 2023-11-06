using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class InvoiceException : ApplicationException
    {
        public InvoiceException()
        { }
        public InvoiceException(string message)
            : base(message) { }

        public InvoiceException(string message, Exception inner)
            : base(message, inner) { }
    }
}
