using System.Collections.Generic;
using TeamManagement.Contracts.v1.Requests;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class PollUpdateRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool DoesAllowMultiple { get; set; }
        public List<CreateOptionRequest> Options { get; set; }
    }
}
