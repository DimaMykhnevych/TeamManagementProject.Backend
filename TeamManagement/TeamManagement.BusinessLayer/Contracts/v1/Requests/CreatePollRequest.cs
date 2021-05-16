using System;
using System.Collections.Generic;
using System.Text;

namespace TeamManagement.Contracts.v1.Requests
{
    public class CreatePollRequest
    {
        public string Name { get; set; }
        public bool DoesAllowMultiple { get; set; }
        public List<CreateOptionRequest> Options { get; set; }
    }
}
