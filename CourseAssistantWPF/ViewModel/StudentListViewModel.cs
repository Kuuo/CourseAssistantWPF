using CourseAssistantWPF.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace CourseAssistantWPF.ViewModel {
    class StudentListViewModel : INotifyPropertyChanged {
        ObservableCollection<StudentViewModel> _stulist;
        int _count;

        public ObservableCollection<StudentViewModel> StudentList {
            get { return _stulist; }
            set {
                if (_stulist == value) return;
                _stulist = value;
                Count = _stulist.Count;
                OnPropertyChanged("StudentList");
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
        
        public StudentListViewModel() {
            StudentList = Load();
        }

        public StudentListViewModel(DataTable dt) {
            StudentList = Load(dt);
        }

        public ObservableCollection<StudentViewModel> Load() {
            var col = new ObservableCollection<StudentViewModel>();
            foreach (var s in DBHelper.Context.Students) {
                col.Add(new StudentViewModel(s));
            }
            return col;
        }

        public ObservableCollection<StudentViewModel> Load(DataTable dt) {
            var col = new ObservableCollection<StudentViewModel>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                col.Add(new StudentViewModel(dt.Rows[i][0].ToString(),
                                             dt.Rows[i][1].ToString(),
                                             dt.Rows[i][2].ToString()));
            }
            return col;
        }

        public void Add(StudentViewModel s) {
            StudentList.Insert(0, s);
            DBHelper.Context.Students.Add(new Student() { ID = s.ID, Name = s.Name, MajorClass = s.MajorClass });
            updateCount();
            DBHelper.Update();
        }

        public void Remove() {
            for (int i = StudentList.Count - 1; i > -1; i--) {
                if (StudentList[i].IsSelected) {
                    DBHelper.Context.Students.Remove(DBHelper.Context.Students.ToList().First(s => s.ID == StudentList[i].ID));
                    StudentList.RemoveAt(i);
                }
            }
            updateCount();
            DBHelper.Update();
        }

        private void updateCount() {
            Count = StudentList.Count;
        }

        public void Save() {
            var l = DBHelper.Context.Students.ToList();
            foreach (var s in l) {
                DBHelper.Context.Students.Remove(s);
            }
            foreach (var s in StudentList) {
                DBHelper.Context.Students.Add(new Student() { ID = s.ID, Name = s.Name, MajorClass = s.MajorClass });
            }
            DBHelper.Update();
        }

        public DataTable ToDataTable() {
            var dt = new DataTable("学生信息");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("班级");
            DataRow r;
            for (int i = 0; i < StudentList.Count; i++) {
                r = dt.NewRow();
                r.ItemArray = StudentList[i].ToArrayInfo();
                dt.Rows.Add(r);
            }
            return dt;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propname) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
