using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.ViewModels
{
      
    public class VacationRequestViewModel
    {
        public string messageId { get; set; }
        public data data { get; set; }
    }

    public class data
    {
        public int employeeId { get; set; }
        public int requestedDays { get; set; }
        public int availableDays { get; set; }

    }


}
