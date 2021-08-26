using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveScheduler.Services.Interface
{
    public interface IProcessVacationQueueRequestService
    {
        Task ProcessMessage();
    }
}
