using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    class PersonMetadataException : ApplicationException
    {
        public PersonMetadataException()
        { }
        public PersonMetadataException(string message)
            : base(message) { }

        public PersonMetadataException(string message, Exception inner)
            : base(message, inner) { }
    }
}
