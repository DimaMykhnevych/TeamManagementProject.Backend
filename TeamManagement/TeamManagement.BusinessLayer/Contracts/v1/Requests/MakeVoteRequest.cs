namespace TeamManagement.Contracts.v1.Requests
{
    public class MakeVoteRequest
    {
        public string PollId { get; set; }
        public string OptionId { get; set; }
    }
}
