using UnityEngine;
using Unity.Notifications.Android;

[CreateAssetMenu(fileName = "NotificationChannelConfig", menuName = "Scriptable Objects/Notifications", order = 3)]
public class NotificationChannelConfig : ScriptableObject
{
    public string channelId = "default_channel";
    public string channelName = "Canal Puntajes";
    public string channelDescription = "Notificaciones de puntajes del videojuego";
    public Importance importance = Importance.Default;
}