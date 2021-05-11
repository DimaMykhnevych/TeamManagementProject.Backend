namespace TeamManagement.Contracts.v1.Requests
{
    public class ArticleCreateRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string TagId { get; set; }
    }
}
