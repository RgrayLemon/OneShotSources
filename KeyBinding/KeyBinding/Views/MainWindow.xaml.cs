using KeyBindingTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyBindingTest.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // ②InputBindingsをコードバインディングで行う
            var windowKeyBinding = new KeyBinding();

            // GestureまたはKeyとModifiersで指定する
            // Gestureプロパティだと指定できないキーの組み合わせがある
            // windowKeyBinding.Gesture = new KeyGesture(Key.Enter, ModifierKeys.Shift);

            windowKeyBinding.Key = Key.H;
            windowKeyBinding.Modifiers = ModifierKeys.Shift;
            // Modifierキーを複数設定したければ以下のように
            // windowKeyBinding.Modifiers = (ModifierKeys.Shift | ModifierKeys.Alt);

            windowKeyBinding.Command = (DataContext as MainWindowViewModel).Command;
            this.InputBindings.Add(windowKeyBinding);

            // ③キーダウンイベントを設定し、メソッド内で押下キーの判断
            // this.KeyDown += Window_OnKeyDown;
        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {

            // Keyboard.IsKeyDown(Key.K)でも
            if (Keyboard.Modifiers == ModifierKeys.Shift && e.Key == Key.K)
            {
                (DataContext as MainWindowViewModel).Command.Execute();
            }

            // 複数のModifierKeyを条件にしたい場合
            if (Keyboard.Modifiers == (ModifierKeys.Shift | ModifierKeys.Alt))
            {
                (DataContext as MainWindowViewModel).Command.Execute();
            }

            // ModifierKey複数と普通のキーを混ぜる場合は無理っぽい?
            // 以下の分岐は反応しない
            if (Keyboard.Modifiers == (ModifierKeys.Shift | ModifierKeys.Alt) && e.Key == Key.K)
            {
                (DataContext as MainWindowViewModel).Command.Execute();
            }

            // ModifierKeyじゃないキーを複数指定したい場合はIsKeyDownを並べていく以外の方法は多分無理
            if (Keyboard.IsKeyDown(Key.K) && Keyboard.IsKeyDown(Key.V))
            {
                (DataContext as MainWindowViewModel).Command.Execute();
            }
        }
    }
}
