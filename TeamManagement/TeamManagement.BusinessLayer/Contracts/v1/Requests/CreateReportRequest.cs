namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class CreateReportRequest
    {
        public string[] Resolved { get; set; }
        public string[] CodeReview { get; set; }
        public string[] Active { get; set; }
        public string AdditionalComment { get; set; }
    }
}
