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

namespace mouseFocus
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

        private void Contact_Select(object send, MouseButtonEventArgs e)
        {
            var sender = (UIElement)send;
            Keyboard.Focus(sender);
        }

        private void Got_Focus(object send, RoutedEventArgs e)
        {
            var sender = (UIElement)send;
            AdornerLayer.GetAdornerLayer(sender).Add(new FocusAdorner(sender));
        }

        private void Lost_Focus(object send, RoutedEventArgs e)
        {
            var sender = (UIElement)send;
            var layer = AdornerLayer.GetAdornerLayer(sender);
            foreach (var adorner in layer.GetAdorners(sender))
                layer.Remove(adorner);
        }
    }
}
