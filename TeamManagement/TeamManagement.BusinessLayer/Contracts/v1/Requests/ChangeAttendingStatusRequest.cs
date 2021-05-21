namespace TeamManagement.Contracts.v1.Requests
{
    public class ChangeAttendingStatusRequest
    {
        public string Id { get; set; }   
        public string Status { get; set; }
    }
}
