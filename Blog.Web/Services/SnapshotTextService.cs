using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Blog.Services
{
    public class SnapshotText : ISnapshotText
    {
        public string GetFirstNCharacters(string html, int numCharacters)
        {
            // https://stackoverflow.com/a/10527026/3910619
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var chunks = new List<string>();

            foreach (var item in htmlDoc.DocumentNode.DescendantsAndSelf())
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            var joined = String.Join(" ", chunks);
            if(joined.Length <= numCharacters)
                return joined;

            return joined.Substring(0, numCharacters);
        }

    }
}
