using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyBindingTest.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel() 
        {
            DisplayText = new ReactivePropertySlim<string>("Yes!");
            Command = new ReactiveCommand();
            Command.Subscribe(ChangeText);
        }

        private void ChangeText()
        {
            if (m_isYes)
            {
                DisplayText.Value = "No!";
                m_isYes= false;
            }
            else
            {
                DisplayText.Value = "Yes!";
                m_isYes = true;
            }
        }

        public ReactivePropertySlim<string> DisplayText { get; set; }

        public ReactiveCommand Command { get; set; }

        private bool m_isYes = true;
    }
}
