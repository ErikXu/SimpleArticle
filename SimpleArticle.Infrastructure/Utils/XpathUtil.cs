using System;
using HtmlAgilityPack;
using SimpleArticle.Domain.Utils;

namespace SimpleArticle.Infrastructure.Utils
{
    public class XpathUtil : IXpathUtil
    {
        public string LocateString(HtmlNode node, string xpath)
        {
            var nodeValue = node.SelectSingleNode(xpath);
            return nodeValue != null ? nodeValue.InnerText : null;
        }

        public DateTime LocateDate(HtmlNode node, string xpath)
        {
            var nodeValue = node.SelectSingleNode(xpath);

            if (nodeValue == null)
            {
                throw new NullReferenceException("The value of xpath is null.");
            }

            return DateTime.Parse(nodeValue.InnerText);
        }
    }
}