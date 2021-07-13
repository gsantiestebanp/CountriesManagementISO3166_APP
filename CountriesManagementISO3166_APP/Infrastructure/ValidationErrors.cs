using System.Collections.Generic;

namespace CountriesManagementISO3166_APP.Infrastructure
{
    public static class ValidationErrors
    {
        public static string Unfolds(FluentValidation.Results.ValidationResult validationResult)
        {
            string errorMessage = string.Empty;

            foreach (FluentValidation.Results.ValidationFailure errors in validationResult.Errors)
                if (errors.ErrorMessage.Contains("*"))
                    errorMessage += errors.ErrorMessage + "\n";

            return errorMessage;
        }

        public static string UnfoldsErrors(List<string> Errors)
        {
            string errorMessage = string.Empty;

            foreach (string error in Errors)
                if (error.Contains("*"))
                    errorMessage += error + "\n";

            return errorMessage;
        }       
    }
}
