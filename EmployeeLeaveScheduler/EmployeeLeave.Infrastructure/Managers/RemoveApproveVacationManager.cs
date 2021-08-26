using EmployeeLeave.Infrastructure.Managers.Interface;
using EmployeeLeave.Infrastructure.Routes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers
{
    public class RemoveApproveVacationManager : IRemoveApproveVacationManager
    {
        private readonly HttpClient _httpClient;

        public RemoveApproveVacationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RemoveApprovedVacation(string messageId)
        {
            try
            {               

                var response = await _httpClient.DeleteAsync(String.Format(QueueEndPoints.RemoveVacationRequestQueue, messageId));
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
