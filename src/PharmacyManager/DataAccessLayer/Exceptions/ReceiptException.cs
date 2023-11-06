using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class ReceiptException : ApplicationException
    {
        public ReceiptException()
        { }
        public ReceiptException(string message)
            : base(message) { }

        public ReceiptException(string message, Exception inner)
            : base(message, inner) { }
    }
}
