
using System;

namespace CourseAssistantWPF.Utils {
    public static class StringUtil {

        public static bool IsAnyOfNullOrEmptyOrWhiteSpace(params string[] s) {
            if (s == null) return true;
            foreach (var item in s) {
                if (item.Equals(string.Empty) || string.IsNullOrWhiteSpace(item))
                    return true;
            }
            return false;
        }

        public static string FullDateTimeString(DateTime dt) => 
            $"{dt.Year}{dt.Month}{dt.Day}{dt.Hour}{dt.Minute}{dt.Second}";
        
    }
}
