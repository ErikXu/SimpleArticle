using SimpleArticle.Domain.Entities;
using SimpleArticle.Domain.Repositories;
using SimpleArticle.Infrastructure.Mongo;

namespace SimpleArticle.Infrastructure.Repositories
{
    public class ArticleRepository : MongoRepository<Article>, IArticleRepository
    {
         
    }
}