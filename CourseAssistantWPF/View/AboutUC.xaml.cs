using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace CourseAssistantWPF.View {
    /// <summary>
    /// Interaction logic for SettingUC.xaml
    /// </summary>
    public partial class AboutUC : UserControl {
        public AboutUC() {
            InitializeComponent();
        }
        
        private void BtnQqchat_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText("1098335635");
        }

        private void BtnEmail_Click(object sender, RoutedEventArgs e) {
            try {
                Process.Start("mailto:hfkuuo@outlook.com");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void BtnGithub_Click(object sender, RoutedEventArgs e) {
            Process.Start("http://github.com/Kuuo");
        }

        private void BtnBlog_Click(object sender, RoutedEventArgs e) {
            Process.Start("http://kuuo.github.io");
        }
                
    }
}
