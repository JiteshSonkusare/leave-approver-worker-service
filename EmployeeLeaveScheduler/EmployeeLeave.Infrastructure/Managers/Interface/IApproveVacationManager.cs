using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers.Interface
{
    public interface IApproveVacationManager
    {
        Task<string> ApproveVacationRequest(string messageId);
    }
}
