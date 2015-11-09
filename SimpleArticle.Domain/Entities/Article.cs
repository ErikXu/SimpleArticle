using System;
using System.Collections.Generic;

namespace SimpleArticle.Domain.Entities
{
    public class Article : Entity
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public DateTime? PublishTime { get; set; }
        public string PublishTimeText { get; set; }
        public string SourceUrl { get; set; }
    }

    public class Paragraph
    {
        public string Text { get; set; }
        public bool IsHeadline { get; set; }
    }
}