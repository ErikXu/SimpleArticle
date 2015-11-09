using System.Text;
using HtmlAgilityPack;
using SimpleArticle.Domain.Utils;

namespace SimpleArticle.Infrastructure.Utils
{
    public class WebSrcUtil : IWebSrcUtil
    {
        public HtmlDocument GetToHtml(string url, Encoding specifiedEncoding = null)
        {
            var htmlWeb = new HtmlWeb();

            if (specifiedEncoding != null)
            {
                htmlWeb.OverrideEncoding = specifiedEncoding;
            }

            htmlWeb.UseCookies = true;

            var doc = htmlWeb.Load(url);
            return doc;
        }
    }
}