using System;
using System.Collections.Generic;
using System.Text;

using BusinessSpecification.Entities;

namespace DefaultBusinessLogic.Validators
{
    public static class PersonMetadataValidator
    {
        public static void Validate(PersonMetadata metadata)
        {
            if (!BaseValidations.IsValidMobileNumber(metadata.Phone))
                throw new ApplicationException("Bad phone number");
            if (metadata.First_name == null || metadata.First_name.Length < 2)
                throw new ApplicationException("Bad first name");
            if (metadata.Middle_name == null || metadata.Middle_name.Length < 2)
                throw new ApplicationException("Bad middle name");
            if (metadata.Last_name == null || metadata.Last_name.Length < 2)
                throw new ApplicationException("Bad last name");
        }
    }
}
