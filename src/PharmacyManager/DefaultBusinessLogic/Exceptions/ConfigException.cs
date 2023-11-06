using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultBusinessLogic.Exceptions
{
    public class ConfigException : BaseDefaultBLException
    {
        public ConfigException()
        { }
        public ConfigException(string message)
            : base(message) { }

        public ConfigException(string message, Exception inner)
            : base(message, inner) { }
    }
}
