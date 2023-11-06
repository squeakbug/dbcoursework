using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class ManufacturerException : ApplicationException
    {
        public ManufacturerException()
        { }
        public ManufacturerException(string message)
            : base(message) { }

        public ManufacturerException(string message, Exception inner)
            : base(message, inner) { }
    }
}
