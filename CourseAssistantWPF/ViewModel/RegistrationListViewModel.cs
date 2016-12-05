using CourseAssistantWPF.Model;
using System;
using System.ComponentModel;
using System.Data;

namespace CourseAssistantWPF.ViewModel {
    class RegistrationListViewModel : INotifyPropertyChanged {

        private int _totalCount;
        private int _calledCount;
        private int _notCalledCount;
        private DataTable _table;

        public DataTable Table {
            get { return _table; }
            set {
                _table = value;
                _table.TableName = "点名信息";
                refresh();
            }
        }
        
        public int CalledCount {
            get { return _calledCount; }
            set {
                if (_calledCount == value) return;
                _calledCount = value;
                NotCalledCount = _totalCount - _calledCount;
                OnPropertyChanged("CalledCount");
            }
        }

        public int NotCalledCount {
            get { return _notCalledCount; }
            set {
                if (_notCalledCount == value) return; 
                _notCalledCount = value;
                OnPropertyChanged("NotCalledCount");
            }
        }

        public RegistrationListViewModel() {
            Table = DBHelper.GetRegistationDataTable();
            refresh();
        }

        public RegistrationListViewModel(DataTable dt) {
            Table = dt;
            refresh();
        }

        public DataTable Shuffle() {
            var dt = new DataTable("点名信息");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("情况");
            var rows = DBHelper.GetRegistationDataTable().Select();
            Utils.RandomUtil.Shuffle(rows);
            DataRow newRow;
            foreach (DataRow r in rows) {
                newRow = dt.NewRow();
                newRow.ItemArray = r.ItemArray;
                dt.Rows.Add(newRow);
            }
            Table = dt;
            return dt;
        }

        private void refresh() {
            _totalCount = Table.Rows.Count;
            CalledCount = 0;
            NotCalledCount = _totalCount - _calledCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propname) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
