using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS1.Core.ViewModels
{
    public class ActivityLogViewModel
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public string ExecutionTime { get; set; }
        public int ExecutionDuration { get; set; }
        public string ClientIpAddress { get; set; }
        public string BrowserInfo { get; set; }
        public string HttpMethod { get; set; }
        public string Url { get; set; }
        public int HttpStatusCode { get; set; }
        public string Comments { get; set; }
        public string Parameters { get; set; }
        public string Headers { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
