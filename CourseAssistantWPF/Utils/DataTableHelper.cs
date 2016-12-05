using System.Data;

namespace CourseAssistantWPF.Utils {
    public static class DataTableHelper {

        public static bool IsFilled(DataTable dt) {

            for (int i = 0; i < dt.Rows.Count; i++) {
                for (int j = 0; j < dt.Columns.Count; j++) {
                    object o = dt.Rows[i][j];
                    if (o == null || string.IsNullOrWhiteSpace(o as string) || (o as string) == string.Empty)
                        return false;
                }
            }

            return true;
        }

    }
}
