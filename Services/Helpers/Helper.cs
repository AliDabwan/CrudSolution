using ServiceContracts.DTOS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class Helper
    {
        internal static void ValidateModel(object model)
        {
            ValidationContext validationContext =
               new ValidationContext(model);
            List<ValidationResult> validationResults =
            new();
            bool isValid = Validator.TryValidateObject(
                model, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults
                    .FirstOrDefault()?.ErrorMessage);
            }


        }
    }
}
