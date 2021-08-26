using EmployeeLeave.Infrastructure;
using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.Routes;
using EmployeeLeaveScheduler.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EmployeeLeaveScheduler
{
    public class Program
    {
        private const string ClientName = "EmployeeVaccationApp";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddgServiceCollection();
                    services.AddInfrastructureServices();
                    services.AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName));
                    services.AddHttpClient(ClientName, client =>
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HeaderContentExtension.HeaderContentType));
                        client.DefaultRequestVersion = HttpVersion.Version10;
                        client.BaseAddress = new Uri(QueueEndPoints.MessageQueueUri);
                    });
                    services.AddHostedService<Worker>();
                });
    }
}
