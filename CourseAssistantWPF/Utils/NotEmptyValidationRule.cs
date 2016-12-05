using System.Globalization;
using System.Windows.Controls;

namespace CourseAssistantWPF.Utils {

    public class NotEmptyValidationRule : ValidationRule {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            return string.IsNullOrWhiteSpace((value ?? "").ToString()) ? 
                   new ValidationResult(false, "此项不可为空") : ValidationResult.ValidResult;
        }

    }
}