using System;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class EmployeeUpdateRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
