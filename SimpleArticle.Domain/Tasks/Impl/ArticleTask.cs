using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using SimpleArticle.Domain.Entities;
using SimpleArticle.Domain.Repositories;
using SimpleArticle.Domain.Utils;

namespace SimpleArticle.Domain.Tasks.Impl
{
    public class ArticleTask : IArticleTask
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ArticleTask));

        private readonly IArticleRepository _articleRepository;
        private readonly IWebSrcUtil _webSrcUtil;
        private readonly IXpathUtil _xpathUtil;

        public ArticleTask(IArticleRepository articleRepository, IWebSrcUtil webSrcUtil, IXpathUtil xpathUtil)
        {
            _articleRepository = articleRepository;
            _webSrcUtil = webSrcUtil;
            _xpathUtil = xpathUtil;
        }


        public void Download()
        {
            Logger.Info("开始下载文章...");

            const string listUrl = "http://funds.hexun.com/report/";
            var doc = _webSrcUtil.GetToHtml(listUrl, Encoding.GetEncoding("GB2312")).DocumentNode;

            var articleItems = doc.SelectNodes("//div[@class='temp01']/ul/li");

            var articles = new List<Article>();

            foreach (var item in articleItems)
            {
                var url = item.SelectSingleNode("a").Attributes["href"].Value;

                try
                {
                    var article = ParseArticle(url);
                    articles.Add(article);
                }
                catch (Exception ex)
                {
                    Logger.Error(string.Format("下载文章失败，Url：{0}", url), ex);
                }
            }

            if (articles.Count > 0)
            {
                foreach (var article in articles)
                {
                    if (_articleRepository.AsQueryable().Any(n => n.Title == article.Title))
                    {
                        continue;
                    }

                    _articleRepository.Insert(article);
                }
            }
        }

        private Article ParseArticle(string url)
        {
            var doc = _webSrcUtil.GetToHtml(url, Encoding.GetEncoding("GB2312")).DocumentNode;

            if (doc.SelectSingleNode("//div[@class='errorInf']") != null)
            {
                Logger.Error(string.Format("文章页面错误，Url：{0}", url));
                return null;
            }

            var article = new Article
            {
                SourceUrl = url,
                Title = _xpathUtil.LocateString(doc, "//div[@id='artibodyTitle']/h1/text()"),
                Source = _xpathUtil.LocateString(doc, "//span[@id='source_baidu']/a/text()"),
                PublishTimeText = _xpathUtil.LocateString(doc, "//span[@id='pubtime_baidu']/text()") ?? _xpathUtil.LocateString(doc, "//span[@id='artibodyDesc']/span[1]/text()"),
                Content = doc.SelectSingleNode("//div[@id='artibody']").InnerHtml
            };

            DateTime publicTime;

            if (DateTime.TryParse(article.PublishTimeText, out publicTime))
            {
                article.PublishTime = publicTime;
            }

            var paragraphNodes = doc.SelectNodes("//div[@id='artibody']/p");

            if (paragraphNodes != null && paragraphNodes.Count > 0)
            {
                article.Paragraphs = new List<Paragraph>();
                foreach (var paragraphNode in paragraphNodes)
                {
                    var paragraph = new Paragraph
                    {
                        Text = paragraphNode.InnerText.Trim(),
                        IsHeadline = (paragraphNode.SelectSingleNode("strong") != null)
                    };
                    article.Paragraphs.Add(paragraph);
                }
            }
            else
            {
                Logger.Error(string.Format("无法获取文章段落信息，Url：{0}", url));
            }

            return article;
        }
    }
}