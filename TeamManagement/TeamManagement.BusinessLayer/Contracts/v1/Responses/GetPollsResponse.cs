using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Responses
{
    public class GetPollsResponse
    {
        public string Name { get; set; }
        public bool DoesAllowMultiple { get; set; }
        public string CreatedByName { get; set; }
        public List<GetPollsOptionsResponse> Options { get; set; }
        public string Id { get; set; }
    }
}
