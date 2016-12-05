using System.ComponentModel;

namespace CourseAssistantWPF.ViewModel {
    public class StudentViewModel : INotifyPropertyChanged {

        private string _id;
        private string _name;
        private string _majorclass;
        private bool _isSelected;
        
        public string ID {
            get { return _id; }
            set {
                if (_id != value) {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string MajorClass {
            get { return _majorclass; }
            set {
                if (_majorclass != value) {
                    _majorclass = value;
                    OnPropertyChanged("MajorClass");
                }
            }
        }

        public bool IsSelected {
            get { return _isSelected; }
            set {
                if (_isSelected != value) {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public StudentViewModel() { }

        public StudentViewModel(Model.Student s) {
            _id = s.ID;
            _name = s.Name;
            _majorclass = s.MajorClass;
            _isSelected = false;
        }

        public StudentViewModel(string i, string n, string m) {
            _id = i;
            _name = n;
            _majorclass = m;
            _isSelected = false;
        }

        public string[] ToArrayInfo() {
            return new string[] { ID, Name, MajorClass };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propname) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
