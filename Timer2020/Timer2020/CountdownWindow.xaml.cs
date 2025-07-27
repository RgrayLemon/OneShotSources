using System.Windows;
using System.Windows.Threading;

namespace Timer2020
{
    /// <summary>
    /// CountdownWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CountdownWindow : Window
    {
        private int _countdownSeconds;
        private readonly DispatcherTimer _countdownTimer = new();
        private bool _isFinish = false;

        public CountdownWindow()
        {
            InitializeComponent();

            Loaded += CountdownWindow_Loaded;
        }

        private void CountdownWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _countdownSeconds = 20;
            CountdownSecText.Text = _countdownSeconds.ToString();

            _countdownTimer.Interval = TimeSpan.FromSeconds(1);
            _countdownTimer.Tick += CountDownSec;
            _countdownTimer.Start();
        }

        private void CountDownSec(object? sender, EventArgs e)
        {
            _countdownSeconds--;
            CountdownSecText.Text = _countdownSeconds.ToString();

            if (_countdownSeconds <= 0)
            {
                _countdownTimer.Stop();
                ((App)System.Windows.Application.Current).StartTwentyMinuteTimer();
                _isFinish = true;
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_isFinish)
            {
                e.Cancel = true;
            }
        }
    }
}
