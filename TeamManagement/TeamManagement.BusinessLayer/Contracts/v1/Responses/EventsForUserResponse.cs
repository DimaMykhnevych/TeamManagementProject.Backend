using System;
using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Responses
{
    public class EventsForUserResponse
    {
        public string CreatedByName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string Passcode { get; set; }
        public DateTime DateTime { get; set; }
        public List<AttendiesResponse> Attendies { get; set; }
    }
}
