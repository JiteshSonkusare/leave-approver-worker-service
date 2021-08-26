using EmployeeLeaveScheduler.Services;
using EmployeeLeaveScheduler.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeLeaveScheduler.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddgServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IProcessVacationQueueRequestService, ProcessVacationQueueService>();

            return services;
        }
    }
}
