using EmployeeLeave.Infrastructure.Managers.Interface;
using EmployeeLeaveScheduler.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EmployeeLeaveScheduler.Services
{
    public class ProcessVacationQueueService : IProcessVacationQueueRequestService
    {
        private readonly ILogger<ProcessVacationQueueService> _logger;
        private readonly IVacationRequestManager _vacationRequestManager;
        private readonly ILogVacationManager _logVacationManager;
        private readonly IApproveVacationManager _approveVacationManager;
        private readonly IRemoveApproveVacationManager _removeApproveVacationManager;

        public ProcessVacationQueueService(ILogger<ProcessVacationQueueService> logger, IVacationRequestManager vacationRequestManager, ILogVacationManager logVacationManager, IApproveVacationManager approveVacationManager, IRemoveApproveVacationManager removeApproveVacationManager)
        {
            _logger = logger;
            _vacationRequestManager = vacationRequestManager;
            _logVacationManager = logVacationManager;
            _approveVacationManager = approveVacationManager;
            _removeApproveVacationManager = removeApproveVacationManager;
        }

        public async Task ProcessMessage()
        {
            try
            {
                var result = await _vacationRequestManager.GetVacationRequest();

                if (result != null)
                {
                    _logger.LogInformation("Vacation request received.");

                    if (result.data.requestedDays > result.data.availableDays || result.data.availableDays == 0)
                    {
                        _logger.LogInformation("Employee does not have sufficient balance leaves.");
                        return;
                    }
                    else
                    {
                        var result1 = await _logVacationManager.LogVacationQueueRequest(result.messageId);
                        _logger.LogInformation("Employee leave request logged.");

                        var result2 = await _approveVacationManager.ApproveVacationRequest(result.messageId);
                        _logger.LogInformation("Requested employee vacation leaves approved.");

                        var result3 = await _removeApproveVacationManager.RemoveApprovedVacation(result.messageId);
                        _logger.LogInformation("Vacation request message remove from the queue system.");
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
