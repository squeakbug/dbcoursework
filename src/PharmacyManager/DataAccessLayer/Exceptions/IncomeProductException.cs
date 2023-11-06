using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class IncomeProductException : ApplicationException
    {
        public IncomeProductException()
        { }
        public IncomeProductException(string message)
            : base(message) { }

        public IncomeProductException(string message, Exception inner)
            : base(message, inner) { }
    }
}
