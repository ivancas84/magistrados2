using CommunityToolkit.WinUI.Notifications;

using MagistradosApp.Contracts.Services;

using Windows.UI.Notifications;

namespace MagistradosApp.Services;

public partial class ToastNotificationsService : IToastNotificationsService
{
    public ToastNotificationsService()
    {
    }

    public void ShowToastNotification(ToastNotification toastNotification)
    {
        ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
    }
}
