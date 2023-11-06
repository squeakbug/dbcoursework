using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultBusinessLogic.Exceptions
{
    public class RootException : BaseDefaultBLException
    {
        public IRoot Root { get; set; }
        public RootException()
        { }
        public RootException(string message)
            : base(message) { }

        public RootException(string message, Exception inner)
            : base(message, inner) { }
    }
}
