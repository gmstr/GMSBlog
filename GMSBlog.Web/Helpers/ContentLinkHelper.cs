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
using GMSBlog.Model.Entities;
using System.Text.RegularExpressions;

namespace GMSBlog.Web.Helpers
{
    public static class ContentLinkHelper
    {
        public static string Stylesheet(this HtmlHelper helper, string stylesheet)
        {
            return String.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", stylesheet);
        }

        public static string Javascript(this HtmlHelper helper, string script)
        {
            return String.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", script);
        }

        public static string MetaDescription(this HtmlHelper helper, string description)
        {
            return String.Format("<meta name=\"description\" content=\"{0}\" />", description);
        }

        public static string MetaKeywords(this HtmlHelper helper, string keywords)
        {
            return String.Format("<meta name=\"keywords\" content=\"{0}\" />", keywords);
        }



        public static string GetCommentDescriptionString(this HtmlHelper helper, Comment comment)
        {
            if (string.IsNullOrEmpty(comment.Website) && string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by {0} on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated);
            }
            else if (!string.IsNullOrEmpty(comment.Website) && string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by <a href=\"{2}\">{0}</a> on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Website);

            }
            else if (string.IsNullOrEmpty(comment.Website) && !string.IsNullOrEmpty(comment.Email))
            {
                return String.Format("Posted by <a href=\"mailto:{2}\">{0}</a> on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Email);
            }
            else
            {
                return String.Format("Posted by <a href=\"mailto:{2}\">{0}</a> (<a href=\"{3}\">{3}</a>) on {1:dd MMMM yyyy h:mm tt}", comment.Name, comment.DateCreated, comment.Email, comment.Website);
            }
        }

        public static string AddDashesToTitle(this HtmlHelper helper, string title)
        {
            var regex = new Regex(@"\w(-{1})\w");

            title = regex.Replace(title, x =>
            {
                string v = x.ToString();

                return String.Format("{0}--{1}", v[0], v[2]);
            });

            return title.Replace(" ", "-");
        }

        public static string RemoveDashesFromTitle(this HtmlHelper helper, string title)
        {
            var regex = new Regex(@"\w(-{3,5})\w");

            var temp = regex.Replace(title, x =>
            {
                string v = x.ToString();

                return String.Format("{0} {1} {2}", v[0], v.Substring(2, v.Length - 4), v[v.Length - 1]);

            });

            regex = new Regex(@"\w(-{1})\w");


            temp = regex.Replace(temp, x =>
            {
                string v = x.ToString();

                return String.Format("{0} {1}", v[0], v[2]);

            });

            regex = new Regex(@"\w(-{2})\w");


            return regex.Replace(temp, x =>
            {
                string v = x.ToString();

                return String.Format("{0}-{1}", v[0], v[3]);

            });


            //return title.Replace("-", " ");
        }
    }
}
