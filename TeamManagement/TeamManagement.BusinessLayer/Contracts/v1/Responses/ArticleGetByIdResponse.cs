using System;

namespace TeamManagement.Contracts.v1.Responses
{
    public class ArticleGetByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string TagId { get; set; }
        public bool IsMadeByUser { get; set; }
        public string PublisherFullName { get; set; }
        public DateTime DateOfPublishing { get; set; }
    }
}
