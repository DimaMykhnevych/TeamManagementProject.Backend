using System;

namespace TeamManagement.Contracts.v1.Responses
{
    public class ArticleCreateResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string PublisherUserName { get; set; }
        public DateTime DateOfPublishing { get; set; }
    }
}
