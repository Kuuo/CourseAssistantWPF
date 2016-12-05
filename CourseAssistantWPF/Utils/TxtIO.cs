using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace CourseAssistantWPF.Utils {
    public static class TxtIO {
        
        public static DataTable Read(string filepath, char[] separator) {
            var dt = new DataTable();
            var lines = File.ReadAllLines(filepath);
            var header = lines[0].Split(separator);
            for (int i = 0; i < header.Length; i++) {
                dt.Columns.Add(header[i]);
            }

            DataRow r;
            for (int i = 1; i < lines.Length; i++) {
                r = dt.NewRow();
                r.ItemArray = lines[i].Split(separator);
                dt.Rows.Add(r);
            }
            return dt;
        }
        
        public static DataTable Read(string filepath, char separator) {
            return Read(filepath, new char[] { separator });
        }

        public static string[] Write(DataTable dt, char separator) {
            if (dt.Columns.Count == 0) return null;

            var ret = new string[dt.Rows.Count + 1];

            var line = "";
            line = dt.Columns[0].ColumnName;
            for (int i = 1; i < dt.Columns.Count; i++) {
                line += separator.ToString() + dt.Columns[i].ColumnName;
            }
            ret[0] = line;

            for (int i = 0; i < dt.Rows.Count; i++) {
                ret[i + 1] = string.Join(separator.ToString(), dt.Rows[i].ItemArray);
            }

            return ret;
        }
        
        public static void Write(string filepath, DataTable dt, char separator) {
            try {
                File.WriteAllLines(filepath, Write(dt, separator), Encoding.UTF8);
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
    }
}
