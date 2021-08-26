using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers.Interface
{
    public interface IRemoveApproveVacationManager
    {
        Task<string> RemoveApprovedVacation(string messageId);
    }
}
