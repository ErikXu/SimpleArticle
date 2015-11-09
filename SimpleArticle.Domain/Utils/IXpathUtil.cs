using System;
using HtmlAgilityPack;

namespace SimpleArticle.Domain.Utils
{
    public interface IXpathUtil
    {
        string LocateString(HtmlNode node, string xpath);

        DateTime LocateDate(HtmlNode node, string xpath);
    }
}