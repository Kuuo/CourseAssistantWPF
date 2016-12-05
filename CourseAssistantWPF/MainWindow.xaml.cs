using System.Windows.Input;

namespace CourseAssistantWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void TabablzControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            maindrag.SelectedIndex = 4;
        }
    }
}
