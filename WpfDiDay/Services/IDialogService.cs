namespace WpfDiDay.Services
{
    public interface IDialogService
    {
        void ShowWarning(string message, string title = "Thông báo");
        void ShowError(string message, string title = "Lỗi");
        void ShowSuccess(string message, string title = "Thành công");
        bool ShowConfirmation(string message, string title = "Xác nhận");
    }
}
