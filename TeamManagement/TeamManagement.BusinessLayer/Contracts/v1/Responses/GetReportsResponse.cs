using System;
using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Responses
{
    public class GetReportsResponse
    {
        public string EmployeeFullName { get; set; }
        public DateTime DateOfPublishsing { get; set; }
        public List<string> CodeReview { get; set; }
        public List<string> Resolved { get; set; }
        public List<string> Active { get; set; }
    }
}
