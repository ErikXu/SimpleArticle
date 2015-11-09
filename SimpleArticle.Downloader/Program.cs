using Autofac;
using log4net;
using SimpleArticle.Common;
using SimpleArticle.Domain.Tasks.Impl;

namespace SimpleArticle.Downloader
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main()
        {

            Logger.Info("开始下载数据...");

            var builder = new ContainerBuilder();
            Starter.Initialize(builder);

            var container = builder.Build();
            var articleTask = container.Resolve<IArticleTask>();
            articleTask.Download();
        }
    }
}
