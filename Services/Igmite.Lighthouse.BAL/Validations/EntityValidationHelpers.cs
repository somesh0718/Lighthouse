using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Igmite.Lighthouse.BAL.Validations
{
    public static class EntityValidationHelpers
    {
        public static T GetModelValidationErrors<T>(this object obj)
        {
            var validationResults = new List<string>();

            var validations = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);

            Validator.TryValidateObject(obj, context, validations, true);

            foreach (ValidationResult errorItem in validations)
            {
                validationResults.Add(errorItem.ErrorMessage);
            }

            if (validationResults.Count > 0)
                obj.GetType().GetProperty("ErrorMessages").SetValue(obj, validationResults, null);

            return (T)obj;
        }

        public static object GetModelValidationErrors(this object obj)
        {
            var validationResults = new List<string>();

            var validations = new List<ValidationResult>();
            var context = new ValidationContext(obj, null, null);

            Validator.TryValidateObject(obj, context, validations, true);

            foreach (ValidationResult errorItem in validations)
            {
                validationResults.Add(errorItem.ErrorMessage);
            }

            if (validationResults.Count > 0)
                obj.GetType().GetProperty("ErrorMessages").SetValue(obj, validationResults, null);

            return obj;
        }
    }
}
