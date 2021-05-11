using System;

namespace TeamManagement.Contracts.v1.Responses
{
    public class HowToArticleGetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public bool IsMadeByUser { get; set; }
        public string PublisherFullName { get; set; }
    }
}
