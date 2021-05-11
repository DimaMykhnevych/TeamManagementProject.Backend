using System;

namespace TeamManagement.Contracts.v1.Responses
{
    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool isAdmin { get; set; }
    }
}
