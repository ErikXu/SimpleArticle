using System.Collections.Generic;
using MongoDB.Bson;

namespace SimpleArticle.Wap.Models
{
    public class ArticleListView
    {
        public bool NoMore { get; set; }

        public List<ArticleListItem> Articles { get; set; }
    }

    public class ArticleListItem
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string PublishTime { get; set; }
    }
}