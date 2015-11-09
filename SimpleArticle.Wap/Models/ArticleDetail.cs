using System.Collections.Generic;
using SimpleArticle.Domain.Entities;

namespace SimpleArticle.Wap.Models
{
    public class ArticleDetail
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public string PublishTime { get; set; }
    }
}