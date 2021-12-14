using MahApps.Metro.Controls;
using QuinaNadal.UI.Dialogs;

namespace QuinaNadal.UI.Dialogs
{
    public partial class MessageDialogWindow : MetroWindow
    {
        public MessageDialogWindow()
        {
            InitializeComponent();
        }

        public MessageDialogViewModel ViewModel
        {
            get => DataContext as MessageDialogViewModel;
            set => DataContext = value;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
