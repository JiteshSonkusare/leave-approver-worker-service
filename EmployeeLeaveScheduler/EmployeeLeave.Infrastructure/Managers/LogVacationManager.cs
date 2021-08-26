using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.Managers.Interface;
using EmployeeLeave.Infrastructure.Routes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers
{
    public class LogVacationManager : ILogVacationManager
    {
        private readonly HttpClient _httpClient;

        public LogVacationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LogVacationQueueRequest(string messageId)
        {
            try
            {

                var response = await _httpClient.PostAsync(QueueEndPoints.LogVacationRequestQueue, HeaderContentExtension.GetHeaderContent(messageId));
                var responseAsString = await response.Content.ReadAsStringAsync();

                return responseAsString;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
