using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamManagement.BusinessLayer.Options
{
    public class ChallengeResultData
    {
        public string ProviderName { get; set; }
        public AuthenticationProperties AuthenticationProperties { get; set; }
    }
}
