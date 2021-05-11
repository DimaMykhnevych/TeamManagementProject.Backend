using System;

namespace TeamManagement.Contracts.v1.Requests
{
    public class ArticleUpdateRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }

        public Guid TagId { get; set; }
    }
}
