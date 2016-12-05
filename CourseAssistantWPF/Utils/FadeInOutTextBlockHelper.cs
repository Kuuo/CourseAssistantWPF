using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CourseAssistantWPF.Utils {

    public static class FadeInOutTextBlockHelper {

        public static void MakeFadeInOut(TextBlock tb, string text, int seconds = 3) {
            tb.Text = text;
            tb.Visibility = Visibility.Visible;
            var t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, seconds);
            t.Tick +=  (sender, eventArgs) => {
                tb.Visibility = Visibility.Hidden;
                ((DispatcherTimer)sender).Stop();
            };
            t.Start();
        }

    }
}
