using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;

namespace DefaultBusinessLogic.Exceptions
{
    public class PharmacyManagerException : BaseDefaultBLException
    {
        public IPharmacyManager PharmacyManager { get; set; }
        public PharmacyManagerException() { }
        public PharmacyManagerException(string message)
            : base(message) { }

        public PharmacyManagerException(string message, Exception inner)
            : base(message, inner) { }
    }
}
