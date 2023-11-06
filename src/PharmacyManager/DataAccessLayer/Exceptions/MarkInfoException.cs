using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class MarkInfoException : ApplicationException
    {
        public MarkInfoException()
        { }
        public MarkInfoException(string message)
            : base(message) { }

        public MarkInfoException(string message, Exception inner)
            : base(message, inner) { }
    }
}
