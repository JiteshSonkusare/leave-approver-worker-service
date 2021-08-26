using EmployeeLeave.Infrastructure.Managers;
using EmployeeLeave.Infrastructure.Managers.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeLeave.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IApproveVacationManager, ApproveVacationManager>();
            services.AddScoped<ILogVacationManager, LogVacationManager>();
            services.AddScoped<IRemoveApproveVacationManager, RemoveApproveVacationManager>();
            services.AddScoped<IVacationRequestManager, VacationRequestManager>();

            return services;
        }
    }
}
