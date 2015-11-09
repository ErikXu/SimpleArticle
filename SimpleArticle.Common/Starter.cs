using Autofac;
using SimpleArticle.Common.Configs;
using SimpleArticle.Infrastructure.Mongo;

namespace SimpleArticle.Common
{
    public class Starter
    {
        public static void Initialize(ContainerBuilder builder)
        {
            //MongoDB Mapping
            Mapping.Set();
            MongoSetting.Current = new MongoSettingByConfig();
            AutofacComponentRegistrar.RegisterComponents(builder);
        }
    }
}