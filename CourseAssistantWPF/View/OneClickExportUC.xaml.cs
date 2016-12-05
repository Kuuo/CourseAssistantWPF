using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using CourseAssistantWPF.ViewModel;
using CourseAssistantWPF.Utils;
using System.Diagnostics;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for OnClickExportUC.xaml
    /// </summary>
    public partial class OneClickExportUC : UserControl {
        public OneClickExportUC() {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            transitioner.SelectedIndex = 0;
        }

        private void BtnEx_Click(object sender, RoutedEventArgs e) {
            var filepath = DataFileInfo.DesktopPath + "\\CourseInfo" + StringUtil.FullDateTimeString(DateTime.Now) +
                           ((bool)(rbx.IsChecked) ? ".xlsx" : ".xls");

            var ds = new DataSet("CourseInfo");
            ds.Tables.Add((new StudentListViewModel()).ToDataTable());
            if ((bool)(tbg1.IsChecked)) {
                ds.Tables.Add((bool)(tbg2.IsChecked) ?
                              (new GroupListViewModel()).GenerateRandom(int.Parse(gcb.Text)) :
                              (new GroupListViewModel()).Table.Copy());
            }
            if ((bool)(tbj1.IsChecked)) {
                ds.Tables.Add((bool)(tbj2.IsChecked) ?
                              (new JudgeListViewModel()).GenerateRandom(int.Parse(jcb.Text), 1) :
                              (new JudgeListViewModel()).Table.Copy());
            }
            if ((bool)(tbc1.IsChecked)) {
                ds.Tables.Add((bool)(tbc2.IsChecked) ?
                              (new ChairmanListViewModel()).GenerateRandom(int.Parse(ccb.Text), 1) :
                              (new ChairmanListViewModel()).Table.Copy());
            }
            if ((bool)(tbr1.IsChecked)) {
                ds.Tables.Add((new RegistrationListViewModel()).Table.Copy());
            }
            ExcelIO.Write(filepath, ds);
            pathtb.Text = filepath;
        }

        
        private void tbc2_Click(object sender, RoutedEventArgs e) {
            ccb.IsEnabled = (bool)(tbc2.IsChecked);
        }
        
        private void tbc1_Click(object sender, RoutedEventArgs e) {
            tbc2.IsEnabled = (bool)(tbc1.IsChecked);
            if (!(bool)(tbc1.IsChecked)) ccb.IsEnabled = false;
            if (tbc2.IsEnabled && (bool)(tbc2.IsChecked)) ccb.IsEnabled = true;
        }

        private void tbj2_Click(object sender, RoutedEventArgs e) {
            jcb.IsEnabled = (bool)(tbj2.IsChecked);
        }

        private void tbj1_Click(object sender, RoutedEventArgs e) {
            tbj2.IsEnabled = (bool)(tbj1.IsChecked);
            if (!(bool)(tbj1.IsChecked)) jcb.IsEnabled = false;
            if (tbj2.IsEnabled && (bool)(tbj2.IsChecked)) jcb.IsEnabled = true;
        }

        private void tbg2_Click(object sender, RoutedEventArgs e) {
            gcb.IsEnabled = (bool)(tbg2.IsChecked);
        }

        private void tbg1_Click(object sender, RoutedEventArgs e) {
            tbg2.IsEnabled = (bool)(tbg1.IsChecked);
            if (!(bool)(tbg1.IsChecked)) gcb.IsEnabled = false;
            if (tbg2.IsEnabled && (bool)(tbg2.IsChecked)) gcb.IsEnabled = true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e) {
            try {
                Process.Start(DataFileInfo.DesktopPath);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
