using CourseAssistantWPF.Model;
using CourseAssistantWPF.Utils;
using System.Data;
using System.Linq;

namespace CourseAssistantWPF.ViewModel {
    class JudgeListViewModel {

        private DataTable _table;
        public DataTable Table {
            get { return _table; }
            set {
                _table = value.Copy();
                _table.TableName = "评委信息";
            }
        }

        public JudgeListViewModel() {
            Table = DBHelper.GetJudgesDataTable();
        }

        public JudgeListViewModel(DataTable dt) {
            Table = dt;
        }

        public void Save() {
            var rejList = DBHelper.Context.Rejoinders.ToList();
            foreach (var group in rejList) {
                DBHelper.Context.Rejoinders.Remove(group);
            }
            for (int i = 0; i < Table.Rows.Count; i++) {
                var rejinfo = new Rejoinder();
                rejinfo.StuID = DBHelper.GetIDByName(Table.Rows[i][0].ToString());
                string members = string.Empty;
                for (int j = 1; j < Table.Columns.Count; j++) {
                    string memberName = Table.Rows[i][j].ToString();
                    if (j != 1 && !string.IsNullOrEmpty(memberName)) members += ",";
                    members += memberName;
                }
                rejinfo.Judges = members;
                DBHelper.Context.Rejoinders.Add(rejinfo);
            }
            DBHelper.Update();
        }
        
        public DataTable GenerateRandom(int n, int flag) {
            if (n <= 0) return null;

            var names = flag == 1 ? DBHelper.GetStudentsName() : DBHelper.GetGroupsLeaderName();
            int numOfStu = names.Count;
            var dt = new DataTable("评委信息");
            dt.Columns.Add("答辩人");
            for (int i = 1; i <= n; i++) dt.Columns.Add("评委" + i);
            for (int i = 0; i < numOfStu; i++) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = names[i];
            }

            bool finished = false;
            while (!finished) {
                for (int i = 1; i <= n; i++) {
                    for (int j = 0; j < numOfStu; j++) {
                        names = flag == 1 ? DBHelper.GetStudentsName() : DBHelper.GetGroupsLeaderName();
                        for (int r = 0; r < i; r++) names.Remove(dt.Rows[j][r] as string);
                        for (int r = 0; r < j; r++) names.Remove(dt.Rows[r][i] as string);
                        if (names.Count <= 0) break;
                        int index = RandomUtil.Uniform(names.Count);
                        dt.Rows[j][i] = names[index] as string;
                    }
                }
                finished = DataTableHelper.IsFilled(dt);
            }
            
            Table = dt;
            return dt;
        }
    }
}
