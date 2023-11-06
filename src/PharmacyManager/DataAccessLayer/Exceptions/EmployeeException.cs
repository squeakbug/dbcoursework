using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class EmployeeException : ApplicationException
    {
        public EmployeeException()
        { }
        public EmployeeException(string message)
            : base(message) { }

        public EmployeeException(string message, Exception inner)
            : base(message, inner) { }
    }
}
