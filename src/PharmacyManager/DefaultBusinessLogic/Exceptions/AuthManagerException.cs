using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;

namespace DefaultBusinessLogic.Exceptions
{
    public class AuthManagerException : BaseDefaultBLException
    {
        public IAuthManager AuthManager { get; set; }
        public AuthManagerException()
        { }
        public AuthManagerException(string message)
            : base(message) { }

        public AuthManagerException(string message, Exception inner)
            : base(message, inner) { }
    }
}
