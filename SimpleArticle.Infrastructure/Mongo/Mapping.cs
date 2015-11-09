using MongoDB.Bson.Serialization.Conventions;

namespace SimpleArticle.Infrastructure.Mongo
{
    public class Mapping
    {
        public static void Set()
        {
            var pack = new ConventionPack { new IgnoreExtraElementsConvention(true), new IgnoreIfNullConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements&IgnoreIfNull", pack, type => true);
        }
    }
}