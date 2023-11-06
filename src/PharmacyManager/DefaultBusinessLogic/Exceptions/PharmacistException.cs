using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;

namespace DefaultBusinessLogic.Exceptions
{
    public class PharmacistException : BaseDefaultBLException
    {
        public IPharmacist Pharmacist { get; set; }
        public PharmacistException() { }
        public PharmacistException(string message)
            : base(message) { }
        public PharmacistException(string message, Exception inner)
            : base(message, inner) { }
    }
}
