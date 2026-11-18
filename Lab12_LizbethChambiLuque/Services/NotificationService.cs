using System;
using Hangfire;

namespace Lab12_LizbethChambiLuque.Services;

public class NotificationService
{
    public void SendNotification(string user)
    {
        Console.WriteLine($"[LOG] Notificación enviada a {user} en {DateTime.Now}");
    }

    [AutomaticRetry(Attempts = 0)]
    public void SendNotificationWithFailure(string user)
    {
        Console.WriteLine($"[LOG] Intentando enviar notificación a {user} en {DateTime.Now}");
        throw new Exception($"Error simulado: Fallo intencional al procesar notificación para {user}.");
    }

    public void CleanExpiredData()
    {
        Console.WriteLine($"[CRON] Proceso de limpieza ejecutado a las {DateTime.Now}");
    }
}