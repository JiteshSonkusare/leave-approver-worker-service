using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeave.Infrastructure.Routes
{
    public static class QueueEndPoints
    {
        public const string MessageQueueUri = "http://localhost:8000/";
        public const string VacationRequestQueue = "queues/vacation-request";
        public const string LogVacationRequestQueue = "queues/vacation-request-processed-log";
        public const string ApprovedVacationRequestQueue = "queues/vacation-approved";
        public const string RemoveVacationRequestQueue = "queues/vacation-request/{0}";
        
    }
}
