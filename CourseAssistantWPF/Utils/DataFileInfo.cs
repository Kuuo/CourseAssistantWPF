using System;

namespace CourseAssistantWPF.Utils {

    public static class DataFileInfo {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        public static readonly string ExcelFilter = "Excel 文件 (97 - 2003)|*.xls";
        public static readonly string ExcelXFilter = "Excel 文件 (2007)|*.xlsx";
        public static readonly string TxtFilter = "文本文件|*.txt";
        public static readonly string ExcelAndTxtFilter = ExcelFilter + "|" + TxtFilter;
        public static readonly string ExcelXAndTxtFilter = ExcelXFilter + "|" + TxtFilter;
        public static readonly string ExcelSAndTxtFilter = ExcelFilter + "|" + ExcelXAndTxtFilter;
        public static readonly string AllFilter = ExcelFilter + "|" + ExcelXFilter + "|" + TxtFilter;
    }

}
