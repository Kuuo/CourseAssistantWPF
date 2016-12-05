using System;
using System.Windows;
using System.Windows.Controls;
using CourseAssistantWPF.ViewModel;
using CourseAssistantWPF.Utils;
using System.Data;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for RejoinderInfoUC.xaml
    /// </summary>
    public partial class RejoinderInfoUC : UserControl {

        JudgeListViewModel jlvm;
        ChairmanListViewModel clvm;

        public RejoinderInfoUC() {
            InitializeComponent();
            updateJudgeContext(datagrid1.DataContext as JudgeListViewModel);
            updateChairmanContext(datagrid2.DataContext as ChairmanListViewModel);
        }

        private void updateJudgeContext(JudgeListViewModel j) {
            jlvm = j;
            datagrid1.ItemsSource = jlvm.Table.DefaultView;
        }

        private void updateChairmanContext(ChairmanListViewModel c) {
            clvm = c;
            datagrid2.ItemsSource = clvm.Table.DefaultView;
        }

        private void BtnImportJudeg_Click(object sender, RoutedEventArgs e) {
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
            updateJudgeContext(new JudgeListViewModel(dt));
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导入成功");
        }


        private void BtnGenerateJudge_Click(object sender, RoutedEventArgs e) {
            int flag = cbFor.SelectedIndex + 1;
            try {
                datagrid1.ItemsSource = jlvm.GenerateRandom((int)(NumberOfJ.Value), flag).DefaultView;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }

            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "生成成功");
        }

        private void BtnExportJudeg_Click(object sender, RoutedEventArgs e) {
            var sfd = FileDialogFactory.GetSFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)sfd.ShowDialog()) return;
            switch (sfd.FilterIndex) {
                case 1:
                case 2: ExcelIO.Write(sfd.FileName, jlvm.Table); break;
                case 3: TxtIO.Write(sfd.FileName, jlvm.Table, ','); break;
                default: return;
            }
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导出成功");
        }

        private void BtnGenerateChairman_Click(object sender, RoutedEventArgs e) {
            int flag = cbCFor.SelectedIndex + 1;
            datagrid2.ItemsSource = clvm.GenerateRandom((int)(NumberOfC.Value), flag).DefaultView;
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "生成成功");
        }

        private void BtnExportChairman_Click(object sender, RoutedEventArgs e) {
            var sfd = FileDialogFactory.GetSFD(DataFileInfo.ExcelSAndTxtFilter);
            if (!(bool)sfd.ShowDialog()) return;
            switch (sfd.FilterIndex) {
                case 1:
                case 2: ExcelIO.Write(sfd.FileName, clvm.Table); break;
                case 3: TxtIO.Write(sfd.FileName, clvm.Table, ','); break;
                default: return;
            }
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "导出成功");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            jlvm.Save();
            clvm.Save();
            FadeInOutTextBlockHelper.MakeFadeInOut(greenPromptTB, "保存成功");
        }

        private void NumberOfJ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            tbnoj.Text = (int)NumberOfJ.Value + "";
        }

        private void NumberOfC_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            tbnoc.Text = (int)NumberOfC.Value + "";
        }

    }
}
