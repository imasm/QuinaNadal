using MahApps.Metro.Controls;
using QuinaNadal.UI.Dialogs;
using QuinaNadal.ViewModels;
using System.Windows.Input;

namespace QuinaNadal
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += HandleKeyDownEvent;
        }

        public MainViewModel ViewModel { get => this.DataContext as MainViewModel; set => this.DataContext = value; }

        private void HandleKeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.X && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                ClearScreen();
            }
        }

        private void ClearScreen()
        {
            var viewModel = new MessageDialogViewModel();
            viewModel.Title = "Netejar taulell";
            viewModel.Message = "Netejar tot el taulell?";

            var msgWindow = new MessageDialogWindow();
            msgWindow.Owner = this;
            msgWindow.ViewModel = viewModel;
            if (msgWindow.ShowDialog().GetValueOrDefault())
            {
                ViewModel?.Clear();
            }
        }
    }
}
