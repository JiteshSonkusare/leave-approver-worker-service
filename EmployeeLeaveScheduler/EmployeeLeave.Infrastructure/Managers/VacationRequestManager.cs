using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.Managers.Interface;
using EmployeeLeave.Infrastructure.Routes;
using EmployeeLeave.Infrastructure.ViewModels;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers
{
    public class VacationRequestManager : IVacationRequestManager
    {
        private readonly HttpClient _httpClient;

        public VacationRequestManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<VacationRequestViewModel> GetVacationRequest()
        {
            try
            {
                var response = await _httpClient.GetAsync(QueueEndPoints.VacationRequestQueue);

                var responseAsString = await response.Content.ReadAsStringAsync();
                VacationRequestViewModel vacationRequestViewModel = null;

                if (responseAsString != "InternalServerError")
                {
                    vacationRequestViewModel = JsonSerializer.Deserialize<VacationRequestViewModel>(responseAsString);
                }

                return vacationRequestViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
