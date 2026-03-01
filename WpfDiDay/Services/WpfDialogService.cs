using System.Windows;

namespace WpfDiDay.Services
{
    public class WpfDialogService : IDialogService
    {
        public void ShowWarning(string message, string title = "Thông báo")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ShowError(string message, string title = "Lỗi")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowSuccess(string message, string title = "Thành công")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
