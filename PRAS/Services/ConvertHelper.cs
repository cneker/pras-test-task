using System.Text;

namespace PRAS.Services
{
    public class ConvertHelper
    {
        public static string ConvertErrorsToMessageString(FluentValidation.Results.ValidationResult result)
        {
            var str = new StringBuilder();
            foreach (var error in result.Errors)
            {
                str.AppendLine(error.ErrorMessage);
            }
            return str.ToString();
        }
    }
}
