using System;
using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Responses
{
    public class GetReportsResponse
    {
        public string EmployeeFullName { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string AdditionalComment { get; set; }
        public List<string> CodeReview { get; set; } = new List<string>();
        public List<string> Resolved { get; set; } = new List<string>();
        public List<string> Active { get; set; } = new List<string>();
    }
}
