using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using MarkdownSharp;

namespace GMSBlog.Web.Helpers
{
    public static class MarkdownHelper
    {
        static MarkdownHelper()
        {
            _markdown = new Markdown();
        }

        private static Markdown _markdown;
        public static string RenderMarkdown(string content)
        {
            return _markdown.Transform(content);
        }
        public static string RenderMarkdown(this HtmlHelper html, string content)
        {
            return _markdown.Transform(content);
        }
    }
}
