using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultBusinessLogic.Exceptions
{
    public class BaseDefaultBLException : ApplicationException
    {
        public BaseDefaultBLException() { }
        public BaseDefaultBLException(string message)
            : base(message) { }

        public BaseDefaultBLException(string message, Exception inner)
            : base(message, inner) { }
    }
}
