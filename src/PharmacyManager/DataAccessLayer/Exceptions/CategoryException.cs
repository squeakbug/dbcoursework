using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class CategoryException : ApplicationException
    {
        public CategoryException()
        { }
        public CategoryException(string message)
            : base(message) { }

        public CategoryException(string message, Exception inner)
            : base(message, inner) { }
    }
}
