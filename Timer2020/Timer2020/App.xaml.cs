using System.Windows;
using System.Windows.Threading;

namespace Timer2020
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private DispatcherTimer _twentyMinuteTimer;
        private DateTime _startTime;
        private readonly TimeSpan _duration = TimeSpan.FromMinutes(20);
        private System.Windows.Forms.NotifyIcon? _notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupNotifyIcon();
        }

        public void SetupNotifyIcon()
        {
            var icon = GetResourceStream(new Uri("telescope.ico", UriKind.Relative)).Stream;
            var menu = new System.Windows.Forms.ContextMenuStrip();
            menu.Items.Add("終了", null, Exit_Cilck);
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = new System.Drawing.Icon(icon),
                Text = "未実行",
                ContextMenuStrip = menu
            };
            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);
        }


        public void StartTwentyMinuteTimer()
        {
            _startTime = DateTime.Now;

            _twentyMinuteTimer?.Stop();

            _twentyMinuteTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _twentyMinuteTimer.Tick += IntervalTimer_Tick;
            _twentyMinuteTimer.Start();
        }

        public String GetRemainingTimer()
        {
            TimeSpan remainingTimeSpan = TimeSpan.Zero;

            if (_twentyMinuteTimer != null && _twentyMinuteTimer.IsEnabled)
            {
                TimeSpan elapsed = DateTime.Now - _startTime;
                TimeSpan remaining = _duration - elapsed;

                remainingTimeSpan = remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
            }

            return remainingTimeSpan.ToString(@"mm\:ss");
        }

        private void IntervalTimer_Tick(object? sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _startTime;
            TimeSpan remaining = _duration - elapsed;

            if (remaining <= TimeSpan.Zero)
            {
                _twentyMinuteTimer.Stop();

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var countdownWindow = new CountdownWindow();
                    countdownWindow.Show();
                });
            }

            if (_notifyIcon != null)
                _notifyIcon.Text = GetRemainingTimer();
        }

        public void StopAllTimers()
        {
            _twentyMinuteTimer?.Stop();
            if (_notifyIcon != null)
                _notifyIcon.Text = "停止中";
        }

        private void NotifyIcon_Click(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var wnd = System.Windows.Application.Current.MainWindow;
                if (wnd != null)
                {
                    wnd.Show();
                    wnd.WindowState = WindowState.Normal;
                    wnd.Activate();
                }
            }
        }
        private void Exit_Cilck(object? sender, EventArgs e)
        {
            Shutdown();
        }

    }

}
