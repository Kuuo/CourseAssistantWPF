using CourseAssistantWPF.Utils;
using CourseAssistantWPF.ViewModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for StudentInfoUC.xaml
    /// </summary>
    public partial class StudentInfoUC : UserControl {
        StudentListViewModel stulistVM;

        public StudentInfoUC() {
            InitializeComponent();
            stulistVM = grid.DataContext as StudentListViewModel;
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e) {
            string _id = idtb.Text;
            string _name = nametb.Text;
            string _class = classtb.Text;
            var stuIDs = Model.DBHelper.GetStudentsID();
            if (StringUtil.IsAnyOfNullOrEmptyOrWhiteSpace(_id, _name, _class)) {
                dangerPromptTB.Text = "请将信息填写完整";
                return;
            } else if (stuIDs.Contains(_id)) {
                dangerPromptTB.Text = "已存在相同学号的学生";
                return;
            }
            stulistVM.Add(new StudentViewModel(_id, _name, _class));
            dangerPromptTB.Text = "";
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "添加成功");
        }

        private void BtnDeleteStudent_Click(object sender, RoutedEventArgs e) {
            stulistVM.Remove();
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e) {
            var ofd = FileDialogFactory.GetOFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)ofd.ShowDialog()) return;
            DataTable dt = null;
            switch (ofd.FilterIndex) {
                case 1:
                case 2: dt = ExcelIO.Read(ofd.FileName); break;
                case 3: dt = TxtIO.Read(ofd.FileName, ','); break;
                default: return;
            }
            if (dt == null) return;
            updateContext(new StudentListViewModel(dt));
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导入成功");
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = FileDialogFactory.GetSFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)sfd.ShowDialog()) return;
            switch (sfd.FilterIndex) {
                case 1:
                case 2: ExcelIO.Write(sfd.FileName, stulistVM.ToDataTable()); break;
                case 3: TxtIO.Write(sfd.FileName, stulistVM.ToDataTable(), ','); break;
                default: return;
            }
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导出成功");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            stulistVM.Save();
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "保存成功");
        }

        private void updateContext(StudentListViewModel slvm) {
            stulistVM = slvm;
            grid.DataContext = stulistVM;
        }

        private void cbAll_Checked(object sender, RoutedEventArgs e) {
            foreach (var item in stulistVM.StudentList) {
                item.IsSelected = true;
            }
        }

        private void cbAll_Unchecked(object sender, RoutedEventArgs e) {
            foreach (var item in stulistVM.StudentList) {
                item.IsSelected = false;
            }
        }
    }
}
