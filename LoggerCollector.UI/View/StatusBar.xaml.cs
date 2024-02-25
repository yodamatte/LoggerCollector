using LoggerCollector.UI.Services;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace LoggerCollector.UI.View
{
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            myPopup.IsOpen = !myPopup.IsOpen;
        }

    }
}
