using System.Windows;
using System.Windows.Controls;
using CourseAssistantWPF.ViewModel;
using CourseAssistantWPF.Utils;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for RegistrationInfoUC.xaml
    /// </summary>
    public partial class RegistrationInfoUC : UserControl {

        RegistrationListViewModel rlvm;
        int cur = 0, totle, called = 0;

        public RegistrationInfoUC() {
            InitializeComponent();
            rlvm = RGrid.DataContext as RegistrationListViewModel;
            totle = rlvm.Table.Rows.Count;
            updateCount();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            transitioner.SelectedIndex = 0;
        }

        private void updateCount() {
            calledT.Text = called + "";
            uncalledT.Text = (totle - called) + "";
        }

        private void updateCurrentStudentInfo() {
            idtb.Text = rlvm.Table.Rows[cur][0].ToString();
            nametb.Text = rlvm.Table.Rows[cur][1].ToString();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e) {
            rlvm = new RegistrationListViewModel(rlvm.Shuffle());
            cur = 0;
            totle = rlvm.Table.Rows.Count;
            called = 0;
            updateCount();
            updateCurrentStudentInfo();
        }
        
        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = FileDialogFactory.GetSFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)sfd.ShowDialog()) return;
            switch (sfd.FilterIndex) {
                case 1:
                case 2: ExcelIO.Write(sfd.FileName, rlvm.Table); break;
                case 3: TxtIO.Write(sfd.FileName, rlvm.Table, ','); break;
                default: return;
            }
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e) {
            movePrev();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e) {
            moveNext();
        }

        private void movePrev() {
            if (cur <= 0) return;
            cur--;
            updateCurrentStudentInfo();
        }

        private void moveNext() {
            if (cur >= rlvm.Table.Rows.Count - 1) return;
            cur++;
            updateCurrentStudentInfo();
        }

        private void addCount()
        {
            if ((rlvm.Table.Rows[cur][2] as string) == string.Empty ||
                string.IsNullOrEmpty(rlvm.Table.Rows[cur][2] as string))
            {
                called++;
                updateCount();
            }
        }

        private void BtnCase1_Click(object sender, RoutedEventArgs e) {
            addCount();
            rlvm.Table.Rows[cur][2] = "请假";
            moveNext();
        }

        private void BtnCase2_Click(object sender, RoutedEventArgs e) {
            addCount();
            rlvm.Table.Rows[cur][2] = "已到";
            moveNext();
        }

        private void BtnCase3_Click(object sender, RoutedEventArgs e) {
            addCount();
            rlvm.Table.Rows[cur][2] = "未到";
            moveNext();
        }


        private void BtnCase4_Click(object sender, RoutedEventArgs e) {
            addCount();
            rlvm.Table.Rows[cur][2] = "其他";
            moveNext();
        }
        
    }
}
