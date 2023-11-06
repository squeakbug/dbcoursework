using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class WriteOffProductException : ApplicationException
    {
        public WriteOffProductException()
        { }
        public WriteOffProductException(string message)
            : base(message) { }

        public WriteOffProductException(string message, Exception inner)
            : base(message, inner) { }
    }
}
