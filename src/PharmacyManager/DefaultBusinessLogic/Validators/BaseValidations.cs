using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DefaultBusinessLogic.Validators
{
    public static class BaseValidations
    {
        public static bool IsValidMobileNumber(string inputMobileNumber)
        {
            // Формат              Регулярное выражение
            // xxxxxxxxxx          ^[0 - 9]{ 10}$
            // +xx xx xxxxxxxx     ^\+[0 - 9]{ 2}\s +[0 - 9]{ 2}\s +[0 - 9]{ 8}$
            // xxx - xxxx - xxxx   ^[0 - 9]{ 3} -[0 - 9]{ 4}-[0 - 9]{ 4}$
            string strRegex = @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]
                {2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)";

            Regex re = new Regex(strRegex);
            return re.IsMatch(inputMobileNumber);
        }
    }
}
