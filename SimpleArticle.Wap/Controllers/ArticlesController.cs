using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MongoDB.Bson;
using SimpleArticle.Domain.Repositories;
using SimpleArticle.Wap.Models;

namespace SimpleArticle.Wap.Controllers
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : ApiController
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        [Route("{offset:int}")]
        public ArticleListView List(int? offset)
        {
            if (offset == null)
            {
                offset = 0;
            }

            const int pageSize = 20;

            var articles = _articleRepository.AsQueryable().Where(n => n.PublishTime != null)
                                                                    .OrderByDescending(n => n.PublishTime)
                                                                    .Skip(offset.Value).Take(pageSize);

            var view = new ArticleListView
            {
                Articles = articles.Select(n => new ArticleListItem { Id = n.Id, Title = n.Title, PublishTime = n.PublishTime.HasValue ? n.PublishTime.Value.ToLocalTime().ToString("yyyy-MM-dd") : "--" }).ToList(),
                NoMore = (offset.Value + pageSize) >= _articleRepository.AsQueryable().Count(n => n.PublishTime != null)
            };

            return view;
        }

        [HttpGet]
        [Route("{id}")]
        public ArticleDetail Get([ModelBinder] ObjectId id)
        {
            var article = _articleRepository.AsQueryable().Single(n => n.Id == id);

            return new ArticleDetail
            {
                Title = article.Title,
                Source = article.Source,
                Paragraphs = article.Paragraphs,
                PublishTime = article.PublishTime.HasValue ? article.PublishTime.Value.ToLocalTime().ToString("yyyy-MM-dd") : "--"
            };
        }
    }
}
