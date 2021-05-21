using System;
using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Requests
{
    public class EventCreateRequest
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Passcode { get; set; }
        public string[] Attendies { get; set; }
        public string DateTime { get; set; }
    }
}
