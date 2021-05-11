using System.Collections.Generic;

namespace TeamManagement.Contracts.v1.Responses
{
    public class ArticleGetResponse
    {
        public string Tag { get; set; }
        public List<ArticleMenuResponse> Articles { get; set; }
    }
}
