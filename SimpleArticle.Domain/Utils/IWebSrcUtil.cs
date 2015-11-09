using System.Text;
using HtmlAgilityPack;

namespace SimpleArticle.Domain.Utils
{
    public interface IWebSrcUtil
    {
        HtmlDocument GetToHtml(string url, Encoding specifiedEncoding = null); 
    }
}