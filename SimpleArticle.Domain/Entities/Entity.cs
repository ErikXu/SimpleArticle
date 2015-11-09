using MongoDB.Bson;

namespace SimpleArticle.Domain.Entities
{
    public abstract class EntityWithTypedId<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class Entity : EntityWithTypedId<ObjectId>
    {
    }
}
