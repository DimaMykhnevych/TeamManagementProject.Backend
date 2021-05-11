using System;

namespace TeamManagement.Contracts.v1.Responses
{
    public class HowToArticleCreateResponse
    {
        public string Name { get; set; }
        public string Problem { get; set; }
        public string Solution { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string PublisherFullName { get; set; }
    }
}
