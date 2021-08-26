using EmployeeLeaveScheduler.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeLeaveScheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private DateTime _nextRun;
        private readonly CrontabSchedule _schedule;
        private static string Schedule => "*/30 * * * * *"; //Runs every 30 seconds

        private readonly IServiceProvider _services;

        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                _logger.LogInformation("Worker running new loop at: {time}", now);
                var nextRun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await ProcessAsync(stoppingToken);
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); 
            }
        }

        private async Task ProcessAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            stoppingToken.ThrowIfCancellationRequested();
            _logger.LogInformation($"Synchronizing start!");

            using (var scope = _services.CreateScope())
            {
                try
                {
                    var ser = scope.ServiceProvider.GetRequiredService<IProcessVacationQueueRequestService>();
                    await ser.ProcessMessage();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error message: {ex}");
                }
            }
            await Task.Delay(100, stoppingToken);
        }
    }
}
