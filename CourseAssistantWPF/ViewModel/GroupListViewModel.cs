using CourseAssistantWPF.Model;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace CourseAssistantWPF.ViewModel {
    public class GroupListViewModel : INotifyPropertyChanged {
        private int _count;
        private DataTable _table;

        public DataTable Table {
            get { return _table; }
            set {
                _table = value.Copy();
                _table.TableName = "分组信息";
                Count = Table.Rows.Count;
            }
        }

        public int Count {
            get { return _count; }
            set {
                if (_count == value) return;
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        public GroupListViewModel() {
            Table = DBHelper.GetGroupsDataTable();
        }

        public GroupListViewModel(DataTable dt) {
            Table = dt;
        }

        public void Save() {
            var groups = DBHelper.Context.HWGroups.ToList();
            foreach (var group in groups) {
                DBHelper.Context.HWGroups.Remove(group);
            }
            for (int i = 0; i < Table.Rows.Count; i++) {
                var group = new HWGroup();
                group.LeaderID = DBHelper.GetIDByName(Table.Rows[i][0].ToString());
                string members = string.Empty;
                for (int j = 1; j < Table.Columns.Count; j++) {
                    string memberName = Table.Rows[i][j].ToString();
                    if (j != 1 && !string.IsNullOrEmpty(memberName)) members += ",";
                    members += memberName;
                }
                group.Members = members;
                DBHelper.Context.HWGroups.Add(group);
            }
            DBHelper.Update();
        }

        public DataTable GenerateRandom(int n) {
            if (n <= 1) return null;

            var stulist = DBHelper.GetStudentsName();
            var dt = new DataTable("分组信息");
            dt.Columns.Add("组长");
            for (int i = 1; i < n; i++) dt.Columns.Add("组员" + i);

            int numOfRows = stulist.Count / n, index;
            if (stulist.Count % n != 0) numOfRows++;
            for (int i = 0; i < numOfRows; i++) {
                dt.Rows.Add(dt.NewRow());
                for (int j = 0; j < n; j++) {
                    if (stulist.Count <= 0) {
                        dt.Rows[i][j] = "";
                        continue;
                    }
                    index = Utils.RandomUtil.Uniform(stulist.Count);
                    dt.Rows[i][j] = stulist[index];
                    stulist.RemoveAt(index);
                }
            }

            Table = dt;
            return dt;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propname) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
