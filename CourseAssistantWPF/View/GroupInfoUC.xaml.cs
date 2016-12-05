using CourseAssistantWPF.Utils;
using CourseAssistantWPF.ViewModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for GroupInfoUC.xaml
    /// </summary>
    public partial class GroupInfoUC : UserControl {
        GroupListViewModel glvm;

        public GroupInfoUC() {
            InitializeComponent();
            glvm = GIUCGrid.DataContext as GroupListViewModel;
            datagrid.ItemsSource = glvm.Table.DefaultView;
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
            updateContext(new GroupListViewModel(dt));
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导入成功");
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = FileDialogFactory.GetSFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)sfd.ShowDialog()) return;
            switch (sfd.FilterIndex) {
                case 1:
                case 2: ExcelIO.Write(sfd.FileName, glvm.Table); break;
                case 3: TxtIO.Write(sfd.FileName, glvm.Table, ','); break;
                default: return;
            }
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导出成功");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            glvm.Save();
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "保存成功");
        }

        private void BtnGen_Click(object sender, RoutedEventArgs e) {
            datagrid.ItemsSource = glvm.GenerateRandom((int)(NumberOfS.Value)).DefaultView;
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "生成成功");
        }

        private void updateContext(GroupListViewModel g) {
            if (g == null) return;
            glvm = g;
            GIUCGrid.DataContext = g;
            datagrid.ItemsSource = g.Table.DefaultView;
        }


        private void NumberOfS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            nofsT.Text = (int)(NumberOfS.Value) + "";
        }
    }
}
