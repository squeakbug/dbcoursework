using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class MarkedProductException : ApplicationException
    {
        public MarkedProductException()
        { }
        public MarkedProductException(string message)
            : base(message) { }

        public MarkedProductException(string message, Exception inner)
            : base(message, inner) { }
    }
}
