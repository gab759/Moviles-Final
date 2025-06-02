using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.Android;

public class NotificationInitializer : MonoBehaviour
{
    private const string idCanal = "default_channel";

    private void Awake()
    {

        RequestAuthorization();
        RegisterNotificationChannel();

    }


    private void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    private void RegisterNotificationChannel()
    {
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = idCanal,
            Name = "Canal Puntajes",
            Importance = Importance.Default,
            Description = "Notificaciones de puntajes del videojuego"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }
}