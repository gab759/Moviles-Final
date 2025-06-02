using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationSystem : MonoBehaviour
{
    [Header("Configuración del Canal")]
    public NotificationChannelConfig config;

    public static event Action OnNotificationSent;
    public static event Action OnNotificationScheduled;
    public static event Action<int> OnNotificationCancelled;

    private void Awake()
    {
        RegisterChannel();
    }

    private void RegisterChannel()
    {
        if (config == null)
        {
            Debug.LogError("Falta asignar el NotificationChannelConfig.");
            return;
        }

        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            Id = config.channelId,
            Name = config.channelName,
            Importance = config.importance,
            Description = config.channelDescription
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    public void SendNotification(string title, string message)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = title,
            Text = message,
            SmallIcon = "icon_notification",
            FireTime = DateTime.Now
        };

        AndroidNotificationCenter.SendNotification(notification, config.channelId);
        OnNotificationSent?.Invoke();
    }

    public void ScheduleNotification(string title, string message, DateTime fireTime)
    {
        AndroidNotification notification = new AndroidNotification
        {
            Title = title,
            Text = message,
            SmallIcon = "icon_notification",
            FireTime = fireTime
        };

        AndroidNotificationCenter.SendNotification(notification, config.channelId);
        OnNotificationScheduled?.Invoke();
    }

    public void CancelNotification(int id)
    {
        AndroidNotificationCenter.CancelNotification(id);
        OnNotificationCancelled?.Invoke(id);
    }

    public void CancelAllNotifications()
    {
        AndroidNotificationCenter.CancelAllNotifications();
    }
}