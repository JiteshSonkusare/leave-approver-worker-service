using EmployeeLeave.Infrastructure.Extensions;
using EmployeeLeave.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Managers.Interface
{
    public interface IVacationRequestManager
    {
       Task<VacationRequestViewModel> GetVacationRequest();
    }
}
