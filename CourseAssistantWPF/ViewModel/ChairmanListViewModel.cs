using CourseAssistantWPF.Model;
using System.Data;
using System.Linq;

namespace CourseAssistantWPF.ViewModel {
    class ChairmanListViewModel {

        private DataTable _table;
        public DataTable Table {
            get { return _table; }
            set {
                _table = value.Copy();
                _table.TableName = "主席信息";
            }
        }

        public ChairmanListViewModel() {
            Table = DBHelper.GetChairmansDataTable();
        }

        public ChairmanListViewModel(DataTable dt) {
            Table = dt;
        }

        public void Save() {
            var chairmen = DBHelper.Context.Chairmen.ToList();
            foreach (var chairman in chairmen) {
                DBHelper.Context.Chairmen.Remove(chairman);
            }
            for (int i = 0; i < Table.Rows.Count; i++) {
                var chairman = new Chairman();
                chairman.CID = Table.Rows[i][0].ToString();
                DBHelper.Context.Chairmen.Add(chairman);
            }
            DBHelper.Update();
        }

        public DataTable GenerateRandom(int n, int flag) {
            if (n < 1) return null;

            var names = flag == 1 ? DBHelper.GetStudentsName() : DBHelper.GetGroupsLeaderName();
            var idlist = DBHelper.GetStudentsID();
            var dt = new DataTable("答辩主席");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");

            for (int i = 0; i < n; i++) {
                dt.Rows.Add(dt.NewRow());
                int index = Utils.RandomUtil.Uniform(names.Count);
                dt.Rows[i][0] = idlist[index];
                dt.Rows[i][1] = names[index];
                idlist.RemoveAt(index);
                names.RemoveAt(index);
            }

            Table = dt;
            return dt;
        }
    }
}
