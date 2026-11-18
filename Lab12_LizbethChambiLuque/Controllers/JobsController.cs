using System;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Lab12_LizbethChambiLuque.Services;

namespace Lab12_LizbethChambiLuque.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    [HttpPost("fire-and-forget")]
    public object CreateFireAndForgetJob([FromQuery] string user = "usuario1")
    {
        var jobId = BackgroundJob.Enqueue<NotificationService>(s => s.SendNotification(user));
        return new { Message = "Job Fire-and-forget creado con éxito.", JobId = jobId };
    }

    [HttpPost("delayed")]
    public object CreateDelayedJob([FromQuery] string user = "usuario2")
    {
        var jobId = BackgroundJob.Schedule<NotificationService>(
            s => s.SendNotification(user),
            TimeSpan.FromMinutes(10)
        );
        return new { Message = "Job Diferido creado para dentro de 10 minutos.", JobId = jobId };
    }

    [HttpPost("recurring")]
    public object CreateRecurringJob()
    {
        RecurringJob.AddOrUpdate<NotificationService>(
            "job-notificacion-diaria",
            s => s.SendNotification("usuario_diario"),
            Cron.Daily
        );
        return new { Message = "Job recurrente diario programado." };
    }

    [HttpPost("simulate-failure")]
    public object CreateFailingJob([FromQuery] string user = "usuario_fallo")
    {
        var jobId = BackgroundJob.Enqueue<NotificationService>(s => s.SendNotificationWithFailure(user));
        return new { Message = "Job con fallo encolado.", JobId = jobId };
    }

    [HttpPost("recurring-cleanup")]
    public object CreateCleanupJob()
    {
        RecurringJob.AddOrUpdate<NotificationService>(
            "job-limpieza-datos",
            s => s.CleanExpiredData(),
            Cron.Minutely
        );
        return new { Message = "Job recurrente de limpieza programado cada minuto." };
    }
}