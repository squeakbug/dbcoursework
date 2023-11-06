using System;
using System.Collections.Generic;
using System.Text;

using DefaultBusinessLogic;

namespace DefaultBusinessLogic.Exceptions
{
    public class StockmanException : BaseDefaultBLException
    {
        public IStockman Stockman { get; set; }
        public StockmanException() { }
        public StockmanException(string message)
            : base(message) { }
        public StockmanException(string message, Exception inner)
            : base(message, inner) { }
    }
}
