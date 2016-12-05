using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CourseAssistantWPF.Model {
    public static class DBHelper {

        public static CourseInfoDBEntities Context { get; private set; }
        
        static DBHelper() { Init(); }

        public static DataTable GetRegistationDataTable() {
            var dt = new DataTable("点名信息");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("情况");
            foreach (var item in Context.Students) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1][0] = item.ID;
                dt.Rows[dt.Rows.Count - 1][1] = item.Name;
            }
            return dt;
        }
        public static DataTable GetChairmansDataTable() {
            var dt = new DataTable("主席信息");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");
            var list = from c in Context.Chairmen
                       from s in Context.Students
                       where c.CID == s.ID
                       select new { id = s.ID, name = s.Name };
            foreach (var item in list) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1][0] = item.id;
                dt.Rows[dt.Rows.Count - 1][1] = item.name;
            }
            return dt;
        }
        public static DataTable GetGroupsDataTable() {
            var items = Context.HWGroups.ToList();
            var dt = new DataTable("分组信息");
            dt.Columns.Add("组长");
            int memberCnt = 0;
            var members = new List<string[]>();
            foreach (var group in items) {
                members.Add(group.Members.Split(new char[] { ',' }));
                var length = members[members.Count - 1].Length;
                if (length > memberCnt) memberCnt = length;
            }
            for (int i = 0; i < memberCnt; i++) dt.Columns.Add("组员" + (i + 1));
            for (int i = 0; i < items.Count; i++) {
                dt.Rows.Add(dt.NewRow());
                string stuName = GetNameByID(items[i].LeaderID);
                dt.Rows[i][0] = stuName;
                for (int j = 0; j < members[i].Length; j++) {
                    dt.Rows[i][j + 1] = members[i][j];
                }
            }
            return dt;
        }
        public static DataTable GetJudgesDataTable() {
            var items = Context.Rejoinders.ToList();
            var dt = new DataTable("评委信息");
            dt.Columns.Add("答辩人");
            int memberCnt = 0;
            var members = new List<string[]>();
            foreach (var rejoinderinfo in items) {
                members.Add(rejoinderinfo.Judges.Split(new char[] { ',' }));
                var length = members[members.Count - 1].Length;
                if (length > memberCnt) memberCnt = length;
            }
            for (int i = 0; i < memberCnt; i++) dt.Columns.Add("评委" + (i + 1));
            for (int i = 0; i < items.Count; i++) {
                dt.Rows.Add(dt.NewRow());
                string stuName = GetNameByID(items[i].StuID);
                dt.Rows[i][0] = stuName;
                for (int j = 0; j < members[i].Length; j++) {
                    dt.Rows[i][j + 1] = members[i][j];
                }
            }
            return dt;
        }
        
        public static List<string> GetStudentsName() {
            var list = new List<string>();
            foreach (var s in Context.Students) {
                list.Add(s.Name);
            }
            return list;
        }
        public static List<string> GetStudentsID() {
            var list = new List<string>();
            foreach (var s in Context.Students) {
                list.Add(s.ID);
            }
            return list;
        }
        public static List<string> GetGroupsLeaderName() {
            var list = new List<string>();
            foreach (var group in Context.HWGroups) {
                list.Add(GetNameByID(group.LeaderID));
            }
            return list;
        }

        public static string GetNameByID(string id) {
            foreach (var s in Context.Students) {
                if (s.ID == id) return s.Name;
            }
            return string.Empty;
        }
        public static string GetIDByName(string name) {
            foreach (var s in Context.Students) {
                if (s.Name == name) return s.ID;
            }
            return string.Empty;
        }

        private static void Init() => Context = new CourseInfoDBEntities();

        public static void Update() {
            Context.SaveChanges();
            Context?.Dispose();
            Init();
        }
    }
}
