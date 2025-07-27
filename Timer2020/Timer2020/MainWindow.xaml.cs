using System.ComponentModel;
using System.Windows;

namespace Timer2020
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).StartTwentyMinuteTimer();
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).StopAllTimers();
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // ×ボタンを押されてもアプリを終了しない
            e.Cancel = true;
            this.Hide();
        }
    }
}